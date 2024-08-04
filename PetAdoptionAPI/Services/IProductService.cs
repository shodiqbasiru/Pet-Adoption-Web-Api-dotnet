using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IProductService
{
    Task<ProductResponse> Create(ProductRequest request);
    Task<Product> FindById(Guid id);
    Task<ProductResponse> FindProductById(Guid id);
    Task<List<ProductResponse>> FindAllProduct();
    Task<ProductResponse> Update(ProductUpdateRequest request);
    Task DeleteById(Guid id);
}