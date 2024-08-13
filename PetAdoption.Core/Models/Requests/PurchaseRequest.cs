using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Core.Models.Requests;

public class PurchaseRequest
{
    [Required]
    public string CustomerId { get; set; } = null!;
    [Required]
    public List<PurchaseDetailRequest> PurchaseDetails { get; set; } = null!;
}