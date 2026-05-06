namespace Gashu.SistemaMecanica.Tests.Domain.OrdensServico;

using global::Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public class TokenTests
{
    [Fact]
    public async Task Token_WhenAlreadyConsumed_ShouldReturnInvalid()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(1), ordemServicoId);


				token.ConsumirToken();

        Assert.False(token.IsValid());
    }

	  [Fact]
    public async Task Token_WhenExpired_ShouldReturnInvalid()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(-1), ordemServicoId);

				token.ConsumirToken();

        Assert.False(token.IsValid());
    }


	  [Fact]
    public async Task Token_WhenExpired_ShouldReturnExpired()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(-1), ordemServicoId);

				token.ConsumirToken();

        Assert.True(token.IsExpired());
    }
}

