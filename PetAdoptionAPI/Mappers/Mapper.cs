using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Mappers;

public static class ProductMapper
{
    public static ProductResponse ConvertToProductResponse(this Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Price = product.Price,
            Rating = product.Rating,
            Stock = product.Stock,
            Description = product.Description,
            Url = product.Url,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static List<ProductResponse> ConvertToProductResponses(this List<Product> products)
    {

        return products.Select(product => product.ConvertToProductResponse()).ToList();
    }
}