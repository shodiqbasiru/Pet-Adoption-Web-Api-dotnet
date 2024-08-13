using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models.Requests;

public class StoreRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Store name is required"), MinLength(3)]
    public string StoreName { get; set; } = null!;

    [Required(ErrorMessage = "Rating is required"), Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public uint Rating { get; set; }
    public string? Address { get; set; }
}
