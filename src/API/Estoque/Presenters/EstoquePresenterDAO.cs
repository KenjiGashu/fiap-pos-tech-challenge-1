using Gashu.SistemaMecanica.Application.Estoque.DTOs;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;

namespace Gashu.SistemaMecanica.API.Estoque.Presenters;

/// <inheritdoc/>
public class EstoquePresenterDAO : IEstoquePresenter
{
    /// <inheritdoc/>
    public OutputEstoque Present()
    {
        return new OutputEstoque
        {
            message = "",
            peca = null
        };
    }
    
    /// <inheritdoc/>
    public OutputEstoque Present(string message)
    {
        return new OutputEstoque
        {
            message = message,
            peca = null
        };
    }

    /// <inheritdoc/>
    public OutputEstoque Present(string message, Peca response)
    {
        return new OutputEstoque
        {
            message = message,
            peca = new PecaResponseDto
            {
                Id = response.Id,
                Nome = response.Nome,
                Preco = response.Preco,
                Quantidade = response.Quantidade
            }
        };
    }

    /// <inheritdoc/>
    public OutputEstoqueList Present(string message, IEnumerable<Peca> response)
    {
        return new OutputEstoqueList
        {
            message = message,
            pecas = response.Select(p => new PecaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Quantidade = p.Quantidade
            })
        };
    }
}
