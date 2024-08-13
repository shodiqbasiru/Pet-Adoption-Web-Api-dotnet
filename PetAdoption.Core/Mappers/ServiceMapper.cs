using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Core.Mappers;

public static class ServiceMapper
{
    public static ServiceResponse ConvertToServiceResponse(this Service service)
    {
        return  new ServiceResponse
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            Description = service.Description,
            Price = service.Price,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt
        };
    }

    public static List<ServiceResponse> ConvertToServiceResponses(this List<Service> services)
    {
        return  services.Select(service => service.ConvertToServiceResponse()).ToList();
    }
}
