using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Services.ImplementAbstract
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;
        private readonly schoolDBContext _context;
        private readonly IUrlHelper _urlHelper;

        public UserService(UserManager<ApplicationUser> userManager,
                           IHttpContextAccessor contextAccessor,
                           IEmailService emailService,
                           schoolDBContext context,
                           IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
            _context = context;
            _urlHelper = urlHelper;
        }
        public async Task<string> AddUserAsync(ApplicationUser user, string password)
        {
            var trnsact = await _context.Database.BeginTransactionAsync();
            try
            {
                if (await _userManager.FindByEmailAsync(user.Email) != null)
                    return "EmailIsExist";
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                    return "UserNameIsExist";
                var createdUser = await _userManager.CreateAsync(user, password);
                if (createdUser == null)
                    return string.Join(',', createdUser.Errors.Select(e => e.Description).ToList());
                var userRole = await _userManager.AddToRoleAsync(user, "User");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnURL = "To confirm email click this link : \n"
                    + requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code })
                    + "\n thanks for subscribe with us.";
                // + $"/Api/V1/Authentication/Confirm-Email?userId={user.Id}&code={code}";
                await _emailService.SendEmailAsync(user.Email, returnURL, "Confirm Email");
                await trnsact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trnsact.RollbackAsync();
                return "Failed";
            }
        }
    }
}
