using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUsermapping();
            GitUserListQueryMapping();
            GetUserBuIdMapping();
            UpdateUserMapping();
        }
    }
}
