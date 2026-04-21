namespace Domain.Notificacao.Interfaces;

public interface ICanalNotificacao
{
	public Task EnviarMensagem(string para, string titulo, string corpo);
}
