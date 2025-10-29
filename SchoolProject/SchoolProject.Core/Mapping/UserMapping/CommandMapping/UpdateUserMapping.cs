using SchoolProject.Core.Features.User.Commands.Models;
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
        public void UpdateUserMapping()
        {
            CreateMap<ApplicationUser,UpdateUserCommand>().ReverseMap();
        }
    }
}
