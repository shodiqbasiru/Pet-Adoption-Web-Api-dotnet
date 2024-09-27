using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.API.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] OrderRequest request)
    {
        OrderResponse order = await _orderService.CreateTransaction(request);
        return Created("/api/orders", order);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransaction()
    {
        var purchases = await _orderService.GetAllTransaction();
        var responses = new CustomResponse<List<OrderResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = purchases
        };
        return Ok(responses);
    }
}