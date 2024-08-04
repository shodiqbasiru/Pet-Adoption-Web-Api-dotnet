using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Mappers;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _uow;

    public CustomerService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Customer> Create(Customer payload)
    {
        var customer = await _uow.Repository<Customer>().SaveAsync(payload);
        await _uow.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> FindById(Guid id)
    {
        return await _uow.Repository<Customer>().FindByIdAsync(id) ?? throw new NotFoundException("customer not found");
    }

    public async Task<CustomerResponse> FindCustomerById(Guid id)
    {
        var customer = await FindById(id);
        return customer.ConvertToCustomerResponse();
    }

    public async Task<List<CustomerResponse>> GetAll()
    {
        try
        {
            string[] includes = { "Account" };
            var customers = await _uow.Repository<Customer>().FindAllAsync(includes); // TODO: Masih Harus di perbaiki untuk melakuka get All with Is active Account
            return customers.ConvertToCustomerResponses();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    public async Task<CustomerResponse> Update(CustomerUpdateRequest request)
    {
        var currentCustomer = await FindById(request.Id);

        currentCustomer.CustomerName = request.CustomerName;
        currentCustomer.Address = request.Address;
        currentCustomer.MobilePhone = request.MobilePhone;
        currentCustomer.Email = request.Email;

        var customer = _uow.Repository<Customer>().Update(currentCustomer);
        await _uow.SaveChangesAsync();
        return customer.ConvertToCustomerResponse();
    }

    public async Task DeleteById(Guid id)
    {
        string[] includes = { "Account" };
        var customer = await _uow.Repository<Customer>().FindAsync(c => c.Id == id, includes);
        customer.Account.IsActive = false;
        _uow.Repository<Customer>().Update(customer);
        await _uow.SaveChangesAsync();

    }
}