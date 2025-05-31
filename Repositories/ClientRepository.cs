using DatabaseFirstAproach.Models;
using DatabaseFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstAproach.Repositories;

public class ClientRepository: IClientRepository
{
    private Apbd31052025Context context;

    public ClientRepository(Apbd31052025Context context)
    {
        this.context = context;
    }

    public async Task<List<Client>> get_clients_by_tripID(int tripID, CancellationToken cancellationToken)
    {
        return await context.Clients
            .Where(c => c.ClientTrips.Any(ct => ct.IdTrip == tripID))
            .ToListAsync(cancellationToken);
    }
}