namespace PetAdoption.Core.Models.Responses;

public class OrderResponse
{
    public string? Id { get; set; }
    public string? CustomerId { get; set; }
    public DateTime TransDate { get; set; }
    public List<OrderDetailResponse>? OrderDetails { get; set; }
}