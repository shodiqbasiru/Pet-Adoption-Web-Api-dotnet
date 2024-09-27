using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Core.Entities;

[Table(name: "t_order_detail")]
public class OrderDetail
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "order_id")]
    public Guid OrderId { get; set; }

    [Column(name: "product_id")]
    public Guid ProductId { get; set; }

    [Column(name: "qty")]
    public uint Qty { get; set; }

    public Order? Order { get; set; } 
    public Product? Product { get; set; }
}