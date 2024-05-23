using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Entities;
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

    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer([FromBody] Customer payload)
    {
        var customer = await _service.Create(payload);
        var response = new CustomResponse<Customer>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Created Data Successfully",
            Data = customer
        };
        return Created("/api/customers", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _service.GetAll();
        var responses = new CustomResponse<List<Customer>>
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
        var customer = await _service.GetById(id);
        var response = new CustomResponse<Customer>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get Data Successfully",
            Data = customer
        };
        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer payload)
    {
        var customer = await _service.Update(payload);
        var response = new CustomResponse<Customer>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = customer
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        await _service.DeleteById(id);
        var response = new CustomResponse<string?>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Deleted Data Successfully",
            Data = null
        };
        return Ok(response);
    }
}