using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailService _emailService;

        public EmailsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IEmailService emailService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var sendEmail = await _emailService.SendEmailAsync(request.Email, request.Message, request.Subject);
            if (sendEmail == "Success")
                return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToSendEmail]);
        }
    }
}
