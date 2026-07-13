using Gashu.SistemaMecanica.Application.Notificacao.DTOs;
using Gashu.SistemaMecanica.Application.Notificacao.Interfaces;

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
