using PokedexApi.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Gateways;

public interface IPokemonGateway
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PokemonResponse> GetPokemonsByNameAsync(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken);
    Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken);
    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
    Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken);
    Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
}