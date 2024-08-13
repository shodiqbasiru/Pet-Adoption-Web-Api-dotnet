using PetAdoption.Entities;
using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;

namespace PetAdoption.Services;

public interface IProductService
{
    Task<ProductResponse> Create(ProductRequest request);
    Task<Product> FindById(string id);
    Task<ProductResponse> FindProductById(string id);
    Task<List<ProductResponse>> FindAllProduct();
    Task<ProductResponse> Update(ProductRequest request);
    Task DeleteById(string id);
}