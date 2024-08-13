using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Entities;

[Table(name: "m_customer")]
public class Customer
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "customer_name", TypeName = "Varchar(50)")]
    public string? CustomerName { get; set; }

    [Column(name: "address", TypeName = "Varchar(250)")]
    public string? Address { get; set; }

    [Column(name: "mobile_phone", TypeName = "Varchar(14)")]
    public string? MobilePhone { get; set; }

    [Column(name: "email", TypeName = "Varchar(50)")]
    public string? Email { get; set; }

    [Column(name: "account_id")]
    public Guid AccountId { get; set; }


    public Account? Account { get; set; }
    public List<Purchase>? Purchases { get; set; }
}