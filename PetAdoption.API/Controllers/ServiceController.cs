using System.Net;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.API.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IServicesService _servicesService;

    public ServiceController(IServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewService([FromBody] ServiceRequest request)
    {
        var service = await _servicesService.CreateNewService(request);
        CustomResponse<ServiceResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Service created successfully",
            Data = service
        };
        return Created("/api/services", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindServiceById(string id)
    {
        var service = await _servicesService.FindServiceById(id);
        CustomResponse<ServiceResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Service found",
            Data = service
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> FindAllService()
    {
        var services = await _servicesService.FindAllService();
        CustomResponse<List<ServiceResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Services found",
            Data = services
        };
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateService(ServiceRequest request)
    {
        var service = await _servicesService.UpdateService(request);
        CustomResponse<ServiceResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Service updated successfully",
            Data = service
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(string id)
    {
        await _servicesService.DeleteService(id);
        CustomResponse<Service> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Service deleted successfully",
        };
        return Ok(response);
    }
}
