using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InsideAirbnb.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation("Sent email with body: \n {message} \n and subject: \n {subject}", message, subject);
            return Task.CompletedTask;
        }
    }
}
