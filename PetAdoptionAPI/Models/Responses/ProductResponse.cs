using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public ulong Price { get; set; }
    public uint Stock { get; set; }
    public uint Rating { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}