using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class ProductRequest
{
    [Required]
    public string ProductName { get; set; } = null!;
    public ulong Price { get; set; }
    public uint Stock { get; set; }
    public uint Rating { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public Guid CategoryId { get; set; }
}