using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Core.Models.Requests;

public class PurchaseDetailRequest
{
    [Required]
    public string ProductID { get; set; } = null!;
    public uint Qty { get; set; }
}