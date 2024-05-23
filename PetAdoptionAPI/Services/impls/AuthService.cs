using PetAdoptionAPI.Constants;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;
using PetAdoptionAPI.Repositories;
using PetAdoptionAPI.Security;

namespace PetAdoptionAPI.Services.impls;

public class AuthService : IAuthService
{
    private readonly IRepository<Account> _repository;
    private readonly IPersistence _persistence;
    private readonly ICustomerService _customerService;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IRepository<Account> repository, IPersistence persistence, ICustomerService customerService, IJwtUtils jwtUtils)
    {
        _repository = repository;
        _persistence = persistence;
        _customerService = customerService;
        _jwtUtils = jwtUtils;
    }

    public async Task<RegisterResponse> RegisterCustomer(RegisterRequest request)
    {
        var user = _repository.FindAsync(acc => acc.Username.ToLower().Equals(request.Username.ToLower()) );
        if (user != null) throw new DuplicateDataException("Username already exists");
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        Account payload = new()
        {
            Username = request.Username,
            Password = hashedPassword,
            Role = Role.Customer,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        var account = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();

        Customer payloadCustomer = new()
        {
            CustomerName = request.Name,
            Account = payload
        };
        var customer = await _customerService.Create(payloadCustomer);

        return new RegisterResponse
        {
            Id = account.Id.ToString(),
            Username = account.Username,
            Name = customer.CustomerName!,
            role = account.Role.ToString()
        };
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var account = await _repository.FindAsync(user => user.Username.ToLower().Equals(request.Username));
        if (account is null) throw new UnauthorizedAccessException("Unauthorized");
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
        {
            throw new UnauthorizedAccessException("Username or password is incorrect");
        }

        var token = _jwtUtils.GenerateJwtToken(account);
        return new LoginResponse
        {
            Username = account.Username,
            Token = token,
            Role = account.Role.ToString()
        };
    }
}