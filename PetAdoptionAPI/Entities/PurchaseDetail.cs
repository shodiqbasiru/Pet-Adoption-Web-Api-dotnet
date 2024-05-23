using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionAPI.Entities;

[Table(name:"t_purchase_detail")]
public class PurchaseDetail
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"purchase_id")]
    public Guid PurchaseId { get; set; }
    
    [Column(name:"pet_id")]
    public Guid PetId { get; set; }
    
    [Column(name:"qty")]
    public int Qty { get; set; }

    public Purchase Purchase { get; set; } // many to one with purchase
    public Pet Pet { get; set; }
}