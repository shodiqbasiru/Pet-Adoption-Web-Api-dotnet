using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Mappers;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class ProductService : IProductService
{

    private readonly IUnitOfWork _uow;
    private readonly ICategoryService _categoryService;
    private readonly IStoreService _storeService;

    public ProductService(IUnitOfWork uow, ICategoryService categoryService, IStoreService storeService)
    {
        _uow = uow;
        _categoryService = categoryService;
        _storeService = storeService;
    }
    public async Task<ProductResponse> Create(ProductRequest request)
    {
        var category = await _categoryService.GetById(request.CategoryId!);
        var store = await _storeService.FindById(request.StoreId!);

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

    public async Task<Product> FindById(string id)
    {
        if (!Guid.TryParse(id, out Guid productId)) throw new NotFoundException("product not found");
        return await _uow.Repository<Product>().FindByIdAsync(productId) ?? throw new NotFoundException("product not found");
    }

    public async Task<ProductResponse> FindProductById(string id)
    {
        var product = await FindById(id);
        return product.ConvertToProductResponse();
    }

    public async Task<List<ProductResponse>> FindAllProduct()
    {
        var products =  await _uow.Repository<Product>().FindAllAsync();
        return products.ConvertToProductResponses();
    }

    public async Task<ProductResponse> Update(ProductRequest request)
    {
        var currentProduct = await FindById(request.Id ?? throw new BadRequestException("Id is required"));
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

    public async Task DeleteById(string id)
    {
        var pet = await FindById(id);
        _uow.Repository<Product>().Delete(pet);
        await _uow.SaveChangesAsync();
    }


}