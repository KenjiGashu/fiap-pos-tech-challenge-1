namespace Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface ITokenRepository
{
    public Task SalvaToken(string token, Guid ordemServicoId);

    public Task<Token> ObterToken(string token);
}
