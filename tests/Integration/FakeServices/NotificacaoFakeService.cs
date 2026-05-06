using Gashu.SistemaMecanica.Application.Notificacao.DTOs;
using Gashu.SistemaMecanica.Application.Notificacao.Interfaces;

namespace Gashu.SistemaMecanica.Tests.Integration.FakeServices;

public class NotificacaoFakeService : INotificacaoService
{
    public async Task EnviarOrcamento(AprovacaoOrcamentoDto dto)
    {
        Console.WriteLine($"Email enviado!");
    }
}
