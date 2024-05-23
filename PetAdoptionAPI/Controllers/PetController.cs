using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Services;

namespace PetAdoptionAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/pets")]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;

    public PetController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPet([FromBody] Pet payload)
    {
        var pet = await _petService.Create(payload);
        var response = new CustomResponse<Pet>
        {
            StatusCode = (int) HttpStatusCode.Created,
            Message = "Created Data Successfully",
            Data = pet,
        };
        return Created("/api/pets", response);
    }    

    [HttpGet]
    public async Task<IActionResult> GetAllPet()
    {
        var pets = await _petService.GetAll();
        var response = new CustomResponse<List<Pet>>
        {
            StatusCode = (int) HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = pets,
        };
        return Ok(response);
    }
}