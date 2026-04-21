namespace Tests;

using Domain.OrdensServico.Entities;

public class TokenTests
{
	[Fact]
	public async Task Token_WhenAlreadyConsumed_ShouldReturnInvalid()
	{
		var token = new Token(Guid.NewGuid())
	}
}
