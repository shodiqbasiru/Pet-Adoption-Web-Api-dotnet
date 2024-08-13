using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.API.Controllers;

[Authorize]
[ApiController]
[Route("api/purchases")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] PurchaseRequest request)
    {
        PurchaseResponse purchase = await _purchaseService.CreateTransaction(request);
        return Created("/api/purchases", purchase);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransaction()
    {
        var purchases = await _purchaseService.GetAllTransaction();
        var responses = new CustomResponse<List<PurchaseResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = purchases
        };
        return Ok(responses);
    }
}