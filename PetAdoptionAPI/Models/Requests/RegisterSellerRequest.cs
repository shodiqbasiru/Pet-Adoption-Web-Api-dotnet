using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class RegisterSellerRequest
{
    [Required, MinLength(6)]
    public string Username { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required] 
    public string Password { get; set; } = null!;

    [Required, MinLength(3)]
    public string StoreName { get; set; } = null!;
    
    [Required, MinLength(10)]
    public string Address { get; set; } = null!;
}
