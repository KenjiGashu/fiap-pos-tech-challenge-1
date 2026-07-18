using Gashu.SistemaMecanica.Application.Notificacao.Services;
using Gashu.SistemaMecanica.Application.Notificacao.Services;

namespace Gashu.SistemaMecanica.Tests.Integration.FakeServices;

public class NotificacaoFakeService : INotificacaoService
{
    public async Task EnviarOrcamento(AprovacaoOrcamentoDto dto)
    {
        Console.WriteLine($"Email enviado!");
    }

    public async Task EnviarMensagem(string destinatario, string titulo, string mensagem)
    {
        Console.WriteLine($"Mensagem enviada!");
    }

}
