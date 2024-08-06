using System;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Models.Requests;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Services;

public interface IServicesService
{
    Task<ServiceResponse> CreateNewService(ServiceRequest request);    
    Task<Service> FindById(string id);
    Task<ServiceResponse> FindServiceById(string id);
    Task<List<ServiceResponse>> FindAllService();
    Task<ServiceResponse> UpdateService(ServiceRequest request);
    Task DeleteService(string id);
}
