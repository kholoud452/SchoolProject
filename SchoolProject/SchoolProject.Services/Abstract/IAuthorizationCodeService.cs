using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;


namespace SchoolProject.Services.Abstract
{
    public interface IAuthorizationCodeService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<List<IdentityRole>> GetRoleListAsync();
        public Task<IdentityRole> GetRoleByIdAsync(string id);
        public Task<ManageUserRolesResults> GetManageUserRoles(ApplicationUser user);
        public Task<ManageUserClaimsResults> ManageUserClaims(ApplicationUser user);
        public Task<string> UpdateUserRoles(ManageUserRolesResults userRoles);
        public Task<string> UpdateUserClaims(ManageUserClaimsResults userClaims);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(string id);
        public Task<bool> IsRoleNameExist(string roleName);
    }
}
