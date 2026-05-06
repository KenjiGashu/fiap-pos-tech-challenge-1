namespace Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface IClienteService
{
    public Task<IEnumerable<ClienteResponseDto>> GetAll();
    public Task<ClienteResponseDto> GetById(Guid id);
    public Task<ClienteResponseDto> GetByNome(string nome);
    public Task Create(ClienteCreateDto dto);
    public Task Update(Guid id, ClienteUpdateDto dto);
    public Task Delete(Guid id);
}
