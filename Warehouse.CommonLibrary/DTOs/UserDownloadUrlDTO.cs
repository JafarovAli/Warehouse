namespace Warehouse.CommonLibrary.DTOs
{
    public class UserDownloadUrlDTO
    {
        public string UserId { get; set; }
        public string DownloadUrl { get; set; }

        public UserDownloadUrlDTO(string userId, string downloadUrl)
        {
            UserId = userId;
            DownloadUrl = downloadUrl;
        }
    }
}
