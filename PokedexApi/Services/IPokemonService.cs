using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Services;

public interface IPokemonService
{
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
    Task<PokemonResponse> GetPokemonsByName(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken);
    Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken);
    Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken);
}