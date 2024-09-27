using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Core.Mappers;

public static class ReviewMapper
{
    public static ReviewResponse ConvertToReviewResponse(this Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id.ToString(),
            Comment = review.Comment,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt,
            ProductId = review.ProductId.ToString(),
            CustomerId = review.CustomerId.ToString()
        };
    }

    public static List<ReviewResponse> ConvertToReviewResponses(this List<Review> reviews)
    {
        return reviews.Select(review => review.ConvertToReviewResponse()).ToList();
    }
}
