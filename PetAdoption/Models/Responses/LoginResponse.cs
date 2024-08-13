namespace PetAdoption.Models.Responses;

public class LoginResponse
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}