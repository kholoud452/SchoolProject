using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authantication.Query.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authantication.Query.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<GetAuthenticationQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(GetAuthenticationQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ValidatorToken(request.AccessToken);
            if (response == "NotExpired")
                return Success(response);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenisExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmedEmail = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmedEmail == "ErrorInConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorInConfirmEmail]);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailisCompleted]);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPasswordCode(request.Code, request.Email);

            switch (result)
            {
                case "UserNotExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvalidCode]);
                case "Success": return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(result);
            }
        }
    }
}
