namespace Application.Notificacao.Services;

using System.Threading.Tasks;
using Application.Notificacao.Interfaces;
using Domain.Notificacao.Entities;
using Application.Notificacao.DTOs;
using Domain.Notificacao.Interfaces;

public class NotificacaoService : INotificacaoService
{
    private readonly ICanalNotificacao _canalNotificacao;
    private readonly int numColumns = 40;

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
