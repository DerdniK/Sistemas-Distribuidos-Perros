namespace GRPCAnime.Infrastructure;

public class CouchbaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string BucketName { get; set; } = null!;
}