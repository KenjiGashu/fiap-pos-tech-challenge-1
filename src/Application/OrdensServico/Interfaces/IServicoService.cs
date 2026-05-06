namespace Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public interface IServicoService
{
    public Task<IEnumerable<ServicoResponseDto>> GetAll();
    public Task<ServicoResponseDto?> GetById(Guid id);
    public Task Create(ServicoCreateDto dto);
    public Task Update(Guid id, ServicoUpdateDto dto);
    public Task Delete(Guid id);
}
