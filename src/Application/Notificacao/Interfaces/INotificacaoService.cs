namespace Application.Notificacao.Interfaces;

using Application.Notificacao.DTOs;

public interface INotificacaoService
{
    public Task EnviarOrcamento(AprovacaoOrcamentoDto dto);
}
