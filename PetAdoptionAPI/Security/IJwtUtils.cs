using PetAdoptionAPI.Entities;

namespace PetAdoptionAPI.Security;

public interface IJwtUtils
{
    string GenerateJwtToken(Account account);
}