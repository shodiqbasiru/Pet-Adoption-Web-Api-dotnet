using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterCustomer(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}