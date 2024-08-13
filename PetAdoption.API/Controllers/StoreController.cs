using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Application.Interfaces;

namespace PetAdoption.API.Controllers;

[ApiController]
[Route("api/stores")]
public class StoreController : ControllerBase
{
    private readonly IStoreService _service;

    public StoreController(IStoreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStores()
    {
        var stores = await _service.GetAll();
        var responses = new CustomResponse<List<StoreResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = stores
        };
        return Ok(responses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreById(string id)
    {
        var store = await _service.FindStoreById(id);
        var response = new CustomResponse<StoreResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get Data Successfully",
            Data = store
        };
        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateStore([FromBody] StoreRequest request)
    {
        var store = await _service.Update(request);
        var response = new CustomResponse<StoreResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Updated Data Successfully",
            Data = store
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(string id)
    {
        await _service.DeleteById(id);
        var response = new CustomResponse<string>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Deleted Data Successfully",
            Data = id
        };
        return Ok(response);
    }
}
