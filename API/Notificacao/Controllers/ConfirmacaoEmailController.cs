namespace API.Notificacao.Controllers;

using Application.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Domain.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Notificacao.DTOs;

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

    // [HttpGet]
    // public async Task<IActionResult> Get()
    //     => Ok(await _service.GetAll());

    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(Guid id)
    // {
    //     var peca = await _service.GetById(id);
    //     if (peca == null) return NotFound();
    //     return Ok(peca);
    // }

    [HttpGet("pedidoConfirmacaoEmail")]
    public async Task<IActionResult> Get([FromBody] AprovacaoOrcamentoDto dto)
    {
        var resposta = _notificacaoService.EnviarOrcamento(dto);

        return Ok(resposta);
    }

    // [HttpPost("confirmarEmail/{guid}")]
    // public async Task<IActionResult> GetToken([FromBody] ConfirmarEmailDto dto)
    // {
    //     var resposta = await _tokenService.ObterTokenPorGuid(dto.Guid);
				
    //     var newTokenDto = new TokenResponseDto
    //     {
    //         GuidToken = resposta.GuidToken,
    //         HashedToken = resposta.HashedToken,
    //         ExpirationDate = resposta.ExpirationDate,
    //         OrdemServicoId = resposta.OrdemServicoId,
    //         Consumido = resposta.Consumido,
    //     };

    //     return Ok(resposta);
    // }

    //     [HttpPost]
    //     public async Task<IActionResult> Post([FromBody] PecaCreateDto dto)
    //     {
    //         await _service.Create(dto);
    //         return Ok();
    //     }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(Guid id, [FromBody] PecaUpdateDto dto)
    // {
    // 	await _service.Update(id, dto);
    // 	return Ok();
    // }

    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> Delete(Guid id)
    //     {
    //         await _service.Delete(id);
    //         return Ok();
    //     }

    // }

}
