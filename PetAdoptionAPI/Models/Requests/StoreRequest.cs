using System;

namespace PetAdoptionAPI.Models.Requests;

public class StoreRequest
{
    public string? Id { get; set; }
    public string StoreName { get; set; } = null!;
    public uint Rating { get; set; }
    public string? Address { get; set; }
}
