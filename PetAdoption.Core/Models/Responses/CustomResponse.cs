namespace PetAdoption.Core.Models.Responses;

public class CustomResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}