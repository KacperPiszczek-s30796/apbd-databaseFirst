using DatabaseFirstAproach.contracts.response;

namespace DatabaseFirstAproach.Services.abstractions;

public interface IService
{
    public Task<tripsResponseDTO> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken);
}