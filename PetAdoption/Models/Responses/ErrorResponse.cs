using System.Net;

namespace PetAdoption.Models.Responses;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    public object Errors { get; set; } = null!;
}