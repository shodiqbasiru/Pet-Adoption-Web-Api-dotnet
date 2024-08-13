namespace PetAdoption.Models.Requests;

public class CustomerUpdateRequest
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string? Address { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
}