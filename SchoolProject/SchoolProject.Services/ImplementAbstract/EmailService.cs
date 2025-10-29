using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helper;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Services.ImplementAbstract
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<string> SendEmailAsync(string email, string message, string reason)
        {
            try
            {


                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $" {message}",
                        TextBody = "Wellcome"
                    };
                    var Message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    Message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    Message.To.Add(new MailboxAddress("Testina", email));
                    Message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(Message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}
