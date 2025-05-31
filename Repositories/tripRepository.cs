using DatabaseFirstAproach.Models;
using DatabaseFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstAproach.Repositories;

public class tripRepository: ItripRepository
{
    private Apbd31052025Context context;

    public tripRepository(Apbd31052025Context context)
    {
        this.context = context;
    }

    public async Task<(List<Trip> trips, int totalCount)> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = context.Trips
            .OrderByDescending(t => t.DateFrom);

        int totalCount = await query.CountAsync(cancellationToken);

        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (trips, totalCount);
    }
}