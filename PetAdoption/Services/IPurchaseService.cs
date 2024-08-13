using PetAdoption.Entities;
using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;

namespace PetAdoption.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateTransaction(PurchaseRequest request);
    Task<List<PurchaseResponse>> GetAllTransaction();
}