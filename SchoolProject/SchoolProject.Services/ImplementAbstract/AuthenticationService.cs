
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Services.ImplementAbstract
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly schoolDBContext _context;
        private readonly IEncryptionProvider _encryptionProvider;

        // private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;

        public AuthenticationService(JWTSettings jwtSettings, IRefreshTokenRepo refreshTokenRepo,
            UserManager<ApplicationUser> userManager, IEmailService emailService, schoolDBContext context)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepo = refreshTokenRepo;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
            // _UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }

        public async Task<JwtAuthResponse> GetJWTToken(ApplicationUser user)
        {
            var (jwtToken, accessToken) = await GenerateJwtToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken()
            {
                AddTime = DateTime.Now,
                ExpiredDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenToRefresh,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepo.AddAsync(userRefreshToken);

            return new JwtAuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshToken GetRefreshToken(string UserName)
        {
            var refreshToken = new RefreshToken()
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = UserName,
                TokenToRefresh = GenerateRefreshToken()
            };
            // _UserRefreshToken.AddOrUpdate(refreshToken.TokenToRefresh, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        public async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userClaimsFromDB = await _userManager.GetClaimsAsync(user);
            List<Claim> UserClaims = new List<Claim>()
                        {
                            // token generated Id Changes
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                            new Claim(JwtRegisteredClaimNames.Email,user.Email)
                        };
            UserClaims.AddRange(userClaimsFromDB);
            foreach (var role in roles)
            {
                UserClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return UserClaims;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(ApplicationUser user)
        {
            var UserClaims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: UserClaims,
                    expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }

        public async Task<JwtAuthResponse> GetRefreshToken(ApplicationUser user, JwtSecurityToken readToken
                                                           , DateTime? expireDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);
            var response = new JwtAuthResponse();
            response.AccessToken = newToken;
            var refreshTokenResponse = new RefreshToken();
            refreshTokenResponse.UserName = readToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value;
            refreshTokenResponse.TokenToRefresh = refreshToken;
            refreshTokenResponse.ExpireAt = (DateTime)expireDate;
            return response;
        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(accessToken);
        }

        public async Task<string> ValidatorToken(string accessToken)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };

            try
            {
                tokenHandler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validatedToken == null)
                    throw new SecurityTokenException("Invalid Token");

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken readToken, string accessToken, string refreshToken)
        {
            if (readToken == null || !readToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return ("Algorithm is wrong", null);
            if (readToken.ValidTo > DateTime.UtcNow)
                return ("Token is not Expited", null);

            var userId = readToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.NameId).Value;
            var refreshTokenOfUser = await _refreshTokenRepo.GetTableNoTracking()
                .FirstOrDefaultAsync(t => t.Token == accessToken && t.RefreshToken == refreshToken && t.UserId == userId);
            if (refreshTokenOfUser.ExpiredDate < DateTime.UtcNow)
            {
                refreshTokenOfUser.IsRevoked = true;
                refreshTokenOfUser.IsUsed = false;
                await _refreshTokenRepo.UpdateAsync(refreshTokenOfUser);
                return ("Refresh Token is Expited", null);
            }
            if (refreshTokenOfUser == null)
                return ("No Refresh Token", null);
            var expiryDate = refreshTokenOfUser.ExpiredDate;
            return (userId, expiryDate);
        }

        public async Task<string> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(userId))
                return "ErrorInConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId);
            var confirmemail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmemail.Succeeded)
                return "ErrorInConfirmEmail";
            return "Success";
        }

        public async Task<string> SendResetPasswordCode(string email)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return "UserNotExist";

                Random random = new Random();
                string randomCode = random.Next(0, 1000000).ToString("D6");
                //var encryptCode =  _encryptionProvider.Encrypt(randomCode);
                user.Code = randomCode;
                var updatedUser = await _userManager.UpdateAsync(user);
                if (!updatedUser.Succeeded) return "FailedToUpdateUserCode";

                string message = $"Use this code to reset your password . \n {randomCode} \n SchoolProject";

                await _emailService.SendEmailAsync(email, message, "Reset Password");

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ResetPasswordCode(string code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "UserNotExist";
            //var userCode = _encryptionProvider.Decrypt(user.Code);
            var userCode = user.Code;
            if (userCode == code) return "Success";
            return "Failed";
        }

        public async Task<string> SetNewPassword(string email, string password)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return "UserNotExist";
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "failed";
            }
        }
    }
}
