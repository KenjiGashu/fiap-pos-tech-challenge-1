namespace Gashu.SistemaMecanica.Tests.Domain.OrdensServico;

using global::Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

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
    public async Task VeiculoCreate_WhenInvalidPlaca_ThrowException()
    {
        Assert.Throws<Exception>(() => new Veiculo("TKJ5AOO", "marca", "modelo", 1000));
    }

    [Fact]
    public async Task VeiculoCreate_WhenInvalidPlacaAntiga_ThrowException()
    {
        Assert.Throws<Exception>(() => new Veiculo("ASK-IIII", "marca", "modelo", 1000));
    }

    [Fact]
    public async Task VeiculoCreate_ShouldHaveCorrectFields()
    {
        var veiculo = new Veiculo("TKJ5A20", "marca", "modelo", 1999);

        Assert.Equal("TKJ5A20", veiculo.Placa);
        Assert.Equal("marca", veiculo.Marca);
        Assert.Equal("modelo", veiculo.Modelo);
        Assert.Equal(1999, veiculo.Ano);
    }

    [Fact]
    public async Task VeiculoCreate_PlacaAntiga_ShouldHaveCorrectFields()
    {
        var veiculo = new Veiculo("JSK-1234", "marca", "modelo", 1999);

        Assert.Equal("JSK-1234", veiculo.Placa);
        Assert.Equal("marca", veiculo.Marca);
        Assert.Equal("modelo", veiculo.Modelo);
        Assert.Equal(1999, veiculo.Ano);
    }

}
