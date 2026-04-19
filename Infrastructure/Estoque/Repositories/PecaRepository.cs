using Domain.Estoque.Entities;
using Domain.Estoque.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Estoque.Repositories;

public class PecaRepository : IPecaRepository
{
    private readonly AppDbContext _context;

    public PecaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Peca>> ObterTodos()
        => await _context.Pecas.ToListAsync();

    public async Task<Peca?> ObterPorId(Guid id)
		{
        return _context.Pecas.FirstOrDefault(p => p.Id == id);
    }

    public async Task Adicionar(Peca peca)
    {
        await _context.Pecas.AddAsync(peca);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Peca peca)
    {
        _context.Pecas.Update(peca);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var peca = await _context.Pecas.FindAsync(id);
        if (peca != null)
        {
            _context.Pecas.Remove(peca);
            await _context.SaveChangesAsync();
        }
    }

		public async Task SaveChangesAsync()
		{
        await _context.SaveChangesAsync();
    }
}
