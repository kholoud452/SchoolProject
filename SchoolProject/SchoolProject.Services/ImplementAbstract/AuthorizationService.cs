using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;
using System.Security.Claims;

namespace SchoolProject.Services.ImplementAbstract
{

    public class AuthorizationService : IAuthorizationCodeService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly schoolDBContext _context;

        public AuthorizationService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            schoolDBContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var addedRole = await _roleManager.CreateAsync(identityRole);
            if (addedRole.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(string id)
        {
            var roleFromDB = await _roleManager.FindByIdAsync(id);
            if (roleFromDB == null)
                return "NotFound";

            var userRole = await _userManager.GetUsersInRoleAsync(roleFromDB.Name);
            if (userRole != null && userRole.Count > 0)
                return "Used";

            var result = await _roleManager.DeleteAsync(roleFromDB);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("_", result.Errors);
            return errors;
        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var roleFromDB = await _roleManager.FindByIdAsync(request.Id);
            if (roleFromDB == null)
                return "NotFound";
            roleFromDB.Name = request.Name;
            var result = await _roleManager.UpdateAsync(roleFromDB);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("_", result.Errors);
            return errors;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            var roleFromDB = await _roleManager.FindByIdAsync(id);
            if (roleFromDB == null)
                return null;
            return roleFromDB;
        }

        public async Task<List<IdentityRole>> GetRoleListAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
                return new List<IdentityRole>();
            return roles;
        }

        public async Task<bool> IsRoleNameExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<ManageUserRolesResults> GetManageUserRoles(ApplicationUser user)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var response = new ManageUserRolesResults();
            var responseRolesList = new List<Roles>();
            response.UserId = user.Id;
            foreach (var item in roles)
            {
                var userRole = new Roles();
                userRole.Id = item.Id;
                userRole.Name = item.Name;
                if (await _userManager.IsInRoleAsync(user, item.Name))
                    userRole.HasRole = true;
                responseRolesList.Add(userRole);
            }
            response.Roles = responseRolesList;
            return response;
        }
        public async Task<ManageUserClaimsResults> ManageUserClaims(ApplicationUser user)
        {
            var response = new ManageUserClaimsResults();
            response.UserId = user.Id;
            var responseClaimsList = new List<UserClaims>();
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var item in ClaimsSrores.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = item.Type;
                if (userClaims.Any(t => t.Type == item.Type))
                    userClaim.Value = true;
                responseClaimsList.Add(userClaim);
            }
            response.UserClaims = responseClaimsList;
            return response;
        }

        public async Task<string> UpdateUserRoles(ManageUserRolesResults userRoles)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(userRoles.UserId);
                if (user == null)
                    return "UserNotFound";
                var rolesFromDB = await _userManager.GetRolesAsync(user);
                var removedRoles = await _userManager.RemoveFromRolesAsync(user, rolesFromDB);
                if (!removedRoles.Succeeded)
                    return "FailedToRemoveRoles";
                var selectedRoles = userRoles.Roles.Where(h => h.HasRole == true).Select(n => n.Name);
                var updatedRoles = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!updatedRoles.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateRoles";
            }
        }

        public async Task<string> UpdateUserClaims(ManageUserClaimsResults userClaims)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(userClaims.UserId);
                if (user == null)
                    return "UserNotFound";
                var claimsFromDB = await _userManager.GetClaimsAsync(user);
                var removedClaims = await _userManager.RemoveClaimsAsync(user, claimsFromDB);
                if (!removedClaims.Succeeded)
                    return "FailedToRemoveClaims";
                var selectedClaims = userClaims.UserClaims.Where(c => c.Value == true)
                                                          .Select(c => new Claim(c.Type, c.Value.ToString()));
                var updatedClaims = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!updatedClaims.Succeeded)
                    return "FailedToAddNewClaims";
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
    }
}
