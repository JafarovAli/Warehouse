using Warehouse.AuthServer.Enums;

namespace Warehouse.AuthServer.Models.DTOs
{
    public class UsersDownloadAllDataDTO
    {
        public string UserId { get; set;}
        public DownloadType DownloadType { get; set;}

        public UsersDownloadAllDataDTO(string userId, DownloadType downloadType)
        {
            UserId = userId;
            DownloadType = downloadType;
        }

    }
}
