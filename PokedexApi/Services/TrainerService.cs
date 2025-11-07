using PokedexApi.Gateways;
using PokedexApi.Models;

namespace PokedexApi.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerGateway _trainerGateway;

    public TrainerService(ITrainerGateway trainerGateway)
    {
        _trainerGateway = trainerGateway;
    }

    public async Task DeleteTrainerAsync(string id, CancellationToken cancellationToken)
    {
        await _trainerGateway.DeleteTrainerAsync(id, cancellationToken);
    }

    public async Task UpdateTrainerAsync(Trainer trainer, CancellationToken cancellationToken)
    {
        await _trainerGateway.UpdateTrainerAsync(trainer, cancellationToken);
    }

    public async Task<Trainer> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _trainerGateway.GetTrainerById(id, cancellationToken);
    }

    public async Task<(int, IEnumerable<Trainer>)> CreateTrainersAsync(IEnumerable<Trainer> trainers, CancellationToken cancellationToken)
    {
        return await _trainerGateway.CreateTrainersAsync(trainers, cancellationToken);
    }

    public async Task<IEnumerable<Trainer>> GetAllByNameAsync(string name, CancellationToken cancellationToken)
    {
        var trainers = new List<Trainer>();
        await foreach (var trainer in _trainerGateway.GetTrainersByName(name, cancellationToken))
        {
            trainers.Add(trainer);
        }
        return trainers;
    }
}