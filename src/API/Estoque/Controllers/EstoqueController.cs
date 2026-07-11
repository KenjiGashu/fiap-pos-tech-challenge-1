namespace Gashu.SistemaMecanica.API.Estoque.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Gashu.SistemaMecanica.API.Estoque.Presenters;

/// <inheritdoc/>
public class EstoqueController : IEstoqueController
{
    private readonly IEstoqueService _service;
    private readonly IEstoquePresenter _presenter;

    /// <summary>
    /// construtor de EstoqueController
    /// </summary>
    public EstoqueController(IEstoqueService service, IEstoquePresenter presenter)
    {
        _service = service;
        _presenter = presenter;
    }

    /// <inheritdoc/>
    public async Task<OutputEstoqueList> Get()
    {
        var pecas = await _service.GetAll();
        return _presenter.Present("Pecas encontradas com sucesso", pecas);
    }

    /// <inheritdoc/>
    public async Task<OutputEstoque> GetById(Guid id)
    {
        var peca = await _service.GetById(id);

        if (peca == null)
            return _presenter.Present("Peca nao encontrada");

        return _presenter.Present("Peca encontrada", peca);
    }

    /// <inheritdoc/>
    public async Task<OutputEstoque> Create(PecaCreateDto dto)
    {
        await _service.Create(dto);

        return _presenter.Present("Peça cadastrada com sucesso!");
    }

    /// <inheritdoc/>
    public async Task<OutputEstoque> Update(Guid id, PecaUpdateDto dto)
    {
        await _service.Update(id, dto);

        return _presenter.Present("Peça atualizada com sucesso!");
    }

    /// <inheritdoc/>
    public async Task<OutputEstoque> Delete(Guid id)
    {
        await _service.Delete(id);

        return _presenter.Present("Peça deletada com sucesso!");
    }
}
