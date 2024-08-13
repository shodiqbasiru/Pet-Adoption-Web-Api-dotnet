using PetAdoption.Core.Entities;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.Application.Interfaces;

public interface IServicesService
{
    Task<ServiceResponse> CreateNewService(ServiceRequest request);    
    Task<Service> FindById(string id);
    Task<ServiceResponse> FindServiceById(string id);
    Task<List<ServiceResponse>> FindAllService();
    Task<ServiceResponse> UpdateService(ServiceRequest request);
    Task DeleteService(string id);
}
