using PetAdoption.Entities;
using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;

namespace PetAdoption.Services;

public interface IStoreService
{
    Task<Store> Create(Store payload);
    Task<Store> FindById(string id);
    Task<StoreResponse> FindStoreById(string id);
    Task<List<StoreResponse>> GetAll();
    Task<StoreResponse> Update(StoreRequest request);
    Task DeleteById(string id);
}