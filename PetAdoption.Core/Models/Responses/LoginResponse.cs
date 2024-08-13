namespace PetAdoption.Core.Models.Responses;

public class LoginResponse
{
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Role { get; set; } = null!;
}