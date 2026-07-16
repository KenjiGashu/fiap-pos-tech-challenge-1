using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Presenters;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public class VeiculoController : IVeiculoController
{
    private readonly IVeiculoService _service;
    private readonly IVeiculoPresenter _presenter;

    public VeiculoController(IVeiculoService service, IVeiculoPresenter presenter)
    {
        _service = service;
        _presenter = presenter;
    }

    public async Task<OutputVeiculos> GetAll()
    {
        var veiculos = await _service.GetAll();
        return _presenter.Present("Obtido todos os veiculos com sucesso", veiculos);
    }

    public async Task<OutputVeiculo> GetById(Guid id)
    {
        var veiculo = await _service.GetById(id);
        return _presenter.Present("Obtido veiculo com sucesso", veiculo);
    }

    public async Task<OutputVeiculo> GetByPlaca(string placa)
    {
        var veiculo = await _service.GetByPlaca(placa);
        return _presenter.Present("Obtido veiculo com sucesso", veiculo);
    }

    public async Task<OutputMessageVeiculo> Create([FromBody] VeiculoCreateDto dto)
    {
        await _service.Create(dto);
        return _presenter.Present("Veiculo criado com sucesso!");
    }

    public async Task<OutputMessageVeiculo> Update(Guid id, [FromBody] VeiculoUpdateDto dto)
    {
        await _service.Update(id, dto);
        return _presenter.Present("Veiculo atualizado com sucesso!");
    }

    public async Task<OutputMessageVeiculo> Delete(Guid id)
    {
        await _service.Delete(id);
        return _presenter.Present("Veiculo deletado com sucesso!");
    }
}
