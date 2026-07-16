namespace Gashu.SistemaMecanica.API.Notificacao.Presenters;

public class OutputConfirmacaoEmail
{
    public string Message;
}

public interface IConfirmacaoEmailPresenter
{
    public OutputConfirmacaoEmail Present(string message);
}
