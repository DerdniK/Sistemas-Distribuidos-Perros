using PokedexApi.Models;

namespace PokedexApi.Gateways;
public interface ITrainerGateway
{
    Task<Trainer> GetTrainerById(string id, CancellationToken cancellationToken);
    IAsyncEnumerable<Trainer> GetTrainersByName(string name, CancellationToken cancellationToken);
    Task DeleteTrainerAsync(string id, CancellationToken cancellationToken);
    Task UpdateTrainerAsync(Trainer trainer, CancellationToken cancellationToken);
    Task<(int SuccessCount, IList<Trainer> CreatedTrainers)> CreateTrainersAsync(IEnumerable<Trainer> trainers, CancellationToken cancellationToken);
}