using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PetAdoption.Core.Constants;

namespace PetAdoption.Core.Entities;

[Table(name:"t_purchase")]
public class Purchase
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"trans_date")]
    public DateTime TransDate { get; set; }
    
    [Column(name:"trans_type")]
    public TransType TransType { get; set; }    

    [Column(name:"customer_id")]
    public Guid CustomerId { get; set; }

    [Column(name:"service_id")]    
    public Guid ServiceId { get; set; }


    public List<PurchaseDetail> PurchaseDetails { get; set; } = null!; 
    
    [JsonIgnore]
    public Customer? Customer { get; set; }
    [JsonIgnore]
    public Service? Service { get; set; }
}