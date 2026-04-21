namespace Tests.Application.Etoque;

using global::Application.Estoque.Interfaces;
using global::Application.Estoque.Services;
using global::Domain.Estoque.Entities;
using global::Domain.Estoque.Interfaces;
using Moq;

public class EstoqueServiceTests
{
    IEstoqueService estoqueService;
    Mock<IPecaRepository> _mockRepo;

    public EstoqueServiceTests()
    {
        _mockRepo = new Mock<IPecaRepository>();
        estoqueService = new EstoqueService(_mockRepo.Object);
    }

    [Fact]
    public async Task AdicionarTest()
    {
        var guid = Guid.NewGuid();
        var expectedQuantidade = 2;
        var peca = new Peca( "peca", 10, 1 );
        _mockRepo.Setup(r => r.ObterPorId(It.Is<Guid>(g => g == guid))).ReturnsAsync(peca);
        _mockRepo.Setup(r => r.SaveChangesAsync());

        await estoqueService.Adicionar(guid, 1);

        Assert.Equal(expectedQuantidade, peca.Quantidade);
        _mockRepo.Verify(r => r.ObterPorId(guid), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ConsumirTest()
    {
        var guid = Guid.NewGuid();
        var expectedQuantidade = 0;
        var peca = new Peca( "peca", 10, 1 );
        _mockRepo.Setup(r => r.ObterPorId(It.Is<Guid>(g => g == guid))).ReturnsAsync(peca);
        _mockRepo.Setup(r => r.SaveChangesAsync());

        await estoqueService.Consumir(guid, 1);

        Assert.Equal(expectedQuantidade, peca.Quantidade);
        _mockRepo.Verify(r => r.ObterPorId(guid), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
