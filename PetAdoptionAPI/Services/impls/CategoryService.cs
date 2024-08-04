using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;
    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Category> Create(CategoryRequest request)
    {
        Category payload = new ()
        {
            CategoryName = request.CategoryName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        var category = await _uow.Repository<Category>().SaveAsync(payload);
        await _uow.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategory()
    {
        string[] includes = {"Products"};
        var categories = await _uow.Repository<Category>().FindAllAsync(includes);
        
        var categoryResponses = categories.Select(category => new CategoryResponse()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Products = category.Products.Select(p => new ProductResponse()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                Rating = p.Rating,
                Stock = p.Stock,
                Description = p.Description,
                Url = p.Url
            }).ToList()
        }).ToList();
        return categoryResponses;

    }

    public async Task<Category> GetById(Guid id)
    {
        return await _uow.Repository<Category>().FindByIdAsync(id) ?? throw new NotFoundException("category not found");
    }   

    public async Task<Category> UpdateCategory(CategoryUpdateRequest request)
    {
        var category = await GetById(request.Id);
        category.CategoryName = request.CategoryName;
        category.UpdatedAt = DateTime.Now;

        var newCategery = _uow.Repository<Category>().Update(category);
        await _uow.SaveChangesAsync();
        return newCategery;
    }

    public async Task DeleteById(Guid id)
    {
        var category = await GetById(id);
        _uow.Repository<Category>().Delete(category);
        await _uow.SaveChangesAsync();
    }
}