using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IReviewService
{
    Task<ReviewResponse> CreateNewReview(ReviewRequest request);
    Task<Review> GetById(string id);
    Task<ReviewResponse> GetReviewById(string id);
    Task<List<ReviewResponse>> GetAllReview();
    Task<ReviewResponse> UpdateReview(ReviewRequest request);
}
