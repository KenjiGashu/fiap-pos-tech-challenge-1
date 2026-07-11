namespace Gashu.SistemaMecanica.Application.Estoque.Interfaces;

using Gashu.SistemaMecanica.Application.Estoque.DTOs;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;

public interface IEstoqueService
{
    public Task<IEnumerable<Peca>> GetAll();

    public Task<Peca?> GetById(Guid id);

    public Task Create(PecaCreateDto dto);

    public Task Update(Guid id, PecaUpdateDto dto);

    public Task Delete(Guid id);

    public Task Adicionar(Guid id, int quantidade);

    public Task Consumir(Guid id, int quantidade);

}
