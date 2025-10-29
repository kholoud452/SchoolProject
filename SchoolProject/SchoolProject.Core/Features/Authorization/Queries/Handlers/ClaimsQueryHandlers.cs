using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandlers : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResults>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsQueryHandlers(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService,
            UserManager<ApplicationUser> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async Task<Response<ManageUserClaimsResults>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound<ManageUserClaimsResults>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
            var userClaims = await _authorizationService.ManageUserClaims(user);
            return Success(userClaims);
        }
    }
}
