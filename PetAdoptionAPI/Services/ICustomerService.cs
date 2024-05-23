using PetAdoptionAPI.Entities;

namespace PetAdoptionAPI.Services;

public interface ICustomerService
{
    Task<Customer> Create(Customer payload);
    Task<Customer> GetById(string id);
    Task<List<Customer>> GetAll();
    Task<Customer> Update(Customer payload);
    Task DeleteById(string id);
}