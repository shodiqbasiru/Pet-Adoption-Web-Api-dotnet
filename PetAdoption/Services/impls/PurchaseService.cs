using PetAdoption.Entities;
using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;
using PetAdoption.Repositories;

namespace PetAdoption.Services.impls;

public class PurchaseService : IPurchaseService
{
    private readonly IUnitOfWork _uow;
    private readonly IProductService _productService;

    public PurchaseService(IUnitOfWork uow, IProductService petService)
    {
        _uow = uow;
        _productService = petService;
    }

    public async Task<PurchaseResponse> CreateTransaction(PurchaseRequest request)
    {

        var newTrx = await _uow.ExecuteTransactionAsync<Purchase>(async () =>
         {
             var payload = new Purchase
             {
                 TransDate = DateTime.Now,
                 CustomerId = Guid.Parse(request.CustomerId),
                 PurchaseDetails = request.PurchaseDetails.Select(detail => new PurchaseDetail
                 {
                     PetId = Guid.Parse(detail.PetId),
                     Qty = detail.Qty
                 }).ToList()
             };
             var purchase = await _uow.Repository<Purchase>().SaveAsync(payload);
             await _uow.SaveChangesAsync();

             foreach (var detail in purchase.PurchaseDetails)
             {
                 var pet = await _productService.FindById(detail.PetId.ToString());
                 pet.Stock -= detail.Qty;
             }

             await _uow.SaveChangesAsync();
             return purchase;

         });

        List<PurchaseDetailResponse> detailResponses = newTrx!.PurchaseDetails.Select(detail =>
                new PurchaseDetailResponse
                {
                    Id = detail.Id.ToString(),
                    PetId = detail.PetId.ToString(),
                    Qty = detail.Qty
                }).ToList();


        PurchaseResponse response = new()
        {
            Id = newTrx.Id.ToString(),
            CustomerId = newTrx.CustomerId.ToString(),
            TransDate = newTrx.TransDate,
            PurchaseDetail = detailResponses
        };

        return response;
    }

    public async Task<List<PurchaseResponse>> GetAllTransaction()
    {
        var purchases = await _uow.Repository<Purchase>().FindAllAsync(new[] { "PurchaseDetails" });
        return purchases.Select(purchase => new PurchaseResponse
        {
            Id = purchase.Id.ToString(),
            CustomerId = purchase.CustomerId.ToString(),
            TransDate = purchase.TransDate,
            PurchaseDetail = purchase.PurchaseDetails.Select(detail => new PurchaseDetailResponse
            {
                Id = detail.Id.ToString(),
                PetId = detail.PetId.ToString(),
                Qty = detail.Qty
            }).ToList(),
        }).ToList();
    }
}