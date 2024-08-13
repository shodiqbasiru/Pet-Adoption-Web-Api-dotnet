using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetAdoption.Constants;

namespace PetAdoption.Entities;

[Table(name: "m_account")]
public class Account
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "username")]
    public string Username { get; set; } = null!;

    [Column(name: "email")]
    public string? Email { get; set; }

    [Column(name: "password")]
    public string Password { get; set; } = null!;

    [Column(name: "role")]
    public Role Role { get; set; }

    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }

    [Column(name: "is_active")]
    public bool IsActive { get; set; }

    [Column(name: "deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public Customer? Customer { get; set; }
    public Store? Store { get; set; }
}