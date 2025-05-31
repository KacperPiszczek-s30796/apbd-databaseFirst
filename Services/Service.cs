using DatabaseFirstAproach.contracts.response;
using DatabaseFirstAproach.Repositories.abstractions;
using DatabaseFirstAproach.Services.abstractions;

namespace DatabaseFirstAproach.Services;

public class Service: IService
{
    private IClientRepository clientRepository;
    private ICountryRepository countryRepository;
    private ItripRepository tripRepository;

    public Service(IClientRepository clientRepository, ICountryRepository countryRepository,
        ItripRepository tripRepository)
    {
        this.clientRepository = clientRepository;
        this.countryRepository = countryRepository;
        this.tripRepository = tripRepository;
    }

    public async Task<tripsResponseDTO> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var (trips, totalCount) = await tripRepository.GetTripsAsync(page, pageSize, cancellationToken);

        var tripDTOs = new List<tripDTO>();

        foreach (var trip in trips)
        {
            var clients = await clientRepository.get_clients_by_tripID(trip.IdTrip, cancellationToken);
            var countries = await countryRepository.get_countries_by_tripID(trip.IdTrip, cancellationToken);

            var tripDTO = new tripDTO
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Clients = clients.Select(c => new ClientDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).ToList(),
                Countries = countries.Select(c => new CountryDTO
                {
                    Name = c.Name
                }).ToList()
            };

            tripDTOs.Add(tripDTO);
        }

        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new tripsResponseDTO
        {
            pageNum = page,
            pageSize = pageSize,
            allPages = totalPages,
            trips = tripDTOs
        };
    }
}