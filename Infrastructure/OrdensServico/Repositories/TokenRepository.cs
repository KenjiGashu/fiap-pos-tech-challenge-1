namespace Infrastructure.OrdensServico.Repositories;

using Infrastructure.Data;
using Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;

public class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _context;

		public TokenRepository(AppDbContext context)
		{
        _context = context;
    }

    public async Task SalvaToken(string token, Guid ordemServicoId)
{
    _context.Tokens.Add(new Token(token, DateTime.Now.AddDays(2), ordemServicoId));
    await _context.SaveChangesAsync();
}

public async Task<Token?> ObterToken(string token)
{
    return _context.Tokens.FirstOrDefault(t => t.HashedToken == token);
}
}



