namespace PetAdoption.Core.Models.Responses;

public class ServiceResponse
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public string? Description { get; set; }
    public uint Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
