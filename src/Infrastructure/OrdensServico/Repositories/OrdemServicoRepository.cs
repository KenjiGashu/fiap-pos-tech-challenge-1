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

    public async Task Criar(OrdemServico os)
    {
        _context.OrdemServicos.Add(os);

        _context.SaveChanges();
    }

    public async Task AdicionarPecas(OrdemServico os)
    {
        var idsExistentes = _context.OrdemServicoPecas
            .Where(x => x.OrdemServicoId == os.Id)
            .Select(x => x.PecaId)
            .ToList();

        var novos = os.OrdemServicoPecas
            .Where(p => !idsExistentes.Contains(p.PecaId))
            .ToList();
            
        await _context.OrdemServicoPecas.AddRangeAsync(novos);
            
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrdemServico>> ObterTodos()
    {
        return await _context.OrdemServicos
            .Include(o => o.OrdemServicoServicos)
            .ThenInclude(s => s.Servico)
            .Include(o => o.OrdemServicoPecas)
            .ThenInclude(p => p.Peca)
            .ToListAsync();
    }

    public async Task<OrdemServico> ObterPorId(Guid id)
    {
        var ordemServico = await _context.OrdemServicos
            .Include(o => o.OrdemServicoServicos)
            .ThenInclude(oss => oss.Servico)
            .Include(o => o.OrdemServicoPecas)
            .ThenInclude(osp => osp.Peca)
            .AsTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

        if(ordemServico == null)
            throw new Exception("ordem servico nao encontrada");

        return ordemServico;
    }

    public async Task<IEnumerable<OrdemServico>> ObterPorIdCliente(Guid clienteId)
    {
        IEnumerable<OrdemServico> ordemServicos = _context.OrdemServicos
            .Include(o => o.OrdemServicoServicos)
            .ThenInclude(oss => oss.Servico)
            .Include(o => o.OrdemServicoPecas)
            .ThenInclude(osp => osp.Peca)
            .AsTracking()
            .Where(o => o.ClienteId == clienteId);

        return ordemServicos;
    }


    public async Task Atualizar(OrdemServico os)
    {
        //_context.OrdemServicos.Update(os);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Guid id)
    {
        var os = await _context.OrdemServicos.FindAsync(id);
        if (os != null)
        {
            _context.OrdemServicos.Remove(os);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AdicionarPecas(Guid id, IEnumerable<OrdemServicoPeca> pecas)
    {
        var ordemServico = _context.OrdemServicos
            .Include(os => os.OrdemServicoPecas)
            .Include(os => os.OrdemServicoServicos)
            .FirstOrDefault(os => os.Id == id);

        if (ordemServico == null)
            throw new Exception("Cliente não encontrado");

        foreach( var p in pecas)
            {
                _context.Entry(p).State = EntityState.Added;
                ordemServico.OrdemServicoPecas.Add(p);
            }
            
        _context.SaveChanges();
    }

    public async Task AdicionarServicos(OrdemServico os)
    {
        var idsExistentes = _context.OrdemServicoServicos
            .Where(x => x.OrdemServicoId == os.Id)
            .Select(x => x.ServicoId)
            .ToList();

        var novos = os.OrdemServicoServicos
            .Where(s => !idsExistentes.Contains(s.ServicoId))
            .ToList();
            
        await _context.OrdemServicoServicos.AddRangeAsync(novos);
            
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        var entries = _context.ChangeTracker.Entries();

        // var entry = _context.Entry(ordemServico);
        // Console.WriteLine(entry.State);

        foreach (var e in entries)
            {
                Console.WriteLine($"{e.Entity.GetType().Name} - {e.State}");
            }
            
        await _context.SaveChangesAsync();
    }

    
}
