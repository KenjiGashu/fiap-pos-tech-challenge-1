namespace Tests.Application.Notificacao;

using global::Application.Notificacao.Interfaces;
using global::Application.Notificacao.Services;
using global::Application.Notificacao.DTOs;
using global::Domain.Notificacao.Interfaces;
using Moq;

public class NotificacaoServiceTests
{
    INotificacaoService notificacaoService;
    Mock<ICanalNotificacao> _mockCanalNotificacao;

    public NotificacaoServiceTests()
    {
        _mockCanalNotificacao = new Mock<ICanalNotificacao>();
        notificacaoService = new NotificacaoService(_mockCanalNotificacao.Object);
    }

    [Fact]
    public async Task AdicionarTest()
    {
        var servicos = new List<ItemOrcamentoDto>();
        servicos.Add(new ItemOrcamentoDto
        {
            Nome = "troca oleo",
            Preco = 12,
            Quantidade = 1
        });

        var pecas = new List<ItemOrcamentoDto>();
        pecas.Add(new ItemOrcamentoDto
        {
            Nome = "oleo",
            Preco = 12,
            Quantidade = 1
        });

        var total = 24;
        var nomeCliente = "maria";
        var destinatario = "maria@gmail.com";
        var orcamentoDto = new AprovacaoOrcamentoDto
        {
            OrdemServicoId = Guid.NewGuid(),
            TokenGuid = Guid.NewGuid().ToString("n"),
            Servicos = servicos,
            Pecas = pecas,
            Total = total,
            NomeCliente = nomeCliente,
            Destinatario = destinatario
        };

        _mockCanalNotificacao.Setup(c => c.EnviarMensagem(It.Is<string>(s => s == destinatario), It.IsAny<string>(), It.IsAny<string>()));

        await notificacaoService.EnviarOrcamento(orcamentoDto);

        _mockCanalNotificacao.Verify(r => r.EnviarMensagem(It.Is<string>(s => s == destinatario), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
