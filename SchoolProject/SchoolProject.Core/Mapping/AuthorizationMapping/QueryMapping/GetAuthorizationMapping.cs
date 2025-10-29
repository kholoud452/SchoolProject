using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Authorization.Queries.Results;


namespace SchoolProject.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void GetAuthorizationMapping()
        {
            CreateMap<IdentityRole, GetRoleResponse>().ReverseMap();
        }
    }
}
