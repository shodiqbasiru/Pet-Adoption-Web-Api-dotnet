using Microsoft.AspNetCore.Mvc;
using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewReview([FromBody] ReviewRequest request)
    {
        var review = await _service.CreateNewReview(request);
        var response = new CustomResponse<ReviewResponse>
        {
            StatusCode = StatusCodes.Status201Created,
            Message = "Review Created Successfully",
            Data = review
        };
        return Created("/api/reviews", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _service.GetAllReview();
        var response = new CustomResponse<List<ReviewResponse>>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Get All Reviews Successfully",
            Data = reviews
        };
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReview([FromBody] ReviewRequest request)
    {
        var review = await _service.UpdateReview(request);
        var response = new CustomResponse<ReviewResponse>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Review Updated Successfully",
            Data = review
        };
        return Ok(response);
    }
}
