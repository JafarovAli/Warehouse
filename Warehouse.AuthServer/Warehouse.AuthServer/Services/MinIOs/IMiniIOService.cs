using Minio;

namespace Warehouse.AuthServer.Services.MiniIOs
{
    public interface IMiniIOService
    {
        Task<string> GetPresignedDownloadUrl(string objectName, int expiry);
        Task<PutObjectResponse> PutObject(string objectName, MemoryStream zipStream);
    }
}