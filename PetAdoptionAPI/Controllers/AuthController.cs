using System.Net;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Services;

namespace PetAdoptionAPI.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterRequest request)
    {
        var customer = await _authService.RegisterCustomer(request);
        var response = new CustomResponse<RegisterResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Register Customer Successfully",
            Data = customer
        };
        
        return Created("/api/auth/register", response);
    }

    [HttpPost("register-seller")]
    public async Task<IActionResult> RegisterSeller([FromBody] RegisterSellerRequest request)
    {
        var seller = await _authService.RegisterSeller(request);
        var response = new CustomResponse<RegisterSellerResponse>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Register Seller Successfully",
            Data = seller
        };
        
        return Created("/api/auth/register-seller", response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var customer = await _authService.Login(request);
        var response = new CustomResponse<LoginResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Login Successfully",
            Data = customer
        };
        
        return Ok(response);
    }
}