namespace Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public interface IVeiculoService
{
    public Task<IEnumerable<VeiculoResponseDto>> GetAll();
    public Task<VeiculoResponseDto> GetById(Guid id);
    public Task<VeiculoResponseDto> GetByPlaca(string placa);
    public Task Create(VeiculoCreateDto dto);
    public Task Update(Guid id, VeiculoUpdateDto dto);
    public Task Delete(Guid id);
}
