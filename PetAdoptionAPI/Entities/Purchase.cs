using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetAdoptionAPI.Entities;

[Table(name:"t_purchase")]
public class Purchase
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"trans_date")]
    public DateTime TransDate { get; set; }
    
    [Column(name:"customer_id")]
    public Guid CustomerId { get; set; }

    public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = null!; 
    
    [JsonIgnore]
    public Customer? Customer { get; set; }
}