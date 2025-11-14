using Couchbase;
using Couchbase.KeyValue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GRPCAnime.Infrastructure;

public class CouchbaseInitializer : IHostedService
{
    private readonly ICluster _cluster;
    private readonly IOptions<CouchbaseSettings> _settings;

    public CouchbaseInitializer(ICluster cluster, IOptions<CouchbaseSettings> settings)
    {
        _cluster = cluster;
        _settings = settings;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var bucketName = _settings.Value.BucketName;
        IBucket? bucket = null;

        var retries = 10;
        var delay = TimeSpan.FromSeconds(3);

        for (int i = 0; i < retries; i++)
        {
            try
            {
                bucket = await _cluster.BucketAsync(bucketName);
                // Espera hasta el bucket
                await bucket.WaitUntilReadyAsync(TimeSpan.FromSeconds(30));
                break;
            }
            catch
            {
                if (i == retries - 1) throw;
                await Task.Delay(delay, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
