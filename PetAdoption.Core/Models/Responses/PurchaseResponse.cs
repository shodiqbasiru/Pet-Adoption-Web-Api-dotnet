namespace PetAdoption.Core.Models.Responses;

public class PurchaseResponse
{
    public string? Id { get; set; }
    public string? CustomerId { get; set; }
    public DateTime TransDate { get; set; }
    public List<PurchaseDetailResponse>? PurchaseDetail { get; set; }
}