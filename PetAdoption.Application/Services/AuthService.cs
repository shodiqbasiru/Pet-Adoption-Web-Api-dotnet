using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Constants;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly ICustomerService _customerService;
    private readonly IStoreService _storeService;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IUnitOfWork uow, ICustomerService customerService, IStoreService storeService, IJwtUtils jwtUtils)
    {
        _uow = uow;
        _customerService = customerService;
        _storeService = storeService;
        _jwtUtils = jwtUtils;
    }

    public async Task<RegisterResponse> RegisterCustomer(RegisterRequest request)
    {
        var user = await _uow.Repository<Account>().FindAsync(acc => acc.Username.ToLower().Equals(request.Username.ToLower()));
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
            Account = payload,
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

    public async Task<RegisterSellerResponse> RegisterSeller(RegisterSellerRequest request)
    {
        var seller = await _uow.Repository<Account>().FindAsync(acc => acc.Email!.Equals(request.Email));
        if (seller != null) throw new DuplicateDataException("Email already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        Account payload = new()
        {
            Username = request.Username,
            Email = request.Email,
            Password = hashedPassword,
            Role = Role.Seller,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        var account = await _uow.Repository<Account>().SaveAsync(payload);
        await _uow.SaveChangesAsync();

        Store payloadStore = new()
        {
            StoreName = request.StoreName,
            Address = request.Address,
            Account = payload,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var store = await _storeService.Create(payloadStore);

        return new RegisterSellerResponse
        {
            Id = account.Id,
            Username = account.Username,
            Email = account.Email,  
            StoreName = store.StoreName,
            Address = store.Address,
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