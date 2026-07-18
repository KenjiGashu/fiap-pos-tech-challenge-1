namespace Gashu.SistemaMecanica.Application.Notificacao.Services;

public interface INotificacaoService
{
    public Task EnviarOrcamento(AprovacaoOrcamentoDto dto);
    public Task EnviarMensagem(string destinatario, string titulo, string mensagem);
}
