using DatabaseFirstAproach.contracts.request;
using DatabaseFirstAproach.contracts.response;
using DatabaseFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirstAproach.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api")]
public class Controller: ControllerBase
{
    private IService service;

    public Controller(IService service)
    {
        this.service = service;
    }
    [HttpGet("/trips")]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken token = default)
    {
        var result = await service.GetTripsAsync(page, pageSize, token);
        return Ok(result);
    }
    [HttpPost("/trips/{idTrip}/clients")]
    public async Task<IActionResult> GetTrip([FromBody] clientRequestDTO clientRequestDto, int idTrip, CancellationToken token = default)
    {
        var result = await service.Assign_client_to_trip(clientRequestDto, idTrip, token);
        return Ok(result);
    }
    
}