using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;
    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Category> Create(CategoryRequest request)
    {
        Category payload = new()
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
        string[] includes = { "Products" };
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

    public async Task<Category> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid categoryId)) throw new NotFoundException("category not found");
        return await _uow.Repository<Category>().FindByIdAsync(categoryId) ?? throw new NotFoundException("category not found");
    }

    public Task<CategoryResponse> GetCategoryById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> UpdateCategory(CategoryRequest request)
    {
        var category = await GetById(request.Id ?? throw new BadRequestException("Category id is required"));
        category.CategoryName = request.CategoryName;
        category.UpdatedAt = DateTime.Now;

        var newCategery = _uow.Repository<Category>().Update(category);
        await _uow.SaveChangesAsync();
        return newCategery;
    }

    public async Task DeleteById(string id)
    {
        var category = await GetById(id);
        _uow.Repository<Category>().Delete(category);
        await _uow.SaveChangesAsync();
    }

}