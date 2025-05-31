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
    public async Task<bool> does_client_exist(string Pesel, CancellationToken cancellationToken)
    {
        return await context.Clients
            .AnyAsync(c => c.Pesel == Pesel, cancellationToken);
    }
    public async Task<bool> is_client_registered(string Pesel, CancellationToken cancellationToken)
    {
        var client = await context.Clients
            .Where(c => c.Pesel == Pesel)
            .FirstOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            return false;
        }
        
        bool registered = await context.ClientTrips
            .AnyAsync(ct => ct.IdClient == client.IdClient, cancellationToken);

        return registered;
    }

    public async Task<int> create_client(Client client, CancellationToken cancellationToken)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync(cancellationToken);
        return client.IdClient;
    }

    public async Task<bool> CreateClientTrip(ClientTrip clientTrip, CancellationToken cancellationToken)
    {
        context.ClientTrips.Add(clientTrip);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}