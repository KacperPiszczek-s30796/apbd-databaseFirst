﻿using DatabaseFirstAproach.Models;

namespace DatabaseFirstAproach.Repositories.abstractions;

public interface IClientRepository
{
    public Task<List<Client>> get_clients_by_tripID(int tripID, CancellationToken cancellationToken);
    public Task<bool> does_client_exist(string Pesel, CancellationToken cancellationToken);
    public Task<bool> is_client_registered(string Pesel, CancellationToken cancellationToken);
    public Task<int> create_client(Client client, CancellationToken cancellationToken);
    public Task<bool> CreateClientTrip(ClientTrip clientTrip, CancellationToken cancellationToken);
    public Task<bool> is_client_registered(int id, CancellationToken cancellationToken);
    public Task<bool> delete_client(int id, CancellationToken cancellationToken);
}