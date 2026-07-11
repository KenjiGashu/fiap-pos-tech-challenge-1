namespace Gashu.SistemaMecanica.API.Estoque.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Estoque.Presenters;

/// <summary>
/// Controller responsável pelo gerenciamento de estoque
/// </summary>
/// <remarks>
/// Este contexto é responsável pelo controle de peças disponíveis em estoque,
/// incluindo cadastro, consulta, atualização e remoção.
/// Todos os endpoints requerem perfil Admin.
/// </remarks>
public interface IEstoqueController
{
    /// <summary>
    /// Lista todas as peças disponíveis no estoque
    /// </summary>
    /// <response code="200">Lista de peças retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    public Task<OutputEstoqueList> Get();

    /// <summary>
    /// Obtém uma peça do estoque pelo ID
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <response code="200">Peça encontrada</response>
    /// <response code="404">Peça não encontrada</response>
    public Task<OutputEstoque> GetById(Guid id);

    /// <summary>
    /// Cadastra uma nova peça no estoque
    /// </summary>
    /// <param name="dto">Dados da peça a ser cadastrada</param>
    /// <response code="200">Peça cadastrada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    public Task<OutputEstoque> Create([FromBody] PecaCreateDto dto);

    /// <summary>
    /// Atualiza uma peça existente no estoque
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <param name="dto">Dados atualizados da peça</param>
    /// <response code="200">Peça atualizada com sucesso</response>
    /// <response code="404">Peça não encontrada</response>
    public Task<OutputEstoque> Update(Guid id, [FromBody] PecaUpdateDto dto);

    /// <summary>
    /// Remove uma peça do estoque
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <response code="204">Peça removida com sucesso</response>
    /// <response code="404">Peça não encontrada</response>
    public Task<OutputEstoque> Delete(Guid id);
}
