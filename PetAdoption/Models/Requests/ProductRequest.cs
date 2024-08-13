using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models.Requests;

public class ProductRequest
{
    public string? Id { get; set; }

    [Required, MinLength(3)]
    public string ProductName { get; set; } = null!;

    [Required, Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public ulong Price { get; set; }

    [Required, Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than 0")]
    public uint Stock { get; set; }

    [Required, Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public uint Rating { get; set; }

    public string? Description { get; set; }
    
    public string? Url { get; set; }
    
    public string? CategoryId { get; set; }
    public string? StoreId { get; set; }
}