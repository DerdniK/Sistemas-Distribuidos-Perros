using System.ServiceModel;
using PokemonApi.Dtos;

namespace PokemonApi.Services;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonServices
{
    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken);

}