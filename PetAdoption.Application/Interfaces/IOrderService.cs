using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateTransaction(OrderRequest request);
    Task<List<OrderResponse>> GetAllTransaction();
}