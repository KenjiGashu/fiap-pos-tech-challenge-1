namespace Application.OrdensServico.Services;

using Domain.Notificacao.Entities;
using Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;
using Application.OrdensServico.Interfaces;

public class TokenService : ITokenService
{
	  private readonly ITokenRepository _repo;

	  public TokenService(ITokenRepository repo)
	  {
        _repo = repo;
    }
	
	public async Task<Token> GeraToken(Guid ordemServicoId)
	{
		string tokenStr = Guid.NewGuid().ToString("n");

		Token token = new Token(tokenStr, DateTime.Now.AddDays(2), ordemServicoId);

		await _repo.SalvaToken(token.GuidToken, ordemServicoId);
		return token;
	}

	  public async Task<Token?> ObterTokenPorGuid(Guid id)
	  {
        Console.WriteLine($"[ObterTokenPorGuid] guid {id.ToString("n")}");
        Console.WriteLine($"[ObterTokenPorGuid] {Token.ComputeSha256Hash(id.ToString("n"))}");
        string hash = Token.ComputeSha256Hash(id.ToString("n"));
        return await _repo.ObterToken(hash);
    }
}
