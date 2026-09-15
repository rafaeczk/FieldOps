namespace FieldOps.Shared.Infrastructure.S3;

public class S3Options
{
    public string ServiceUrl { get; set; } = null!;
    public string AccessKeyId { get; set; } = null!;
    public string SecretAccessKey { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}
