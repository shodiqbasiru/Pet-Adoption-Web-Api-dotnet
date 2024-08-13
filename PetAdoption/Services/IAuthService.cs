using PetAdoption.Models.Requests;
using PetAdoption.Models.Responses;

namespace PetAdoption.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterCustomer(RegisterRequest request);
    Task<RegisterSellerResponse> RegisterSeller(RegisterSellerRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}