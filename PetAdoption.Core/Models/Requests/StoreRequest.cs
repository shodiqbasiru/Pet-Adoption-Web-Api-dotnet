using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Core.Models.Requests;

public class StoreRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Store name is required"), MinLength(3)]
    public string StoreName { get; set; } = null!;

    public string? Address { get; set; }
}
