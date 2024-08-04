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
    private readonly IUnitOfWork _uow;
    private readonly ICustomerService _customerService;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IUnitOfWork uow, ICustomerService customerService, IJwtUtils jwtUtils)
    {
        _uow = uow;
        _customerService = customerService;
        _jwtUtils = jwtUtils;
    }

    public async Task<RegisterResponse> RegisterCustomer(RegisterRequest request)
    {
        var user = await _uow.Repository<Account>().FindAsync(acc => acc.Username.ToLower().Equals(request.Username.ToLower()) );
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

        var account = await _uow.Repository<Account>().SaveAsync(payload);
        await _uow.SaveChangesAsync();

        Customer payloadCustomer = new()
        {
            CustomerName = request.Name,
            Account = payload
        };
        var customer = await _customerService.Create(payloadCustomer);

        return new RegisterResponse
        {
            Id = account.Id,
            Username = account.Username,
            Name = customer.CustomerName!,
            Role = account.Role.ToString()
        };
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var account = await _uow.Repository<Account>().FindAsync(user => user.Username.ToLower().Equals(request.Username));
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