using Gashu.SistemaMecanica.API.Notificacao.Presenters;
using Gashu.SistemaMecanica.Application.Notificacao.Services;

namespace Gashu.SistemaMecanica.API.Notificacao.Controllers;

public interface IConfirmacaoEmailController
{
    public Task<OutputConfirmacaoEmail> EnviarConfirmacaoEmail(AprovacaoOrcamentoDto dto);
}
