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
    public async Task<bool> does_trip_exist(int id, CancellationToken cancellationToken)
    {
        return await context.Trips
            .AnyAsync(c => c.IdTrip == id, cancellationToken);
    }
    public async Task<bool> is_trip_realized(int id, CancellationToken cancellationToken)
    {
        var trip = await context.Trips
            .Where(t => t.IdTrip == id)
            .Select(t => t.DateFrom)
            .FirstOrDefaultAsync(cancellationToken);

        if (trip == default)
        {
            return true;
        }

        return trip <= DateTime.Now;
    }
}