using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; } = null!;
    [Required, MinLength(6)]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}