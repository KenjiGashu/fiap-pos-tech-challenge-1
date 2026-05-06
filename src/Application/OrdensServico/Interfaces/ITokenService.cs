namespace Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Domain.Notificacao.Entities;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface ITokenService
{
    public Task<Token> GeraToken(Guid ordemServicoId);
    public Task<Token?> ObterTokenPorGuid(Guid id);
}
