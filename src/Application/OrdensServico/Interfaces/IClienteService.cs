namespace Application.OrdensServico.Interfaces;

using Application.OrdensServico.DTOs;
using Domain.OrdensServico.Entities;

public interface IClienteService
{
    public Task<IEnumerable<ClienteResponseDto>> GetAll();
    public Task<ClienteResponseDto> GetById(Guid id);
    public Task Create(ClienteCreateDto dto);
    public Task Update(Guid id, ClienteUpdateDto dto);
    public Task Delete(Guid id);
}
