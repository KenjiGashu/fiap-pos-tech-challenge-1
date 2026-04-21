namespace Tests.Domain.Estoque;

using global::Domain.Estoque.Entities;

public class PecaTests
{
	  [Fact]
	  public async Task CreatePeca_WhenEmptyNome_ThrowException()
	  {
        var invalidNome = "";

        Assert.Throws<Exception>(() => new Peca(invalidNome, 10, 1));
    }

	  [Fact]
	  public async Task CreatePeca_WhenInvalidQuantity_ThrowException()
	  {
        var invalidQuantidade = 0;

        Assert.Throws<Exception>(() => new Peca("peca", 10, invalidQuantidade));
    }

	  [Fact]
	  public async Task CreatePeca_WhenInvalidPreco_ThrowException()
	  {
        var invalidPreco = 0;

        Assert.Throws<Exception>(() => new Peca("peca", invalidPreco, 1));
    }


	  [Fact]
	  public async Task PecaAdicionarTest()
	  {
        var expectedQuantidade = 2;
        var peca = new Peca("peca", 12, 1);
        peca.Adicionar(1);

        Assert.Equal(expectedQuantidade, peca.Quantidade);
    }

		[Fact]
	  public async Task PecaConsumirTest()
	  {
        var expectedQuantidade = 0;
        var peca = new Peca("peca", 12, 1);
        peca.Consumir(1);

        Assert.Equal(expectedQuantidade, peca.Quantidade);
    }
}
