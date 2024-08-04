using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class PurchaseService : IPurchaseService
{
    private readonly IRepository<Purchase> _repository;
    private readonly IPersistence _persistence;
    private readonly IProductService _petService;

    public PurchaseService(IRepository<Purchase> repository, IPersistence persistence, IProductService petService)
    {
        _repository = repository;
        _persistence = persistence;
        _petService = petService;
    }

    public async Task<PurchaseResponse> CreateTransaction(PurchaseRequest request)
    {
        await _persistence.BeginTransactionAsync();
        try
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

            var purchase = await _repository.SaveAsync(payload);
            await _persistence.SaveChangesAsync();

            foreach (var detail in purchase.PurchaseDetails)
            {
                var pet = await _petService.FindById(detail.PetId);
                pet.Stock -= detail.Qty;
            }

            await _persistence.SaveChangesAsync();

            await _persistence.CommitTransactionAsync();

            // cara normal
            // List<PurchaseDetailResponse> detailResponses = new List<PurchaseDetailResponse>();
            // foreach (PurchaseDetail detail in purchase.PurchaseDetails)
            // {
            //     detailResponses.Add(new PurchaseDetailResponse
            //     {
            //         ProductId = detail.PetId.ToString(),
            //         Qty = detail.Qty
            //     });
            // }

            // linq
            List<PurchaseDetailResponse> detailResponses = purchase.PurchaseDetails.Select(detail =>
                new PurchaseDetailResponse
                {
                    Id = detail.Id.ToString(),
                    PetId = detail.PetId.ToString(),
                    Qty = detail.Qty
                }).ToList();


            PurchaseResponse response = new()
            {
                Id = purchase.Id.ToString(),
                CustomerId = purchase.CustomerId.ToString(),
                TransDate = purchase.TransDate,
                PurchaseDetail = detailResponses
            };
            return response;
        }
        catch (Exception e)
        {
            await _persistence.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<PurchaseResponse>> GetAllTransaction()
    {
        var purchases = await _repository.FindAllAsync(new[] { "PurchaseDetails" });
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