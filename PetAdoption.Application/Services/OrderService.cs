using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;
    private readonly IProductService _productService;

    public OrderService(IUnitOfWork uow, IProductService petService)
    {
        _uow = uow;
        _productService = petService;
    }

    public async Task<OrderResponse> CreateTransaction(OrderRequest request)
    {

        var newTrx = await _uow.ExecuteTransactionAsync<Order>(async () =>
         {
             var payload = new Order
             {
                 TransDate = DateTime.Now,
                 CustomerId = Guid.Parse(request.CustomerId),
                 OrderDetails = request.PurchaseDetails.Select(detail => new OrderDetail
                 {
                     ProductId = Guid.Parse(detail.ProductID),
                     Qty = detail.Qty
                 }).ToList()
             };
             var purchase = await _uow.Repository<Order>().SaveAsync(payload);
             await _uow.SaveChangesAsync();

             foreach (var detail in purchase.OrderDetails)
             {
                 var pet = await _productService.FindById(detail.ProductId.ToString());
                 pet.Stock -= detail.Qty;
             }

             await _uow.SaveChangesAsync();
             return purchase;

         });

        List<OrderDetailResponse> detailResponses = newTrx!.OrderDetails.Select(detail =>
                new OrderDetailResponse
                {
                    Id = detail.Id.ToString(),
                    ProductId = detail.ProductId.ToString(),
                    Qty = detail.Qty
                }).ToList();


        OrderResponse response = new()
        {
            Id = newTrx.Id.ToString(),
            CustomerId = newTrx.CustomerId.ToString(),
            TransDate = newTrx.TransDate,
            OrderDetails = detailResponses
        };

        return response;
    }

    public async Task<List<OrderResponse>> GetAllTransaction()
    {
        var purchases = await _uow.Repository<Order>().FindAllAsync(new[] { "OrderDetails" });
        return purchases.Select(purchase => new OrderResponse
        {
            Id = purchase.Id.ToString(),
            CustomerId = purchase.CustomerId.ToString(),
            TransDate = purchase.TransDate,
            OrderDetails = purchase.OrderDetails.Select(detail => new OrderDetailResponse
            {
                Id = detail.Id.ToString(),
                ProductId = detail.ProductId.ToString(),
                Qty = detail.Qty
            }).ToList(),
        }).ToList();
    }
}