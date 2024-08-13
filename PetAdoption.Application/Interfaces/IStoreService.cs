using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IStoreService
{
    Task<Store> Create(Store payload);
    Task<Store> FindById(string id);
    Task<StoreResponse> FindStoreById(string id);
    Task<List<StoreResponse>> GetAll();
    Task<StoreResponse> Update(StoreRequest request);
    Task DeleteById(string id);
}