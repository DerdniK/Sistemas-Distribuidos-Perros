using Couchbase;
using Couchbase.KeyValue;
using GRPCAnime.Infrastructure;
using GRPCAnime.Mappers;
using GRPCAnime.Models;
using Microsoft.Extensions.Options;

namespace GRPCAnime.Repositories;

public class AnimeRepository : IAnimeRepository
{
        private readonly ICouchbaseCollection _collection;

        public AnimeRepository(ICluster cluster, IOptions<CouchbaseSettings> settings)
        {
            var bucket = cluster.BucketAsync(settings.Value.BucketName).Result;
            _collection = bucket.DefaultCollection();
        }
        
        public async Task<AnimeModel> CreateAnime(AnimeModel anime, CancellationToken cancellationToken)
        {
            var animeDocument = anime.ToDocument();
            if (string.IsNullOrEmpty(animeDocument.Id))
                animeDocument.Id = Guid.NewGuid().ToString();

            var key = $"anime::{animeDocument.Id}";
            await _collection.UpsertAsync(key, animeDocument);
            return animeDocument.ToDomain();
        }
}