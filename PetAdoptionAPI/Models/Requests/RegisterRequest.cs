using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}