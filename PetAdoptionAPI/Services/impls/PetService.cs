using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services.impls;

public class PetService : IPetService
{

    private readonly IRepository<Pet> _repository;
    private readonly IPersistence _persistence;


    public PetService(IRepository<Pet> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<Pet> Create(Pet payload)
    {
        var pet = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return pet;
    }

    public async Task<Pet> GetById(string id)
    {
        try
        {
            var pet = await _repository.FindByIdAsync(Guid.Parse(id));
            if (pet is null) throw new Exception("pet not found");

            return pet;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Pet>> GetAll()
    {
        return await _repository.FindAllAsync();
    }

    public async Task<Pet> Update(Pet payload)
    {
        Pet pet = _repository.Update(payload);
        await _persistence.SaveChangesAsync();
        return pet;
    }

    public async Task DeleteById(string id)
    {
        var pet = await GetById(id);
        _repository.Delete(pet);
        await _persistence.SaveChangesAsync();
    }
}