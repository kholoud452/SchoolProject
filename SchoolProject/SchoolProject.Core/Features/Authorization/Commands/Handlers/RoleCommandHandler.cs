using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserClaimsCommend, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;

        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.AddRoleAsync(request.RoleName);
            if (role != "Success")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddRole]);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.RoleAddedSuccefully]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound") return NotFound<string>(_stringLocalizer[SharedResourcesKeys.RoleIsNotFound]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.RoleUpdatedSuccessfully]);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.RoleIsNotFound]);
            else if (result == "Used")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            return BadRequest<string>(result);
        }
        public async Task<Response<string>> Handle(UpdateUserClaimsCommend request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            if (result == "UserNotFound")
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
            else if (result == "FailedToRemoveRoles")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveRoles]);
            else if (result == "FailedToUpdateRoles")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateRoles]);
            else if (result == "FailedToAddNewRoles")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.ClaimsUpdatedSuccessfully]);
            return BadRequest<string>(result);
        }
    }
}
