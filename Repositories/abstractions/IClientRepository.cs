using DatabaseFirstAproach.Models;

namespace DatabaseFirstAproach.Repositories.abstractions;

public interface IClientRepository
{
    public Task<List<Client>> get_clients_by_tripID(int tripID, CancellationToken cancellationToken);
}