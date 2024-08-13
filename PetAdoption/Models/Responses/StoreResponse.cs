namespace PetAdoption.Models.Responses;

public class StoreResponse
{
    public Guid Id { get; set; }
    public string StoreName { get; set; } = null!;
    public uint Rating { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ProductResponse>? Products { get; set; }
}