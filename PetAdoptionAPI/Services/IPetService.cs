using PetAdoptionAPI.Entities;

namespace PetAdoptionAPI.Services;

public interface IPetService
{
    Task<Pet> Create(Pet payload);
    Task<Pet> GetById(string id);
    Task<List<Pet>> GetAll();
    Task<Pet> Update(Pet payload);
    Task DeleteById(string id);
}