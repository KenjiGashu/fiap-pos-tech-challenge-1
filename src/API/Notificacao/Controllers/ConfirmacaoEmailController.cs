namespace API.Notificacao.Controllers;

using Application.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;
using Application.OrdensServico.Services;
using Domain.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Notificacao.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ConfirmacaoEmailController : ControllerBase
{
    private readonly INotificacaoService _notificacaoService;
    private readonly ITokenService _tokenService;
    private readonly OrdemServicoService _osService;
    private readonly ClienteService _clienteService;
    private readonly IOrdemServicoRepository _osrepo;
    private readonly IClienteRepository _clienterepo;

    public ConfirmacaoEmailController(INotificacaoService notificacaoService,
                OrdemServicoService osService, ClienteService clienteService,
                                                                            IOrdemServicoRepository osrepo, IClienteRepository clienterepo,
                                                                            ITokenService tokenService)
    {
        _notificacaoService = notificacaoService;
        _osService = osService;
        _clienteService = clienteService;
        _osrepo = osrepo;
        _clienterepo = clienterepo;
        _tokenService = tokenService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("pedidoConfirmacaoEmail")]
    public async Task<IActionResult> Get([FromBody] AprovacaoOrcamentoDto dto)
    {
        var resposta = _notificacaoService.EnviarOrcamento(dto);

        return Ok(resposta);
    }
}
