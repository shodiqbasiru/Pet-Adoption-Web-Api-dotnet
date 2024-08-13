using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;

namespace PetAdoption.Application.Interfaces;

public interface ICustomerService
{
    Task<Customer> Create(Customer payload);
    Task<Customer> FindById(Guid id);
    Task<CustomerResponse> FindCustomerById(Guid id);
    Task<List<CustomerResponse>> GetAll();
    Task<CustomerResponse> Update(CustomerUpdateRequest request);
    Task DeleteById(Guid id);
}