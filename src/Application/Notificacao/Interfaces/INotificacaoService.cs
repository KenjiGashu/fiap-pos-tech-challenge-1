namespace Gashu.SistemaMecanica.Application.Notificacao.Interfaces;

using Gashu.SistemaMecanica.Application.Notificacao.DTOs;

public interface INotificacaoService
{
    public Task EnviarOrcamento(AprovacaoOrcamentoDto dto);
}
