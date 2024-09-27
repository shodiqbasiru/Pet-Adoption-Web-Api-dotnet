namespace PetAdoption.Core.Models.Requests;

public class CustomerUpdateRequest
{
    public string Id { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string? Address { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
}