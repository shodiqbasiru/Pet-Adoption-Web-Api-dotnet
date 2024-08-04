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
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {
        var product = await _productService.Create(request);
        CustomResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Created Data Successfully",
            Data = product,
        };
        return Created("/api/products", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        var products = await _productService.FindAllProduct();
        CustomResponse<List<ProductResponse>> responses = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = products,
        };
        return Ok(responses);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(string productId)
    {
        var product = await _productService.FindProductById(Guid.Parse(productId));
        CustomResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get Data Successfully",
            Data = product,
        };
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductUpdateRequest request)
    {
        var product = await _productService.Update(request);
        CustomResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Update Data Successfully",
            Data = product,
        };
        return Ok(response);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(string productId)
    {
        await _productService.DeleteById(Guid.Parse(productId));
        CustomResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Delete Data Successfully",
        };
        return Ok(response);
    }
}