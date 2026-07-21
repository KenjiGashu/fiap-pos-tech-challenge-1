namespace Gashu.SistemaMecanica.API.Notificacao.Presenters;

public class OutputConfirmacaoEmail
{
    public string Message { get; set; }
}

public interface IConfirmacaoEmailPresenter
{
    public OutputConfirmacaoEmail Present(string message);
}
