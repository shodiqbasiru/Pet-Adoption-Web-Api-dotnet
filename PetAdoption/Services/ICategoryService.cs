using PetAdoption.Entities;
using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;

namespace PetAdoption.Services;

public interface ICategoryService
{
    Task<Category> Create(CategoryRequest request);
    Task<Category> GetById(string id);
    Task<CategoryResponse> GetCategoryById(string id);
    Task<IEnumerable<CategoryResponse>> GetAllCategory();
    Task<Category> UpdateCategory(CategoryRequest request);
    Task DeleteById(string id);
}