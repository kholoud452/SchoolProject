
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRoleListQuery, Response<List<GetRoleResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleResponse>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResults>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationCodeService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationCodeService authorizationService,
            IMapper mapper, UserManager<ApplicationUser> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<List<GetRoleResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roleList = await _authorizationService.GetRoleListAsync();
            var roleMapper = _mapper.Map<List<GetRoleResponse>>(roleList);
            var result = Success(roleMapper);
            result.Meta = new { Count = roleMapper.Count() };
            return result;
        }

        public async Task<Response<GetRoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
                return BadRequest<GetRoleResponse>(_stringLocalizer[SharedResourcesKeys.RoleIsNotFound]);
            var roleMapper = _mapper.Map<GetRoleResponse>(role);
            var result = Success(roleMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }

        public async Task<Response<ManageUserRolesResults>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound<ManageUserRolesResults>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
            var userRoles = await _authorizationService.GetManageUserRoles(user);
            var result = Success(userRoles);
            result.Meta = new { Operation = "Success" };
            return result;

        }


    }
}
