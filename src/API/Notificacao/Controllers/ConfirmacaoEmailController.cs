namespace Gashu.SistemaMecanica.API.Notificacao.Controllers;

using Gashu.SistemaMecanica.API.Notificacao.Presenters;
using Gashu.SistemaMecanica.Application.Notificacao.Services;

public class ConfirmacaoEmailController : IConfirmacaoEmailController
{
    private readonly INotificacaoService _notificacaoService;
    private readonly IConfirmacaoEmailPresenter _presenter;

    public ConfirmacaoEmailController(INotificacaoService notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }

    public async Task<OutputConfirmacaoEmail> EnviarConfirmacaoEmail(AprovacaoOrcamentoDto dto)
    {
        await _notificacaoService.EnviarOrcamento(dto);

        return _presenter.Present("E-mail de confirmação enviado com sucesso!");
    }
}
