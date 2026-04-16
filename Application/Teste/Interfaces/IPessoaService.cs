namespace Application.Teste.Interfaces;

using Application.Teste.DTOs;
using Domain.Teste.Entities;

public interface IPessoaService
{
    public Task<IEnumerable<PessoaResponseDto>> GetAll();

    public Task<IEnumerable<Blog>> GetAllBlogs();

    public Task<PessoaResponseDto?> GetById(Guid id);

    public Task Create(PessoaDto dto);

    public Task Update(Guid id, PessoaDto dto);

    public Task Delete(Guid id);

    public Task AdicionaPedido(Guid id, PedidoDto dto);

    public Task AdicionaPedidos(Guid id, IEnumerable<PedidoDto> dtos);
}
