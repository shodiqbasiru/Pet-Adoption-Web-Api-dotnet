using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Mappers;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _uow;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;

    public ReviewService(IUnitOfWork uow, IProductService productService, ICustomerService customerService)
    {
        _uow = uow;
        _productService = productService;
        _customerService = customerService;
    }

    public async Task<ReviewResponse> CreateNewReview(ReviewRequest request)
    {
        var product = await _productService.FindById(request.ProductId);
        var customer = await _customerService.FindById(request.CustomerId);

        var review = await _uow.ExecuteTransactionAsync<Review>(async () =>
        {
            var payload = new Review
            {
                Comment = request.Comment,
                Rating = request.Rating,
                ProductId = product.Id,
                CustomerId = customer.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var review = await _uow.Repository<Review>().SaveAsync(payload);
            product.Rating = product.Rating == 0 ? request.Rating : (product.Rating + request.Rating) / 2;

            await _uow.SaveChangesAsync();
            return review;
        });

        return review!.ConvertToReviewResponse();
    }

    public async Task<Review> GetById(string id)
    {
        if (!Guid.TryParse(id, out var reviewId)) throw new NotFoundException("review not found");
        return await _uow.Repository<Review>().FindByIdAsync(reviewId) ?? throw new NotFoundException("review not found");
    }

    public async Task<ReviewResponse> GetReviewById(string id)
    {
        var review = await GetById(id);
        return review.ConvertToReviewResponse();
    }

    public async Task<List<ReviewResponse>> GetAllReview()
    {
        var reviews = await _uow.Repository<Review>().FindAllAsync(new string[] { "Product", "Customer" });
        return reviews.ConvertToReviewResponses();
    }

    public async Task<ReviewResponse> UpdateReview(ReviewRequest request)
    {
        var currentReview = await GetById(request.Id ?? throw new BadRequestException("review id is required"));
        currentReview.Comment = request.Comment;
        currentReview.Rating = request.Rating;
        currentReview.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync();
        return currentReview.ConvertToReviewResponse();
    }
}
