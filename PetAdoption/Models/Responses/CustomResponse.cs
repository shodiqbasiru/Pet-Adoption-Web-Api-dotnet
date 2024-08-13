namespace PetAdoption.Models.Responses;

public class CustomResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}