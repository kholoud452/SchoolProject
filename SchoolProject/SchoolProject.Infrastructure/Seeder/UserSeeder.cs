using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var usersCount = await userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = "admin@project.com",
                    Email = "admin@project.com",
                    FullName = "SchoolProject",
                    Country = "Egypt",
                    PhoneNumber = "01000000000",
                    Address = "Cairo",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await userManager.CreateAsync(defaultUser, "Admin@123");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }

        }
    }
}
