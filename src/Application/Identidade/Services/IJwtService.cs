namespace Gashu.SistemaMecanica.Application.Identidade.Services;

using Gashu.SistemaMecanica.Domain.Identidade.Entities;

public interface IJwtService
{
    public string GenerateToken(Usuario usuario);
}
