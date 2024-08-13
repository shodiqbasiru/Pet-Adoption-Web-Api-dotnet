using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models.Requests;

public class LoginRequest
{
    [Required, MinLength(5)]
    public string Username { get; set; } = null!;

    [Required, MinLength(5)]
    public string Password { get; set; } = null!;
}