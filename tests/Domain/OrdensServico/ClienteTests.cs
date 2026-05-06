namespace Gashu.SistemaMecanica.Tests.Domain.OrdensServico;

using global::Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public class ClienteTests
{
	[Fact]
	public async Task WhenInvalidCpf_ConstructorThrowsException()
	{
		var cpfInvalido = "433.023.538-00";

		Assert.Throws<Exception>(() => new Cliente("nome", cpfInvalido, "", TipoPessoa.Fisica));
	}

  [Fact]
	public async Task WhenValidCpf_CreatesCliente()
	{
		var cpfValido = "433.023.538-20";
		
		var cliente = new Cliente("nome", cpfValido, "", TipoPessoa.Fisica);

		Assert.Equal(cliente.Cpf, cpfValido);
	}

  [Fact]
	public async Task WhenInvalidCnpj_ConstructorThrowsException()
	{
		var cnpjInvalido = "60.701.190/0001-00";

		Assert.Throws<Exception>(() => new Cliente("nome", "", cnpjInvalido, TipoPessoa.Juridica));
	}

  [Fact]
	public async Task WhenValidCnpj_CreatesCliente()
	{
		var cnpjValido = "60.701.190/0001-04";
		var cliente = new Cliente("nome", "", cnpjValido, TipoPessoa.Juridica);

		Assert.Equal(cliente.Cnpj, cnpjValido);
	}
}
