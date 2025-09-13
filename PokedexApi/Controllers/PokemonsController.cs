using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Expections;
using PokedexApi.Mappers;
using PokedexApi.Services;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase // Nos va a dar status code verbos HTTP etc.
{
    private readonly IPokemonService _pokemonService;

    public PokemonsController(IPokemonService pokemonsService)
    {
        _pokemonService = pokemonsService;
    }

    // localhost:PORT/api/v1/pokemons/ID(<- Es el ID del recurso)
    [HttpGet("{id}", Name = "GetPokemonByIdAsync")] // Es un endopoint del controlador y escucha un metodo Get, {id} es el recurso que va a viajar en la peticion
    public async Task<ActionResult<PokemonResponse>> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        // HTTP verb - GET
        return pokemon is null ? NotFound() : Ok(pokemon.ToResponse()); // Regresa un status code en caso de que existe el pokemon
        // En caso de que no existe se regresa un 404 (NotFound)
    }


    // localhost:port/api/v1/pokemons
    // Body request - JSON
    // HTTP verb POST
    // HTTP Status
    // 400 Bad request (Si el usuario manda informacion incorrecta)
    // 409 Conlict (Ya existe esa entidad)
    // 422 Entidad no procesable (Por alguna regla de negocio interna)
    // 500 Internal Error
    // 200 - Ok (El recurso creado + id) -- no sigue bien las buenas practicas de RESTFUL
    // 201 - Created (Recurso creado + id) -- Response header retorna un href donde referencia al GET para obtener el recurso
    // 202 - Accepted (Procesamiento async)
    [HttpPost]
    public async Task<ActionResult<PokemonResponse>> CreatePokemonAsync([FromBody] CreatePokemonRequest createPokemon, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsValidAttack(createPokemon))
            {
                // {"message"} : ""
                return BadRequest(new { Message = "Attack does not have a valid value" });
            }

            var pokemon = await _pokemonService.CreatePokemonAsync(createPokemon.ToModel(), cancellationToken);

            // 201
            return CreatedAtRoute(nameof(GetPokemonByIdAsync), new { id = pokemon.Id }, pokemon.ToResponse());
        }
        catch(PokemonAlreadyExistsException e)
        {
            // 409 Conflict - JSON {"Message": "......"}
            return Conflict(new {Message = e.Message});
        }
        
    }

    private static bool IsValidAttack(CreatePokemonRequest createPokemon)
    {
        return createPokemon.Stats.Attack > 0;
    }
}