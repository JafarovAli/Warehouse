using MassTransit;
using System.IO.Compression;
using Warehouse.AuthServer.Enums;
using Warehouse.AuthServer.Models.DTOs;
using Warehouse.AuthServer.Services.MiniIOs;
using Warehouse.AuthServer.Services.Users;
using Warehouse.CommonLibrary.DTOs;

namespace Warehouse.AuthServer.Jobs
{
    public class UsersDownloadAllDataConsumer : IConsumer<UsersDownloadAllDataDTO>
    {
        public readonly IUserService userService;
        private readonly IMiniIOService miniIOService;
        private readonly IMessageQueue<UserDownloadUrlDTO> messageQueue;
        private readonly ILogger<UsersDownloadAllDataConsumer> logger;
        public UsersDownloadAllDataConsumer(IUserService userService,
                                            IMiniIOService miniIOService,
                                            IMessageQueue<UserDownloadUrlDTO> messageQueue,
                                            ILogger<UsersDownloadAllDataConsumer> logger)
        {
            this.userService = userService;
            this.miniIOService = miniIOService;
            this.messageQueue = messageQueue;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UsersDownloadAllDataDTO> context)
        {
            var userId = context.Message.UserId;
            var downloadType = context.Message.DownloadType;

            logger.LogInformation($" {userId} user");

            var users = await userService.GetUsersAsync();
            IReadOnlyList<UsersDTO> usersDTOs = users.Select(u => new UsersDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName,
                Password = u.PasswordHash
            }).ToList();
            logger.LogInformation($"Retrieved Users {userId}");

            switch (downloadType)
            {
                case DownloadType.CSV:
                    logger.LogInformation($"Generating CSV for user {userId}");
                    await GenerateCSV(userId, usersDTOs);
                    logger.LogInformation($"CSV generated successfully for user {userId}");
                    break;
                case DownloadType.Excel:
                    logger.LogInformation($"Generating Excel for user {userId}");
                    await GenerateExcel(userId, usersDTOs);
                    logger.LogInformation($"Excel generated successfully for user {userId} ");
                    break;
                default:
                    throw new NotSupportedException($"Unsupported download type: {downloadType}");
            }
        }

        private async Task GenerateCSV(string userId, IReadOnlyList<UsersDTO> users)
        {
            var csvData = await userService.GenerateCSVFromUserAsync(users, null);
            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var entry = archive.CreateEntry($"users_{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
                    using (var entryStream = entry.Open())
                    using (var writer = new StreamWriter(entryStream))
                    {
                        await writer.WriteAsync(File.ReadAllText(csvData));
                    }
                }
                zipStream.Seek(0, SeekOrigin.Begin);

                var zipFileName = $"users_{DateTime.UtcNow:yyyyMMddHHmmss}.zip";
                await PublishFile(userId, zipStream, zipFileName);
            }
        }
        private async Task GenerateExcel(string userId, IReadOnlyList<UsersDTO> users)
        {
            var excelDataList = new List<byte[]>();
            var maxRowCount = 1000;

            for (int i = 0; i < users.Count; i += maxRowCount)
            {
                var subset = users.Skip(i).Take(maxRowCount).ToList();
                var excelData = await userService.GenerateExcelFromUserAsync(subset,null);
                excelDataList.Add(excelData);
            }

            logger.LogInformation($"Generated {excelDataList.Count} Excel file(s) containing user data");

            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < excelDataList.Count; i++)
                    {
                        var entry = archive.CreateEntry($"user_part_{i + 1}.xlsx");
                        using (var entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(excelDataList[i], 0, excelDataList[i].Length);
                        }
                    }
                }
                zipStream.Seek(0, SeekOrigin.Begin);

                var zipFileName = $"user_{DateTime.UtcNow:yyyyMMddHHmmss}.zip";
                await PublishFile(userId, zipStream, zipFileName);
            }
        }


        private async Task PublishFile(string userId, MemoryStream zipStream, string fileName)
        {
            await miniIOService.PutObject(fileName, zipStream);
            logger.LogInformation($"Uploaded ZIP file to MiniIO: {fileName}");

            var downloadUrl = await miniIOService.GetPresignedDownloadUrl(fileName, 604800);
            logger.LogInformation($"Generated download URL for user {fileName}: {downloadUrl}");

            await messageQueue.Enqueue(new UserDownloadUrlDTO(userId, downloadUrl));
            logger.LogInformation($"Published download URL {downloadUrl} for user {userId}");
        }
    }
}