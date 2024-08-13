using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateTransaction(PurchaseRequest request);
    Task<List<PurchaseResponse>> GetAllTransaction();
}