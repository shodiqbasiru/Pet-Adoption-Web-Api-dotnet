namespace PetAdoption.Models.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public IEnumerable<ProductResponse>? Products { get; set; }
}