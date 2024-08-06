using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class ServiceRequest
{
    public string? Id { get; set; }
    
    [Required(ErrorMessage = "Service name is required")]
    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required"), Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public uint Price { get; set; }
}
