using PokedexApi.Dtos;
using PokedexApi.Models;

namespace PokedexApi.Services;

public interface ITrainerService
{
    Task<Trainer> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Trainer>> GetAllByNameAsync(string name, CancellationToken cancellationToken);
    Task<(int, IEnumerable<Trainer>)> CreateTrainersAsync(IEnumerable<Trainer> trainers, CancellationToken cancellationToken);
    Task DeleteTrainerAsync(string id, CancellationToken cancellationToken);
    Task UpdateTrainerAsync(Trainer trainer, CancellationToken cancellationToken);
}