using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IProductService
{
    Task<ProductResponse> Create(ProductRequest request);
    Task<Product> FindById(string id);
    Task<ProductResponse> FindProductById(string id);
    Task<List<ProductResponse>> FindAllProduct();
    Task<ProductResponse> Update(ProductRequest request);
    Task DeleteById(string id);
}