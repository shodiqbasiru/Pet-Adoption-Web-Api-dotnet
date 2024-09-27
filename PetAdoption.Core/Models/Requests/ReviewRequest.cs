using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Core.Models.Requests;

public class ReviewRequest
{
    public string? Id { get; set; }

    public string? Comment { get; set; }

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public uint Rating { get; set; }

    [Required]
    public string ProductId { get; set; } = null!;

    [Required]
    public string CustomerId { get; set; } = null!;
}
