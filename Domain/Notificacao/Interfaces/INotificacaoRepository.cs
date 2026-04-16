namespace Domain.Notificacao.Interfaces;

using Domain.Notificacao.Entities;

public interface INotificacaoRepository
{
    public Task SalvaToken(string token, Guid ordemServicoId);

    public Task<Token?> ObterToken(string token);
}
