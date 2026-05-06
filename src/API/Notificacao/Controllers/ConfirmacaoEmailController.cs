namespace Gashu.SistemaMecanica.API.Notificacao.Controllers;

using Application.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;
using Application.OrdensServico.Services;
using Domain.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Notificacao.DTOs;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pelo envio de solicitações de confirmação de orçamento por e-mail
/// </summary>
/// <remarks>
/// Este endpoint dispara o envio de um e-mail para o cliente contendo um link/token
/// para aprovação ou rejeição de orçamento de uma ordem de serviço.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ConfirmacaoEmailController : ControllerBase
{
    private readonly INotificacaoService _notificacaoService;

    public ConfirmacaoEmailController(INotificacaoService notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }

    /// <summary>
    /// Envia e-mail de confirmação de orçamento
    /// </summary>
    /// <remarks>
    /// Dispara o envio de um e-mail para o cliente com um link contendo um token único,
    /// permitindo a aprovação ou rejeição do orçamento da ordem de serviço.
    /// </remarks>
    /// <param name="dto">Dados necessários para envio do e-mail de confirmação</param>
    /// <response code="200">E-mail enviado com sucesso</response>
    /// <response code="400">Dados inválidos para envio</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("pedido-confirmacao-email")]
    public async Task<IActionResult> EnviarConfirmacaoEmail([FromBody] AprovacaoOrcamentoDto dto)
    {
        await _notificacaoService.EnviarOrcamento(dto);

        return Ok(new { Message = "E-mail de confirmação enviado com sucesso!"});
    }
}
