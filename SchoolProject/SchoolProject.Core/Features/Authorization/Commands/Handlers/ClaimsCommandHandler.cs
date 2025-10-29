using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;

        public ClaimsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            if (result == "UserNotFound")
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
            else if (result == "FailedToRemoveClaims")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveClaims]);
            else if (result == "FailedToUpdateClaims")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateClaims]);
            else if (result == "FailedToAddNewClaims")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.ClaimsUpdatedSuccessfully]);
            return BadRequest<string>(result);
        }
    }
}
