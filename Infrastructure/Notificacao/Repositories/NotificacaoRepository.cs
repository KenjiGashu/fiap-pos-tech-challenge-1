namespace Infrastructure.Notificacao.Repositories;

using System.Threading.Tasks;
using Domain.Notificacao.Interfaces;
using Domain.Notificacao.Entities;
using Infrastructure.Data;

public class NotificacaoRepository : INotificacaoRepository
{
	  private readonly AppDbContext _context;

    public NotificacaoRepository(AppDbContext context)
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
