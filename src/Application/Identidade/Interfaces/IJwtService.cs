namespace Application.Identidade.Interfaces;

using Domain.Identidade.Entities;

public interface IJwtService
{
    public string GenerateToken(Usuario usuario);
}
