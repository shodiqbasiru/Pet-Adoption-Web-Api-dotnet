using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Mappers;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class ProductService : IProductService
{

    private readonly IUnitOfWork _uow;

    public ProductService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ProductResponse> Create(ProductRequest request)
    {
        var category = await _uow.Repository<Category>().FindByIdAsync(request.CategoryId) ?? throw new NotFoundException("category not found");
        var store = await _uow.Repository<Store>().FindByIdAsync(request.StoreId) ?? throw new NotFoundException("store not found");

        Product payload = new()
        {
            ProductName = request.ProductName,
            Price = request.Price,
            Stock = request.Stock,
            Rating = request.Rating,
            Description = request.Description,
            Url = request.Url,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CategoryId = category.Id,
            StoreId = store.Id
        };

        var product = await _uow.Repository<Product>().SaveAsync(payload);
        await _uow.SaveChangesAsync();
        return product.ConvertToProductResponse();
    }

    public async Task<Product> FindById(Guid id)
    {
        return await _uow.Repository<Product>().FindByIdAsync(id) ?? throw new NotFoundException("pet not found");
    }

    public async Task<ProductResponse> FindProductById(Guid id)
    {
        var product = await FindById(id);
        return product.ConvertToProductResponse();
    }

    public async Task<List<ProductResponse>> FindAllProduct()
    {
        var products =  await _uow.Repository<Product>().FindAllAsync();
        return products.ConvertToProductResponses();
    }

    public async Task<ProductResponse> Update(ProductUpdateRequest request)
    {
        var currentProduct = await FindById(request.Id);
        currentProduct.ProductName = request.ProductName;
        currentProduct.Price = request.Price;
        currentProduct.Stock = request.Stock;
        currentProduct.Rating = request.Rating;
        currentProduct.Description = request.Description;
        currentProduct.Url = request.Url;
        currentProduct.UpdatedAt = DateTime.Now;

        Product product = _uow.Repository<Product>().Update(currentProduct);
        await _uow.SaveChangesAsync();
        return product.ConvertToProductResponse();
    }

    public async Task DeleteById(Guid id)
    {
        var pet = await FindById(id);
        _uow.Repository<Product>().Delete(pet);
        await _uow.SaveChangesAsync();
    }


}