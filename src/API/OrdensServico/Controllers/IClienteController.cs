using Gashu.SistemaMecanica.API.OrdensServico.Presenters;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public interface IClienteController
{
    public Task<OutputClientes> Get();
    public Task<OutputCliente> Get(Guid id);
    public Task<OutputCliente> GetByNome(string nome);
    public Task<OutputMessage> CriaCliente(ClienteCreateDto dto);
    public Task<OutputMessage> AtualizaCliente(Guid id, ClienteUpdateDto dto);
    public Task<OutputMessage> DeletaCliente(Guid id);
}
