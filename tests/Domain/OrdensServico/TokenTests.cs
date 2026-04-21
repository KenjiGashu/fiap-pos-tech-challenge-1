namespace Tests.Domain.OrdensServico;

using global::Domain.OrdensServico.Entities;

public class TokenTests
{
    [Fact]
    public async Task Token_WhenAlreadyConsumed_ShouldReturnInvalid()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(1), ordemServicoId);


				token.ConsumirToken();

        Assert.Equal(token.IsValid(), false);
    }

	  [Fact]
    public async Task Token_WhenExpired_ShouldReturnInvalid()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(-1), ordemServicoId);

				token.ConsumirToken();

        Assert.Equal(token.IsValid(), false);
    }


	  [Fact]
    public async Task Token_WhenExpired_ShouldReturnExpired()
    {
        var tokenGuid = Guid.NewGuid();
        var ordemServicoId = Guid.NewGuid();
        var token = new Token(tokenGuid.ToString() , DateTime.Now.AddDays(-1), ordemServicoId);

				token.ConsumirToken();

        Assert.Equal(token.IsExpired(), true);
    }
}

