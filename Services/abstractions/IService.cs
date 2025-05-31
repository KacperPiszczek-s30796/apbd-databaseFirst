using DatabaseFirstAproach.contracts.request;
using DatabaseFirstAproach.contracts.response;

namespace DatabaseFirstAproach.Services.abstractions;

public interface IService
{
    public Task<tripsResponseDTO> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);

    public Task<bool> Assign_client_to_trip(clientRequestDTO clientRequestDto, int idTrip, CancellationToken cancellationToken);
}