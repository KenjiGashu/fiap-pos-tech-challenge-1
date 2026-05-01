using Application.Notificacao.DTOs;
using Application.Notificacao.Interfaces;

namespace Tests.Integration.FakeServices;

public class NotificacaoFakeService : INotificacaoService
{
    public async Task EnviarOrcamento(AprovacaoOrcamentoDto dto)
    {
        Console.WriteLine($"Email enviado!");
    }
}
