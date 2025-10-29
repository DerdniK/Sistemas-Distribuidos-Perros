using Couchbase;
using Couchbase.KeyValue;
using Grpc.Core;

namespace GRPCAnime.Services;

public class AnimeService : GRPCAnime.AnimeService.AnimeServiceBase
{
    private readonly ICouchbaseCollection _collection;

    public AnimeService(ICouchbaseCollection collection)
    {
        _collection = collection;
    }

    public override async Task<CreateAnimeResponse> CreateAnime(IAsyncStreamReader<CreateAnimeRequest> requestStream, ServerCallContext context)
    {
        int successCount = 0;
        var createdAnimes = new List<AnimeResponse>();

        await foreach (var req in requestStream.ReadAllAsync())
        {
            var id = Guid.NewGuid().ToString();

            var animeDoc = new
            {
                id = id,
                name = req.Name,
                author = req.Author,
                genre = req.Genre,
                first_broadcast = req.FirstBroadcast.ToDateTime(),
                last_broadcast = req.LastBroadcast.ToDateTime(),
                has_manga = req.HasManga,
                created_at = DateTime.UtcNow
            };

            await _collection.InsertAsync(id, animeDoc);

            createdAnimes.Add(new AnimeResponse
            {
                Id = id,
                Name = req.Name,
                Author = req.Author,
                Genre = req.Genre,
                LastBroadcast = req.LastBroadcast
            });

            successCount++;
        }

        return new CreateAnimeResponse
        {
            SuccessCount = successCount,
            Animes = { createdAnimes }
        };
    }
}
