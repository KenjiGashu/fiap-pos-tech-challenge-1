namespace Application.OrdensServico.Interfaces;

using Domain.Notificacao.Entities;
using Domain.OrdensServico.Entities;

public interface ITokenService
{
    public Task<Token> GeraToken(Guid ordemServicoId);
    public Task<Token?> ObterTokenPorGuid(Guid id);
}
