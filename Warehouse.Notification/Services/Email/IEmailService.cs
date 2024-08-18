using MimeKit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Warehouse.Notification.Services.Email
{
    public interface IEmailService
    {
        MimeMessage BuildDownloadReadyMessage(IEnumerable<string> emails, string fullName, string downloadUrl);
        Task SendMessageAsync(MimeMessage message, CancellationToken cancellationToken);
    }
}
