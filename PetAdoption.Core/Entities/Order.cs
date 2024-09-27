using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PetAdoption.Core.Constants;

namespace PetAdoption.Core.Entities;

[Table(name:"t_order")]
public class Order
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"trans_date")]
    public DateTime TransDate { get; set; }
    
    [Column(name:"customer_id")]
    public Guid CustomerId { get; set; }


    public List<OrderDetail> OrderDetails { get; set; } = null!; 
    
    [JsonIgnore]
    public Customer? Customer { get; set; }
    
}