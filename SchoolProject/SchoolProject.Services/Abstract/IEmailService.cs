namespace SchoolProject.Services.Abstract
{
    public interface IEmailService
    {
        public Task<string> SendEmailAsync(string email, string message, string reason);
    }
}
