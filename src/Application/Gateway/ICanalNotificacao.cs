namespace Gashu.SistemaMecanica.Application.Gateway;

public interface ICanalNotificacao
{
	public Task EnviarMensagem(string para, string titulo, string corpo);
}
