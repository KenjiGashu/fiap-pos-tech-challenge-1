namespace Gashu.SistemaMecanica.API.Notificacao.Controllers;

using Application.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;
using Application.OrdensServico.Services;
using Domain.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Notificacao.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Notificacao.Presenters;

public class ConfirmacaoEmailController : IConfirmacaoEmailController
{
    private readonly INotificacaoService _notificacaoService;
    private readonly IConfirmacaoEmailPresenter _presenter;

    public ConfirmacaoEmailController(INotificacaoService notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }

    public async Task<OutputConfirmacaoEmail> EnviarConfirmacaoEmail([FromBody] AprovacaoOrcamentoDto dto)
    {
        await _notificacaoService.EnviarOrcamento(dto);

        return _presenter.Present("E-mail de confirmação enviado com sucesso!");
    }
}
