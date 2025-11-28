using Couchbase;
using Couchbase.KeyValue;
using GRPCAnime.Infrastructure;
using GRPCAnime.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Bind config
builder.Services.Configure<CouchbaseSettings>(
    builder.Configuration.GetSection("Couchbase"));

//Couchbase Cluster
builder.Services.AddSingleton<ICluster>(sp =>
{
    var options = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
    return Cluster.ConnectAsync(options.ConnectionString, options.Username, options.Password).GetAwaiter().GetResult();
});

// (espera que el bucket este listo)
builder.Services.AddHostedService<CouchbaseInitializer>();

builder.Services.AddSingleton<ICluster>(sp =>
{
    var options = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
    var cluster = Cluster.ConnectAsync(options.ConnectionString, options.Username, options.Password).GetAwaiter().GetResult();

    // Espera a que el cluster este listo
    cluster.WaitUntilReadyAsync(TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
    return cluster;
});

builder.Services.AddSingleton<IBucket>(sp =>
{
    var cluster = sp.GetRequiredService<ICluster>();
    var bucketName = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value.BucketName;
    var bucket = cluster.BucketAsync(bucketName).GetAwaiter().GetResult();

    // Espera a el bucket
    bucket.WaitUntilReadyAsync(TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
    return bucket;
});

builder.Services.AddSingleton<ICouchbaseCollection>(sp =>
{
    var bucket = sp.GetRequiredService<IBucket>();
    return bucket.DefaultCollection();
});


// Add gRPC
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AnimeService>();
app.MapGet("/", () => "Use gRPC client to connect.");

app.Run();
