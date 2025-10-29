using MediatR;
using SchoolProject.Core.Features.User.Queries.Results;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.User.Queries.Models
{
    public class GitUserPaginationListQuery :IRequest<PaginatedResult<GitUserPaginatedListResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
