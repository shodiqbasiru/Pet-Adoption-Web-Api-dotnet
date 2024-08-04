using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class ProductUpdateRequest
{
    public Guid Id { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    public ulong Price { get; set; }
    public uint Stock { get; set; }
    public uint Rating { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
}