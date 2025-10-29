using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Queries.Models;
using SchoolProject.Core.Features.User.Queries.Results;
using SchoolProject.Core.SharedResource;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GitUserPaginationListQuery,PaginatedResult<GitUserPaginatedListResult>>,
        IRequestHandler<GetUserByIdQuery,Response<GetUserByIdResult>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper, UserManager<ApplicationUser> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<GitUserPaginatedListResult>> Handle(GitUserPaginationListQuery request, CancellationToken cancellationToken)
        {
            var users =  _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GitUserPaginatedListResult>(users).ToPaginatedListAsync(request.PageNumber,request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userById = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (userById == null)
                return NotFound<GetUserByIdResult>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var userMapper = _mapper.Map<GetUserByIdResult>(userById);
            var result = Success(userMapper);
            result.Meta = new { Operation = "Success" };
            return result;
        }
    }
}
