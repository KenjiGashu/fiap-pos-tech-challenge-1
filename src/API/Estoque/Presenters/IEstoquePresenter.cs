using Gashu.SistemaMecanica.Application.Estoque.Services;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;

namespace Gashu.SistemaMecanica.API.Estoque.Presenters;

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputEstoque
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// peca DTO
    /// </summary>
    public PecaResponseDto? peca { get; set; }
}

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputEstoqueList
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// lista de pecas DTO
    /// </summary>
    public IEnumerable<PecaResponseDto> pecas { get; set; }
}

/// <summary>
/// Adaptor que tem a função de Presenter do clean arch
/// </summary>
public interface IEstoquePresenter
{
    /// <summary>
    /// Converter retorno do controller para string json
    /// quando nao encontrou nenhuma peca
    /// </summary>
    public OutputEstoque Present();
    
    /// <summary>
    /// Converter retorno do controller para string json
    /// </summary>
    public OutputEstoque Present(string message);

    /// <summary>
    /// Converter retorno do controller (PecaResponseDto) para string json
    /// </summary>
    public OutputEstoque Present(string message, Peca response);

    /// <summary>
    /// Converter retorno do controller (IEnumerable<PecaResponseDto>) para string json
    /// </summary>
    public OutputEstoqueList Present(string message, IEnumerable<Peca> response);
}
