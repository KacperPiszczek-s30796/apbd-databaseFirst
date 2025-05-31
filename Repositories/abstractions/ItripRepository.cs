using DatabaseFirstAproach.Models;

namespace DatabaseFirstAproach.Repositories.abstractions;

public interface ItripRepository
{
    public Task<(List<Trip> trips, int totalCount)> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);
}