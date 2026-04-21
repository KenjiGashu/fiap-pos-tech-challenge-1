namespace Tests.Domain.OrdensServico;

using global::Domain.OrdensServico.Entities;

public class ServicoTests
{
	[Fact]
	public async Task ServicoCreate_WhenNoNome_ThrowException()
	{
		Assert.Throws<Exception>(() => new Servico("", 1));
	}

  [Fact]
	public async Task ServicoCreate_WhenInvalidPreco_ThrowException()
	{
		Assert.Throws<Exception>(() => new Servico("TKJ5A20", -1));
	}

  [Fact]
	public async Task ServicoCreate_ShouldHaveCorrectFields()
	{
        var expectedNome = "TKJ5A20";
        decimal expectedPreco = 1999;
        var servico = new Servico(expectedNome, expectedPreco);

				Assert.Equal(servico.Nome, expectedNome);
				Assert.Equal(servico.Preco, expectedPreco);
	}
}
