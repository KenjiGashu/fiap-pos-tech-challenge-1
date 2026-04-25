using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.OrdensServico.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObterTodos()
        => await _context.Clientes.Include(c => c.Usuario).ToListAsync();

    public async Task<Cliente?> ObterPorId(Guid id)
        => await _context.Clientes.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Cliente?> ObterPorNome(string nome)
        => await _context.Clientes.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Nome == nome);

    public async Task Adicionar(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
