using DatabaseFirstAproach.Models;

namespace DatabaseFirstAproach.Repositories.abstractions;

public interface ICountryRepository
{
    public Task<List<Country>> get_countries_by_tripID(int tripID, CancellationToken cancellationToken);
}