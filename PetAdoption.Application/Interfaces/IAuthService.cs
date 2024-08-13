using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IAuthService
{
    Task<RegisterResponse> RegisterCustomer(RegisterRequest request);
    Task<RegisterSellerResponse> RegisterSeller(RegisterSellerRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}