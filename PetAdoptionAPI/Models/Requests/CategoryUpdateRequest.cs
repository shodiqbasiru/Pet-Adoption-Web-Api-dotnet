using System.ComponentModel.DataAnnotations;

namespace PetAdoptionAPI.Models.Requests;

public class CategoryUpdateRequest
{
    public Guid Id { get; set; }
    [Required, MinLength(3)]
    public string CategoryName { get; set; } = null!;
}