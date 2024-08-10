using System;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Mappers;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class StoreService : IStoreService
{
    private readonly IUnitOfWork _uow;
    public StoreService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Store> Create(Store payload)
    {
        var store = await _uow.Repository<Store>().SaveAsync(payload);
        await _uow.SaveChangesAsync();
        return store;
    }

    public async Task<Store> FindById(string id)
    {
        if (!Guid.TryParse(id, out var guid)) throw new NotFoundException("store not found");
        return await _uow.Repository<Store>().FindByIdAsync(guid) ?? throw new NotFoundException("store not found");
    }

    public async Task<StoreResponse> FindStoreById(string id)
    {
        var store = await FindById(id);
        return store.ConvertToStoreResponse();
    }

    public async Task<List<StoreResponse>> GetAll()
    {
        var stores = await _uow.Repository<Store>().FindAllAsync(new[] { "Products" });
        return stores.ConvertToStoreResponses();
    }

    public async Task<StoreResponse> Update(StoreRequest request)
    {
        var store = await FindById(request.Id ?? throw new BadRequestException("Id is required"));
        store.StoreName = request.StoreName;
        store.Rating = request.Rating;
        store.Address = request.Address;
        store.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync();

        return store.ConvertToStoreResponse();
    }

    public async Task DeleteById(string id)
    {
        /*
            TODO: Fix this method to soft delete the store
        */
        var currentStore = await FindById(id);
        // var currentAccount = await _uow.Repository<Account>().FindAsync(acc => acc.Id == currentStore.AccountId, new[] { "Stores" }) ?? throw new NotFoundException("Account not found");

        // currentAccount.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync();
    }
}