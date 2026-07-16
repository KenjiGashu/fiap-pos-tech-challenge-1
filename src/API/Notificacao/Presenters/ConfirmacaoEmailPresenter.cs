namespace Gashu.SistemaMecanica.API.Notificacao.Presenters;

public class ConfirmacaoEmailPresenter : IConfirmacaoEmailPresenter
{
    public OutputConfirmacaoEmail Present(string message)
    {
        return new OutputConfirmacaoEmail
        {
            Message = message
        };
    }
}
