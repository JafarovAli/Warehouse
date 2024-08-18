using MassTransit;
using Warehouse.CommonLibrary.DTOs;
using Warehouse.Notification.Services.Caches;
using Warehouse.Notification.Services.Email;

namespace Warehouse.Notification.Jobs
{
    public class UserDownloadUrlConsumer : IConsumer<UserDownloadUrlDTO>
    {
        private readonly IEmailService smtpEmailService;
        private readonly ICacheService cacheService;
        private readonly ILogger<UserDownloadUrlConsumer> logger;

        public UserDownloadUrlConsumer(IEmailService emailSenderService,
                                          ICacheService cacheService,
                                          ILogger<UserDownloadUrlConsumer> logger)
        {
            this.smtpEmailService = emailSenderService;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UserDownloadUrlDTO> context)
        {
            var message = context.Message;

            if (string.IsNullOrWhiteSpace(message.UserId))
            {
                logger.LogWarning("Invalid userId received in message. Skipping processing.");
                return;
            }

            if (!Uri.TryCreate(message.DownloadUrl, UriKind.Absolute, out _))
            {
                logger.LogWarning($"Invalid download URL received for user {message.UserId}. Skipping processing.");
                return;
            }

            string fullName = await cacheService.GetUserFullNameAsync(message.UserId);

            logger.LogInformation($"Received download URL for user {message.UserId}: {message.DownloadUrl}");

            string email = await cacheService.GetEmailAsync(message.UserId);

            var emails = new List<string> { email };
            var emailMessage = smtpEmailService.BuildDownloadReadyMessage(
                emails,
                fullName,
                message.DownloadUrl);

            await smtpEmailService.SendMessageAsync(
                emailMessage,
                default(CancellationToken));

            logger.LogInformation($"Download URL email sent to user {message.UserId}.");
        }
    }
}
