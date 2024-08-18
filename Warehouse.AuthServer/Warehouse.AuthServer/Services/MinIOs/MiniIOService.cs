using Microsoft.Extensions.Options;
using Minio;
using Warehouse.AuthServer.Settings;

namespace Warehouse.AuthServer.Services.MiniIOs;

public class MiniIOService : IMiniIOService
{
    private readonly IMinioClient minioClient;
    private readonly MinioSettings minioSettings;
    private readonly string bucketName;

    public MiniIOService(IMinioClient minioClient,
                         MinioSettings minioSettings,
                         string bucketName)
    {
        this.minioClient = minioClient;
        this.minioSettings = minioSettings;
        this.bucketName = bucketName;
    }


    public MiniIOService(IOptions<MinioSettings> options)
    {
        minioSettings = options.Value;
        minioClient = new MinioClient()
            .WithEndpoint(minioSettings.Endpoint)
            .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
            .Build();
        this.bucketName = minioSettings.BucketName;
    }

    public async Task<PutObjectResponse> PutObject(string objectName, MemoryStream zipStream)
    {
        if (string.IsNullOrEmpty(objectName))
        {
            throw new ArgumentNullException("Object name cannot be empty or null.");
        }

        bool found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!found)
        {
            await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }

        var response = await minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(zipStream)
            .WithObjectSize(zipStream.Length));

        return response;
    }

    public async Task<string> GetPresignedDownloadUrl(string objectName, int expiry)
    {
        if (string.IsNullOrEmpty(objectName))
        {
            throw new ArgumentNullException(nameof(objectName), "Object name cannot be empty or null.");
        }

        var url = await minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry(expiry)
        );
        return url;
    }
}