using SchoolProject.Data.Entities.Identity;


namespace SchoolProject.Services.Abstract
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(ApplicationUser user, string password);
    }
}
