using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _repository;
    private readonly IPersistence _persistence;

    public CustomerService(IRepository<Customer> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<Customer> Create(Customer payload)
    {
        var customer = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> GetById(string id)
    {
        var customer = await _repository.FindByIdAsync(Guid.Parse(id));
        if (customer is null) throw new NotFoundException("customer not found");
        return customer;
    }

    public async Task<List<Customer>> GetAll()
    {
        return await _repository.FindAllAsync();
    }

    public async Task<Customer> Update(Customer payload)
    {
        var customer = _repository.Update(payload);
        await _persistence.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteById(string id)
    {
        var customer = await GetById(id);
        _repository.Delete(customer);
        await _persistence.SaveChangesAsync();
    }
}