namespace Application.OrdensServico.Interfaces;

using Application.OrdensServico.DTOs;

public interface IVeiculoService
{
    public Task<IEnumerable<VeiculoResponseDto>> GetAll();
    public Task<VeiculoResponseDto> GetById(Guid id);
    public Task Create(VeiculoCreateDto dto);
    public Task Update(Guid id, VeiculoUpdateDto dto);
    public Task Delete(Guid id);
}
