using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse> Create(ProductRequest request);
    Task<Product> FindById(string id);
    Task<ProductResponse> FindProductById(string id);
    Task<List<ProductResponse>> FindAllProduct();
    Task<ProductResponse> Update(ProductRequest request);
    Task DeleteById(string id);
}