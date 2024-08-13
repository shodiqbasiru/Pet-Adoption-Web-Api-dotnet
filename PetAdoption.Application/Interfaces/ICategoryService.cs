using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface ICategoryService
{
    Task<Category> Create(CategoryRequest request);
    Task<Category> GetById(string id);
    Task<CategoryResponse> GetCategoryById(string id);
    Task<IEnumerable<CategoryResponse>> GetAllCategory();
    Task<Category> UpdateCategory(CategoryRequest request);
    Task DeleteById(string id);
}