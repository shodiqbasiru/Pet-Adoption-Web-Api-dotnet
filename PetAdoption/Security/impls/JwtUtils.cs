using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PetAdoption.Entities;

namespace PetAdoption.Security.impls;

public class JwtUtils : IJwtUtils
{

    private readonly IConfiguration _configuration;

    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(Account account)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        
        var securityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            new List<Claim>
            {
                new(ClaimTypes.Name, account.Username),
                new(ClaimTypes.Role, account.Role.ToString())
            },
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}