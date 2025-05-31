using DatabaseFirstAproach.Models;
using DatabaseFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstAproach.Repositories;

public class CountryRepository: ICountryRepository
{
    private Apbd31052025Context context;

    public CountryRepository(Apbd31052025Context context)
    {
        this.context = context;
    }
    public async Task<List<Country>> get_countries_by_tripID(int tripID, CancellationToken cancellationToken)
    {
        var trip = await context.Trips
            .Include(t => t.IdCountries)
            .FirstOrDefaultAsync(t => t.IdTrip == tripID, cancellationToken);

        return trip?.IdCountries.ToList() ?? new List<Country>();
    }
}