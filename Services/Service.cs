using DatabaseFirstAproach.contracts.request;
using DatabaseFirstAproach.contracts.response;
using DatabaseFirstAproach.errors;
using DatabaseFirstAproach.Models;
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

    public async Task<bool> Assign_client_to_trip(clientRequestDTO clientRequestDto, int idTrip, CancellationToken cancellationToken)
    {
        var check1 = await clientRepository.does_client_exist(clientRequestDto.Pesel, cancellationToken);
        var check2 = await clientRepository.is_client_registered(clientRequestDto.Pesel, cancellationToken);
        var check3 = !(await tripRepository.does_trip_exist(idTrip, cancellationToken));
        var check4 = !(await tripRepository.is_trip_realized(idTrip, cancellationToken));
        if (check1)throw new ClientAlreadyExistsException();
        if (check2)throw new ClientAssignedToTripException();
        if (check3)throw new TripDoesntExistException();
        if (check4)throw new TripAlreadyDoneException();
        Client client = new Client()
        {
            FirstName = clientRequestDto.FirstName,
            LastName = clientRequestDto.LastName,
            Email = clientRequestDto.Email,
            Telephone = clientRequestDto.Telephone,
            Pesel = clientRequestDto.Pesel
        };
        int id = await clientRepository.create_client(client, cancellationToken);
        ClientTrip clientTrip = new ClientTrip()
        {
            IdClient = id,
            IdTrip = clientRequestDto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientRequestDto.PaymentDate,
        };
        if (!(await clientRepository.CreateClientTrip(clientTrip, cancellationToken)))
        {
            return false;
        }
        return true;
    }

    public async Task<bool> delete_client(int id, CancellationToken cancellationToken)
    {
        if (await clientRepository.is_client_registered(id, cancellationToken))throw new ClientAssignedToTripException();
        return await clientRepository.delete_client(id, cancellationToken);
    }
}