using Gashu.SistemaMecanica.API.OrdensServico.Presenters;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public interface IServicoController
{
    public Task<OutputServicos> GetAll();
    public Task<OutputServico> GetById(Guid id);
    public Task<OutputMessageServico> Create(ServicoCreateDto dto);
    public Task<OutputMessageServico> Update(Guid id, ServicoUpdateDto dto);
    public Task<OutputMessageServico> Delete(Guid id);
}
