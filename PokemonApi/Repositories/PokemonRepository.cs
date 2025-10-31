using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;
using System.Linq.Dynamic.Core;

namespace PokemonApi.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly RelationalDbContext _context;

    public PokemonRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public async Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Update(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Remove(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Pokemon> Data, int TotalRecords)> GetPokemonsByNameAsync(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
{
    var query = _context.Pokemons.AsNoTracking().AsQueryable();

    if (!string.IsNullOrWhiteSpace(name))
        query = query.Where(p => p.Name.Contains(name));

    if (!string.IsNullOrWhiteSpace(type))
        query = query.Where(p => p.Type == type);

    var totalRecords = await query.CountAsync(cancellationToken);

    query = (orderBy?.ToLower(), orderDirection?.ToLower()) switch
    {
        ("name", "desc") => query.OrderByDescending(p => p.Name),
        ("name", _) => query.OrderBy(p => p.Name),

        ("id", "desc") => query.OrderByDescending(p => p.Id),
        ("id", _) => query.OrderBy(p => p.Id),

        _ => query.OrderBy(p => p.Id) // default
    };

    var data = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    return (data.ToModel(), totalRecords);
}

    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        //Selectr * from pokemons where id is = "asdasd"
        var pokemon = await _context.Pokemons.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return pokemon.ToModel();
    }

    public async Task<Pokemon> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        //select * from pokemons where name like '%TEXTO%'
        var pokemon = await _context.Pokemons.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Contains(name));
        return pokemon.ToModel();
    }

    public async Task<Pokemon> CreateAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var pokemonToCreate = pokemon.ToEntity();
        pokemonToCreate.Id = Guid.NewGuid();
        await _context.Pokemons.AddAsync(pokemonToCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return pokemonToCreate.ToModel();
    }
}