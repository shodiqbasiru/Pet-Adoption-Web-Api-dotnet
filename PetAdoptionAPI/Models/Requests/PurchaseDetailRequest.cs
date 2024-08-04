using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class PurchaseDetailRequest
{
    [Required]
    public string PetId { get; set; }
    public uint Qty { get; set; }
}