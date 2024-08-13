namespace PetAdoption.Core.Models.Responses;

public class RegisterSellerResponse
{   

    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? StoreName { get; set; }
    public string? Address { get; set; }
    public string? Role { get; set; }
}
