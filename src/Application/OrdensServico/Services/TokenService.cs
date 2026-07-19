namespace Gashu.SistemaMecanica.Application.OrdensServico.Services;

using Gashu.SistemaMecanica.Domain.Notificacao.Entities;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.Extensions.Logging;

public class TokenService : ITokenService
{
	  private readonly ITokenRepository _repo;
	  private readonly ILogger<TokenService> _logger;

	  public TokenService(ITokenRepository repo, ILogger<TokenService> logger)
	  {
        _repo = repo;
        _logger = logger;
    }
	
	public async Task<Token> GeraToken(Guid ordemServicoId)
	{
		string tokenStr = Guid.NewGuid().ToString("n");

		Token token = new Token(tokenStr, DateTime.UtcNow.AddDays(2), ordemServicoId);

		await _repo.SalvaToken(token.GuidToken, ordemServicoId);
		return token;
	}

	  public async Task<Token?> ObterTokenPorGuid(Guid id)
	  {
        _logger.LogDebug("[ObterTokenPorGuid] guid {TokenId}", id.ToString("n"));
        _logger.LogDebug("[ObterTokenPorGuid] {TokenHash}", Token.ComputeSha256Hash(id.ToString("n")));
        string hash = Token.ComputeSha256Hash(id.ToString("n"));
        return await _repo.ObterToken(hash);
    }
}
