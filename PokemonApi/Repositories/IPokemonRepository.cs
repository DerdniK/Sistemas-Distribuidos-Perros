using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PokemonApi.Models;

namespace PokemonApi.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<Pokemon> CreateAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Pokemon> Data, int TotalRecords)> GetPokemonsByNameAsync(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken);

    Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);

    Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
}