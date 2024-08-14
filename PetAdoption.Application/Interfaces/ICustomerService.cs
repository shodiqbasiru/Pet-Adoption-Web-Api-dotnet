using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;

namespace PetAdoption.Application.Interfaces;

public interface ICustomerService
{
    Task<Customer> Create(Customer payload);
    Task<Customer> FindById(string id);
    Task<CustomerResponse> FindCustomerById(string id);
    Task<List<CustomerResponse>> GetAll();
    Task<CustomerResponse> Update(CustomerUpdateRequest request);
    Task DeleteById(string id);
}