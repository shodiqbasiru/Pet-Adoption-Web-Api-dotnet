using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class CategoryRequest
{
    [Required]
    public string CategoryName { get; set; } = null!;
}