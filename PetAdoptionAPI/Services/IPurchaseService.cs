using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateTransaction(PurchaseRequest request);
    Task<List<PurchaseResponse>> GetAllTransaction();
}