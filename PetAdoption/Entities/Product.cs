using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Entities;

[Table(name: "m_product")]
public class Product
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "product_name", TypeName = "Varchar(100)")]
    public string ProductName { get; set; } = null!;

    [Column(name: "price")]
    public ulong Price { get; set; }

    [Column(name: "stock")]
    public uint Stock { get; set; }

    [Column(name: "rating")]
    public uint Rating { get; set; }

    [Column(name: "description")]
    public string? Description { get; set; }

    [Column(name: "url")]
    public string? Url { get; set; }

    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }

    [Column(name: "updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column(name: "category_id")]
    public Guid CategoryId { get; set; }

    [Column(name: "store_id")]
    public Guid StoreId { get; set; }

    public Category Category { get; set; } = null!;
    public Store Store { get; set; } = null!;
}