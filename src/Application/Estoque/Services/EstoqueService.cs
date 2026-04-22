namespace Application.Estoque.Services;

using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Domain.Estoque.Entities;
using Domain.Estoque.Interfaces;

public class EstoqueService : IEstoqueService
{
    private readonly IPecaRepository _repo;

    public EstoqueService(IPecaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PecaResponseDto>> GetAll()
    {
        var lista = await _repo.ObterTodos();

        return lista.Select(p => new PecaResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Quantidade = p.Quantidade
        });
    }

    public async Task<Peca?> GetById(Guid id)
        => await _repo.ObterPorId(id);

    public async Task Create(PecaCreateDto dto)
    {
        var peca = new Peca(dto.Nome, dto.Preco, dto.Quantidade);
        await _repo.Adicionar(peca);
    }

    public async Task Update(Guid id, PecaUpdateDto dto)
    {
        var peca = await _repo.ObterPorId(id);

        if (peca == null)
            throw new Exception("Cliente não encontrado");

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
