namespace Application.Identidade.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Identidade.Entities;
using Application.Identidade.Interfaces;

public class JwtService : IJwtService
{
    private readonly string? _secret = Environment.GetEnvironmentVariable("FIAP_POS_SECRET");

    public string GenerateToken(Usuario usuario)
    {
        if(_secret == null)
            throw new Exception("secret inexistente");

        var key = Encoding.ASCII.GetBytes(_secret);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
        };

        foreach(var role in usuario.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Nome));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

}
