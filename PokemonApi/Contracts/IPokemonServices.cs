using System.ServiceModel;
using PokemonApi.Dtos;

namespace PokemonApi.Services;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonServices
{
    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponse> GetPokemonsByName(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken);

    [OperationContract]
    Task<DeletePokemonResponseDto> DeletePokemon(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken);
}