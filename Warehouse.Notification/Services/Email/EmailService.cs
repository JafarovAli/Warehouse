using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Warehouse.Notification.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public MimeMessage BuildDownloadReadyMessage(IEnumerable<string> emails, string fullName, string downloadUrl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Warehouse", configuration["EmailSettings:From"]));
            foreach (var email in emails)
            {
                message.To.Add(new MailboxAddress("", email));
            }
            message.Subject = "Your Download Link";

            message.Body = new TextPart("plain")
            {
                Text = $"Hello {fullName},\n\nYour download link is ready: {downloadUrl}\n\nBest regards,\nWarehouse Team"
            };

            return message;
        }

        public async Task SendMessageAsync(MimeMessage message, CancellationToken cancellationToken)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(configuration["EmailSettings:SmtpServer"], int.Parse(configuration["EmailSettings:Port"]), false, cancellationToken);
            await client.AuthenticateAsync(configuration["EmailSettings:Username"], configuration["EmailSettings:Password"], cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
