using PetAdoption.Core.Entities;

namespace PetAdoption.Application.Interfaces;

public interface IJwtUtils
{
    string GenerateJwtToken(Account account);
}