using PetAdoption.Application.Interfaces;
using PetAdoption.Core.Entities;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Mappers;
using PetAdoption.Core.Models.Requests;
using PetAdoption.Core.Models.Responses;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Services;

public class ServicesService : IServicesService
{

    private readonly IUnitOfWork _uow;
    public ServicesService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ServiceResponse> CreateNewService(ServiceRequest request)
    {
        Service payload = new()
        {
            ServiceName = request.ServiceName,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var service = await _uow.Repository<Service>().SaveAsync(payload);
        await _uow.SaveChangesAsync();
        return service.ConvertToServiceResponse();
    }

    public async Task<Service> FindById(string id)
    {
        if (!Guid.TryParse(id, out var guid)) throw new NotFoundException("Service not found");
        return await _uow.Repository<Service>().FindByIdAsync(guid) ?? throw new NotFoundException("Service not found");
    }

    public async Task<ServiceResponse> FindServiceById(string id)
    {
        var service = await FindById(id);
        return service.ConvertToServiceResponse();
    }

    public async Task<List<ServiceResponse>> FindAllService()
    {
        var services = await _uow.Repository<Service>().FindAllAsync(c => c.DeletedAt == null);
        return services.ConvertToServiceResponses();
    }

    public async Task<ServiceResponse> UpdateService(ServiceRequest request)
    {
        var currentService = await FindById(request.Id ?? throw new BadRequestException("Id is required"));
        currentService.ServiceName = request.ServiceName;
        currentService.Description = request.Description;
        currentService.Price = request.Price;
        currentService.UpdatedAt = DateTime.Now;
        await _uow.SaveChangesAsync();
        return currentService.ConvertToServiceResponse();
    }

    public async Task DeleteService(string id)
    {
        Service currentService = await FindById(id);
        currentService.DeletedAt = DateTime.Now;
        await _uow.SaveChangesAsync();
    }
}
