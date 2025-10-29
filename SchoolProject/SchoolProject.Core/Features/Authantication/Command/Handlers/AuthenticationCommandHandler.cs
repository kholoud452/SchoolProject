using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authantication.Command.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authantication.Command.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResponse>>,
        IRequestHandler<SendResetPasswordCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var userFromDB = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (userFromDB == null)
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(userFromDB, request.Password, false);
            if (!signInResult.Succeeded)
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.ProblemWithNameOrPass]);
            if (!userFromDB.EmailConfirmed)
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            var token = await _authenticationService.GetJWTToken(userFromDB);
            return Success(token);
        }

        public async Task<Response<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var readToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDateFromValidatorDetails = await _authenticationService
                                                  .ValidateDetails(readToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDateFromValidatorDetails)
            {
                case ("Algorithm is wrong", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.Algorithmiswrong]);
                case ("Token is not Expited", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.TokenisnotExpired]);
                case ("Refresh Token is Expited", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenisExpired]);
                case ("No Refresh Token", null):
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.NoRefreshToken]);
            }
            //var (userId, expireDate) = userIdAndExpireDateFromValidatorDetails;
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userIdAndExpireDateFromValidatorDetails.Item1);
            if (user == null)
                return NotFound<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var response = await _authenticationService.GetRefreshToken(user, readToken, userIdAndExpireDateFromValidatorDetails.Item2, request.RefreshToken);
            return Success(response);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);

            switch (result)
            {
                case "UserNotExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "FailedToUpdateUserCode": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToSendUserCode]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
                case "Success": return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SetNewPassword(request.Email, request.NewPassword);

            switch (result)
            {
                case "UserNotExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
                case "Success": return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(result);
            }
        }
    }
}
