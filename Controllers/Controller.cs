using DatabaseFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirstAproach.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller: ControllerBase
{
    private IService service;

    public Controller(IService service)
    {
        this.service = service;
    }
    
}