using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionAPI.Entities;

[Table(name:"m_pet")]
public class Pet
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"pet_name",TypeName = "NVarchar(50)")]
    public string? Name { get; set; }
    
    [Column(name:"price")]
    public long Price { get; set; }
    
    [Column(name:"stock")]
    public int Stock { get; set; }
}