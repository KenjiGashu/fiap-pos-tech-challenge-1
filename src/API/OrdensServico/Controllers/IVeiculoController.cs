using Gashu.SistemaMecanica.API.OrdensServico.Presenters;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public interface IVeiculoController
{
    public Task<OutputVeiculos> GetAll();
    public Task<OutputVeiculo> GetById(Guid id);
    public Task<OutputVeiculo> GetByPlaca(string placa);
    public Task<OutputMessageVeiculo> Create(VeiculoCreateDto dto);
    public Task<OutputMessageVeiculo> Update(Guid id, VeiculoUpdateDto dto);
    public Task<OutputMessageVeiculo> Delete(Guid id);
}
