namespace PetAdoption.Core.Models.Responses;

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
}