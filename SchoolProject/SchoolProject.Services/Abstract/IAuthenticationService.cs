using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Services.Abstract
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJWTToken(ApplicationUser user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken readToken, string accessToken, string refreshToken);
        public Task<JwtAuthResponse> GetRefreshToken(ApplicationUser user, JwtSecurityToken readToken
                                                          , DateTime? expireDate, string refreshToken);
        public Task<string> ValidatorToken(string accessToken);
        public Task<string> ConfirmEmail(string userId, string code);
        public Task<string> SendResetPasswordCode(string email);
        public Task<string> ResetPasswordCode(string code, string email);
        public Task<string> SetNewPassword(string email, string password);
    }
}
