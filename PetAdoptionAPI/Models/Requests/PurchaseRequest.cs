using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class PurchaseRequest
{
    [Required]
    public string CustomerId { get; set; }
    public List<PurchaseDetailRequest> PurchaseDetails { get; set; }
}