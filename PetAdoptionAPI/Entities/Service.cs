using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionAPI.Entities;

[Table("m_service")]
public class Service{
    [Key, Column("id")]
    public Guid Id { get; set; }
    
    [Column("service_name")]
    public string ServiceName { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public uint Price { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // navigation property
    public List<Purchase> Purchases { get; set; } = null!;
}

// layanan service petshop atau pet adoption ada apa aja ?
// 1. grooming
// 2. vaksin
// 3. penitipan
// 4. konsultasi
// 5. operasi
// 6. pemeriksaan

// Harga layanan service petshop atau pet adoption berapa ?
// 1. grooming : 100.000
// 2. vaksin : 150.000
// 3. penitipan : 50.000
// 4. konsultasi : 75.000
// 5. operasi : 500.000
// 6. pemeriksaan : 100.000
