using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetAdoptionAPI.Entities;

[Table("m_store")]
public class Store
{
    [Key,Column("id")]
    public Guid Id { get; set; }

    [Column("store_name")]
    public string StoreName { get; set; } = null!;

    [Column("rating")]
    public uint Rating { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("account_id")]
    public Guid AccountId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }


    public Account? Account { get; set; }
    public List<Product>? Products {get;set;}
}