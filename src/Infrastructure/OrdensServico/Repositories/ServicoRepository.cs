using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.OrdensServico.Repositories;

public class ServicoRepository : IServicoRepository
{
    private readonly AppDbContext _context;

    public ServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Servico>> ObterTodos()
        => await _context.Servicos.ToListAsync();

    public async Task<Servico?> ObterPorId(Guid id)
        => await _context.Servicos.FindAsync(id);

    public async Task Adicionar(Servico cliente)
    {
        await _context.Servicos.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Servico cliente)
    {
        _context.Servicos.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var cliente = await _context.Servicos.FindAsync(id);
        if (cliente != null)
        {
            _context.Servicos.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
