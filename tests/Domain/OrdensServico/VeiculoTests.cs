namespace Tests.Domain.OrdensServico;

using global::Domain.OrdensServico.Entities;

public class VeiculoTests
{
	[Fact]
	public async Task VeiculoCreate_WhenNoPlaca_ThrowException()
	{
		Assert.Throws<Exception>(() => new Veiculo("", "marca", "modelo", 1999));
	}

  [Fact]
	public async Task VeiculoCreate_WhenInvalidAno_ThrowException()
	{
		Assert.Throws<Exception>(() => new Veiculo("TKJ5A20", "marca", "modelo", 1000));
	}

  [Fact]
	public async Task VeiculoCreate_ShouldHaveCorrectFields()
	{
		var veiculo = new Veiculo("TKJ5A20", "marca", "modelo", 1999);

		Assert.Equal(veiculo.Placa, "TKJ5A20");
		Assert.Equal(veiculo.Marca, "marca");
		Assert.Equal(veiculo.Modelo, "modelo");
		Assert.Equal(veiculo.Ano, 1999);
	}
}
