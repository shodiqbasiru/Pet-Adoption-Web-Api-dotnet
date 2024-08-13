using PetAdoption.Core.Entities;

namespace PetAdoption.Core.Mappers;

public static class CustomerMapper
{
    public static CustomerResponse ConvertToCustomerResponse(this Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
            Address = customer.Address,
            MobilePhone = customer.MobilePhone,
            Email = customer.MobilePhone,
            AccountId = customer.AccountId,
            IsActive = customer.Account.IsActive 
        };
    }

    public static List<CustomerResponse> ConvertToCustomerResponses(this List<Customer> customers)
    {
        return customers.Select(customer => customer.ConvertToCustomerResponse()).ToList();
    }
}