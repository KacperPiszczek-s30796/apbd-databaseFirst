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
    
}