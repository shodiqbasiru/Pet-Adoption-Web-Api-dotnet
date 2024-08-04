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
    
    [JsonIgnore]
    public virtual Customer? Customer { get; set; } //foreign key
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } // for one to many
}


/*
 * virtual => penanda object relasi antara one to one ataupun one to many
 */