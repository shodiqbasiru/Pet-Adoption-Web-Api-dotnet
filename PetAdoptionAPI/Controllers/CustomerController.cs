using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Services;

namespace PetAdoptionAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _service.GetAll();
        var responses = new CustomResponse<List<CustomerResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = customers
        };
        return Ok(responses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        var customer = await _service.FindCustomerById(Guid.Parse(id));
        var response = new CustomResponse<CustomerResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get Data Successfully",
            Data = customer
        };
        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateRequest request)
    {
        var customer = await _service.Update(request);
        var response = new CustomResponse<CustomerResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Updated Data Successfully",
            Data = customer
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        await _service.DeleteById(Guid.Parse(id));
        var response = new CustomResponse<string>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Deleted Data Successfully",
        };
        return Ok(response);
    }
}