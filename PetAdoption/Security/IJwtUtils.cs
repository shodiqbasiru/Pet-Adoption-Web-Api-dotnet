using PetAdoption.Entities;

namespace PetAdoption.Security;

public interface IJwtUtils
{
    string GenerateJwtToken(Account account);
}