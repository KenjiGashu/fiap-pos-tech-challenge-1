namespace Application.Notificacao.Services;

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Notificacao.Interfaces;
using Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;
using System.Security.Cryptography;
using Domain.Notificacao.Entities;
using Application.OrdensServico.Interfaces;
using Application.Notificacao.DTOs;
using Domain.Notificacao.Interfaces;

public class NotificacaoService : INotificacaoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IClienteRepository _clienteRepo;
    private readonly ITokenService _tokenService;
    private readonly ICanalNotificacao _canalNotificacao;
    private readonly int numColumns = 40;

    public NotificacaoService(IOrdemServicoRepository repo,
															IClienteRepository clienteRepo,
															ITokenService tokenService,
															ICanalNotificacao canalNotificacao)
    {
        _repo = repo;
        _clienteRepo = clienteRepo;
        _tokenService = tokenService;
        _canalNotificacao = canalNotificacao;
    }

    public async Task EnviarOrcamento(AprovacaoOrcamentoDto dto)
    {
			var token = await _tokenService.ObterTokenPorGuid(new Guid(dto.Token));
        var servicos = dto.Servicos.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade));
        var pecas = dto.Pecas.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade));
        var aprocavaoOrcamento = new AprovacaoOrcamento(dto.OrdemServicoId, token, servicos, pecas, dto.Total, dto.NomeCliente, dto.Destinatario);

        await _canalNotificacao.EnviarMensagem(aprocavaoOrcamento.Destinatario, aprocavaoOrcamento.Titulo, aprocavaoOrcamento.Mensagem);
    }

		public async Task AprovarOrcamento(Guid token)
		{
			//_tokenService.
		}
}
