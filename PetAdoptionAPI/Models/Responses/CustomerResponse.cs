using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionAPI.Entities;

public class CustomerResponse
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string? Address { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
    public Guid AccountId { get; set; }
    public bool IsActive { get; set; }
}