namespace PetAdoptionAPI.Models.Requests;

public class RegisterRequest
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}