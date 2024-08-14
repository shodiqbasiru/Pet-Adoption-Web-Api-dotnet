using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Core.Entities;

[Table("m_review")]
public class Review
{
    [Key,Column("id")]
    public Guid Id { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("rating")]
    public uint Rating { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("customer_id")]
    public Guid CustomerId { get; set; }


    public Product? Product { get; set; }
    public Customer? Customer { get; set; }
}
