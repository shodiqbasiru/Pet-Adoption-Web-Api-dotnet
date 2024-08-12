using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class LoginRequest
{
    [Required, MinLength(6)]
    public string Username { get; set; } = null!;

    [Required, MinLength(6)]
    public string Password { get; set; } = null!;
}