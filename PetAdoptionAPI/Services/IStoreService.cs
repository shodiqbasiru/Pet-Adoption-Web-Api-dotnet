using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IStoreService
{
    Task<Store> Create(Store payload);
    Task<Store> FindById(string id);
    Task<StoreResponse> FindStoreById(string id);
    Task<List<StoreResponse>> GetAll();
    Task<StoreResponse> Update(StoreRequest request);
    Task DeleteById(string id);
}