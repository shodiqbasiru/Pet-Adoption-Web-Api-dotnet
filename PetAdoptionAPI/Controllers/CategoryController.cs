using System.Net;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Services;

namespace PetAdoptionAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{

    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
    {
        var category = await _categoryService.Create(request);
        CustomResponse<Category> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Create new data successfully",
            Data = category
        };

        return Created("/api/categories", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
        var categories = await _categoryService.GetAllCategory();
        CustomResponse<IEnumerable<CategoryResponse>> responses = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get All Data Successfully",
            Data = categories
        };

        return Ok(responses);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(string categoryId)
    {
        var category = await _categoryService.GetById(categoryId);
        CustomResponse<Category> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Get Data Successfully",
            Data = category
        };
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest request)
    {
        var category = await _categoryService.UpdateCategory(request);
        CustomResponse<Category> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Update Data Successfully",
            Data = category
        };
        return Ok(response);
    }


    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(string categoryId)
    {
        await _categoryService.DeleteById(categoryId);
        CustomResponse<Category> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Delete Data Successfully",
        };
        return Ok(response);
    }

}