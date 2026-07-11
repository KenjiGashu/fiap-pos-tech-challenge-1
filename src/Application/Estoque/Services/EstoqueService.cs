namespace Gashu.SistemaMecanica.Application.Estoque.Services;

using Gashu.SistemaMecanica.Application.Estoque.DTOs;
using Gashu.SistemaMecanica.Application.Estoque.Interfaces;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;
using Gashu.SistemaMecanica.Domain.Estoque.Interfaces;

public class EstoqueService : IEstoqueService
{
    private readonly IPecaRepository _repo;

    public EstoqueService(IPecaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Peca>> GetAll()
    {
        var lista = await _repo.ObterTodos();
        return lista;
    }

    public async Task<Peca?> GetById(Guid id)
    {
        var peca = await _repo.ObterPorId(id);

        if (peca == null) return null;

        return peca;
    }
    

    public async Task Create(PecaCreateDto dto)
    {
        var peca = new Peca(dto.Nome, dto.Preco, dto.Quantidade);
        await _repo.Adicionar(peca);
    }

    public async Task Update(Guid id, PecaUpdateDto dto)
    {
        var peca = await _repo.ObterPorId(id);

        if (peca == null)
            throw new Exception("Peca não encontrada");

        peca.Atualizar(dto.Nome, dto.Preco, dto.Quantidade);

        await _repo.Atualizar(peca);
    }

    public async Task Delete(Guid id)
        => await _repo.Remover(id);

    public async Task Adicionar(Guid id, int quantidade)
    {
        var peca = await _repo.ObterPorId(id);

        if(peca == null)
            throw new Exception("Peca nao encontrada");

        peca.Adicionar(quantidade);

        await _repo.SaveChangesAsync();
    }

    public async Task Consumir(Guid id, int quantidade)
    {
        var peca = await _repo.ObterPorId(id);

        if(peca == null)
            throw new Exception("Peca nao encontrada");

        peca.Consumir(quantidade);

        await _repo.SaveChangesAsync();
    }

}
