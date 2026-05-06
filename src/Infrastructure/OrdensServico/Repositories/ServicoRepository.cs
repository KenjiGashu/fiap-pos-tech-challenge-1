using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gashu.SistemaMecanica.Infrastructure.OrdensServico.Repositories;

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

    public async Task Adicionar(Servico servico)
    {
        await _context.Servicos.AddAsync(servico);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Servico servico)
    {
        _context.Servicos.Update(servico);
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
