using Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;
using Domain.Estoque.Interfaces;
using Domain.OrdensServico.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.OrdensServico.Repositories;

public class OrdemServicoRepository : IOrdemServicoRepository
{
    private readonly AppDbContext _context;

    public OrdemServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(OrdemServico os)
    {
        await _context.OrdemServicos.AddAsync(os);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrdemServico>> ObterTodos()
    {
        return await _context.OrdemServicos
            .Include(o => o.Servicos)
            .Include(o => o.Pecas)
            .ToListAsync();
    }

    public async Task<OrdemServico> ObterPorId(Guid id)
    {
        OrdemServico? ordemServico = await _context.OrdemServicos
            .Include(o => o.Servicos)
            .Include(o => o.Pecas)
            .FirstOrDefaultAsync(o => o.Id == id);
        return ordemServico;
    }

    public async Task Atualizar(OrdemServico os)
    {
        _context.OrdemServicos.Update(os);
        await _context.SaveChangesAsync();
    }

}
