using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.API.OrdensServico.Presenters;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public class ClienteController : IClienteController
{
    private readonly IClienteService _service;
    private readonly IClientePresenter _presenter;

    public ClienteController(IClienteService service, IClientePresenter presenter)
    {
        _service = service;
        _presenter = presenter;
    }

    public async Task<OutputClientes> Get()
    => _presenter.Present("Obteve clientes com sucesso", await _service.GetAll());

    public async Task<OutputCliente> Get(Guid id)
    {
        var cliente = await _service.GetById(id);

        if(cliente == null)
            return _presenter.Present("Cliente nao encontrado", cliente);

        return _presenter.Present("obteve cliente com sucesso", cliente);
    }

    public async Task<OutputCliente> GetByNome(string nome)
    {
        var cliente = await _service.GetByNome(nome);

        if (cliente == null)
            return _presenter.Present("Cliente nao encontrado", cliente);

        return _presenter.Present("Cliente encontrado", cliente);
    }

    public async Task<OutputMessage> CriaCliente(ClienteCreateDto dto)
    {
        await _service.Create(dto);
        return _presenter.Present( "Cliente criado com sucesso!" );
    }

    public async Task<OutputMessage> AtualizaCliente(Guid id, ClienteUpdateDto dto)
    {
        await _service.Update(id, dto);
        return _presenter.Present("Cliente atualizado com sucesso!" );
    }

    public async Task<OutputMessage> DeletaCliente(Guid id)
    {
        await _service.Delete(id);
        return _presenter.Present("Cliente deletado com sucesso");
    }
}
