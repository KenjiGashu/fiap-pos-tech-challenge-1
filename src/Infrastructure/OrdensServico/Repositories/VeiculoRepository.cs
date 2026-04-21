using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.OrdensServico.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly AppDbContext _context;

    public VeiculoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Veiculo>> ObterTodos()
        => await _context.Veiculos.ToListAsync();

    public async Task<Veiculo> ObterPorId(Guid id)
        => await _context.Veiculos.FindAsync(id);

    public async Task Adicionar(Veiculo veiculo)
    {
        await _context.Veiculos.AddAsync(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Veiculo veiculo)
    {
        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var v = await ObterPorId(id);
        if (v != null)
        {
            _context.Veiculos.Remove(v);
            await _context.SaveChangesAsync();
        }
    }
}
