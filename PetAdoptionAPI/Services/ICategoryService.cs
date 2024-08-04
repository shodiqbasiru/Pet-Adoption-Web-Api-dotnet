using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface ICategoryService
{
    Task<Category> Create(CategoryRequest request);
    Task<Category> GetById(Guid id);
    Task<IEnumerable<CategoryResponse>> GetAllCategory();
    Task<Category> UpdateCategory(CategoryUpdateRequest request);
    Task DeleteById(Guid id);
}