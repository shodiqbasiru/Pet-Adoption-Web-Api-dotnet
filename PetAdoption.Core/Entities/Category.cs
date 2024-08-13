using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetAdoption.Core.Entities;

[Table(name: "m_category")]
public class Category
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "category_name", TypeName = "NVarchar(50)")]
    public string CategoryName { get; set; } = null!;

    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }

    [Column(name: "updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public List<Product> Products {get; set;} = null!;
}