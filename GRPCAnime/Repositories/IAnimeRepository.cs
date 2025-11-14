using GRPCAnime.Models;

namespace GRPCAnime.Repositories;

public interface IAnimeRepository
{
    Task<AnimeModel> CreateAnime(AnimeModel anime, CancellationToken cancellationToken);
}