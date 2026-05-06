namespace Gashu.SistemaMecanica.Application.Notificacao.Services;

using System.Threading.Tasks;
using Gashu.SistemaMecanica.Application.Notificacao.Interfaces;
using Gashu.SistemaMecanica.Domain.Notificacao.Entities;
using Gashu.SistemaMecanica.Application.Notificacao.DTOs;
using Gashu.SistemaMecanica.Domain.Notificacao.Interfaces;

public class NotificacaoService : INotificacaoService
{
    private readonly ICanalNotificacao _canalNotificacao;

    public NotificacaoService(ICanalNotificacao canalNotificacao)
    {
        _canalNotificacao = canalNotificacao;
    }

    public async Task EnviarOrcamento(AprovacaoOrcamentoDto dto)
    {
        var servicos = dto.Servicos.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade, TipoItemOrcamento.Servico));
        var pecas = dto.Pecas.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade, TipoItemOrcamento.Peca));
        var aprocavaoOrcamento = new AprovacaoOrcamento(dto.OrdemServicoId, dto.TokenGuid, servicos, pecas, dto.Total, dto.NomeCliente, dto.Destinatario);

        await _canalNotificacao.EnviarMensagem(aprocavaoOrcamento.Destinatario, aprocavaoOrcamento.Titulo, aprocavaoOrcamento.Mensagem);
    }
}
