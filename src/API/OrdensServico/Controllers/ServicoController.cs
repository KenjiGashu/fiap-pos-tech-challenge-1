namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class ServicoController : IServicoController
{
    private readonly IServicoService _service;
    private readonly IServicoPresenter _presenter;

    public ServicoController(IServicoService service, IServicoPresenter presenter)
    {
        _service = service;
        _presenter = presenter;
    }

    public async Task<OutputServicos> GetAll()
    {
        var output = await _service.GetAll();
        return _presenter.Present("Servicos encontrados com sucesso.", output);
    }

    public async Task<OutputServico> GetById(Guid id)
    {
        var servico = await _service.GetById(id);
        return _presenter.Present("Servico Obtido com sucesso.", servico);
    }

    public async Task<OutputMessageServico> Create(ServicoCreateDto dto)
    {
        await _service.Create(dto);
        return _presenter.Present("Serviço criado com sucesso!");
    }

    public async Task<OutputMessageServico> Update(Guid id, ServicoUpdateDto dto)
    {
        await _service.Update(id, dto);
        return _presenter.Present("Serviço atualizado com sucesso!");
    }

    public async Task<OutputMessageServico> Delete(Guid id)
    {
        await _service.Delete(id);
        return _presenter.Present("Servico deletado com sucesso!");
    }
}
