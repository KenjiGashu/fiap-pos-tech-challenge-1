namespace Gashu.SistemaMecanica.Application.Identidade.Interfaces;

using Gashu.SistemaMecanica.Domain.Identidade.Entities;

public interface IJwtService
{
    public string GenerateToken(Usuario usuario);
}
