namespace Infrastructure.Identidade.Repositories;

using Domain.Identidade.Entities;
using Domain.Identidade.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ObterTodos()
        => _context.Usuarios.Include(u => u.Roles).ToList();

    public async Task<Usuario?> ObterPorId(Guid id)
    {
        return _context.Usuarios.FirstOrDefault(p => p.Id == id);
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return _context.Usuarios.Include(u => u.Roles).FirstOrDefault(p => p.Email == email);
    }

    public async Task Adicionar(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

		public async Task SaveChangesAsync()
		{
        await _context.SaveChangesAsync();
    }
   
}
