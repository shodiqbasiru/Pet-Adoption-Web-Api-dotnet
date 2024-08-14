namespace PetAdoption.Core.Models.Responses;

public class ReviewResponse
{
    public string Id { get; set; } = null!;
    public string? Comment { get; set; }
    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ProductId { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
}
