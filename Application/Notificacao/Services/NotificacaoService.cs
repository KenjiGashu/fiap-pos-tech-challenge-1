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
        var token = await _tokenService.GeraToken(dto.OrdemServicoId);
        var servicos = dto.Servicos.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade));
        var pecas = dto.Pecas.Select(s => new ItemOrcamento(s.Nome, s.Preco, s.Quantidade));
        var aprocavaoOrcamento = new AprovacaoOrcamento(dto.OrdemServicoId, token, servicos, pecas, dto.Total, dto.NomeCliente, dto.Destinatario);

        // var os = await _repo.ObterPorId(dto.GetHashCode);

        // if (os == null)
        // {
        //     throw new Exception("ordem servico invalida");
        // }

        // var cliente = await _clienteRepo.ObterPorId(os.ClienteId);

        // if(cliente == null)
        // {
        //     throw new Exception("Cliente invalido");
        // }

        // Console.WriteLine($"cliente::: {cliente} {cliente.Id} {cliente.Nome}");

        // Token token = await _tokenService.GeraToken(ordemServicoId);

        // string linkAprovacao = GeraLinkAprovacao(token);
        // string linkRejeicao = GeraLinkRejeicao(token);

        // string corpoEmail = MontaCorpoEmail(token, os, cliente);

        // EmailOrcamento email = new EmailOrcamento(corpoEmail, $"Orçamento Ordem Servico {ordemServicoId}");

        await _canalNotificacao.EnviarMensagem(aprocavaoOrcamento.Destinatario, aprocavaoOrcamento.Titulo, aprocavaoOrcamento.Mensagem);
    }

		public async Task AprovarOrcamento(Guid token)
		{
			//_tokenService.
		}
}
