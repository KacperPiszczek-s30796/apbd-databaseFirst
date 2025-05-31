using DatabaseFirstAproach.Models;

namespace DatabaseFirstAproach.Repositories.abstractions;

public interface ItripRepository
{
    public Task<(List<Trip> trips, int totalCount)> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);
    public Task<bool> does_trip_exist(int id, CancellationToken cancellationToken);
    public Task<bool> is_trip_realized(int id, CancellationToken cancellationToken);
}