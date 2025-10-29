using SchoolProject.Core.Features.User.Queries.Results;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void GitUserListQueryMapping() 
        { 
            CreateMap<ApplicationUser,GitUserPaginatedListResult>().ReverseMap();
        }

    }
}
