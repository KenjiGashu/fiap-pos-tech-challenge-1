namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pelo gerenciamento de Ordens de Serviço
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdemServicoController : ControllerBase
{
    private readonly IOrdemServicoService _service;

    public OrdemServicoController(IOrdemServicoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as ordens de serviço
    /// </summary>
    /// <remarks>
    /// Retorna todas as ordens cadastradas no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de ordens retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll());
    }

    /// <summary>
    /// Mostra lista ordens de serviço filtradas e ordenadas
    /// </summary>
    /// <remarks>
    /// Retorna todas as ordens cadastradas no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de ordens retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("ListaOrdensServicos")]
    public async Task<IActionResult> ListaOrdensServicos()
    {
        return Ok(await _service.ListaOrdensServicos());
    }


    /// <summary>
    /// Obtém uma ordem de serviço por ID
    /// </summary>
    /// <param name="id">Identificador da ordem de serviço</param>
    /// <response code="200">Ordem encontrada</response>
    /// <response code="404">Ordem não encontrada</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        return Ok(await _service.ObterPorId(id));
    }

    /// <summary>
    /// Busca ordens de serviço por cliente
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <response code="200">Ordens encontradas</response>
    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> ObterPorIdCliente(Guid id)
    {
        return Ok(await _service.ObterPorIdCliente(id));
    }

    /// <summary>
    /// Cria uma nova ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem (cliente e veículo)</param>
    /// <response code="200">Ordem criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDto dto)
    {
        await _service.Criar(dto);
        return Ok(new { Message = "Serviço criado com sucesso!" });
    }

    /// <summary>
    /// Cria uma nova ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem (cliente e veículo)</param>
    /// <response code="200">Ordem criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("criarOrdemServico")]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDtoTodosDados dto)
    {
        await _service.CriarComTodosOsDados(dto);
        return Ok(new { Message = "Serviço criado com sucesso!" });
    }

    /// <summary>
    /// Adiciona peças a uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados das peças a serem adicionadas</param>
    /// <response code="200">Peças adicionadas com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaPeca")]
    public async Task<IActionResult> AdicionaPecas([FromBody] OrdemServicoAdicionaPecaDto dto)
    {
        await _service.AdicionarPecas(dto);
        return Ok(new { Message = "Peça adicionada!" });
    }

    /// <summary>
    /// Adiciona serviços a uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados dos serviços</param>
    /// <response code="200">Serviço adicionado com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaServico")]
    public async Task<IActionResult> AdicionaServicos([FromBody] OrdemServicoAdicionaServicoDto dto)
    {
        await _service.AdicionarServicos(dto);
        return Ok(new { Message = "Serviço adicionado!" });
    }

    /// <summary>
    /// Envia orçamento para aprovação do cliente.
    /// No momento, implementado apenas com email
    /// </summary>
    /// <param name="dto">Dados do envio do orçamento</param>
    /// <response code="200">Orçamento enviado com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("EnviarOrcamento")]
    public async Task<IActionResult> EnviarOrcamento([FromBody] OrdemServicoEnviarOrcamentoDto dto)
    {
        await _service.EnviarOrcamento(dto);
        return Ok(new { Message = "Orçamento enviado!" });
    }

    /// <summary>
    /// Aprova um orçamento
    /// </summary>
    /// <param name="id">Token de aprovação</param>
    /// <response code="200">Orçamento aprovado</response>
    [HttpGet("AprovarOrcamento/{id}")]
    public async Task<IActionResult> AprovarOrcamento(Guid id)
    {
        await _service.AprovarOrcamento(new OrdemServicoAprovarOrcamentoDto { TokenGuid = id });
        return Ok(new { Message = $"Orçamento {id} aprovado com sucesso" });
    }

    /// <summary>
    /// Rejeita um orçamento
    /// </summary>
    /// <param name="id">Token de rejeição</param>
    /// <response code="200">Orçamento rejeitado</response>
    [HttpGet("RejeitarOrcamento/{id}")]
    public async Task<IActionResult> RejeitarOrcamento(Guid id)
    {
        await _service.RejeitarOrcamento(new OrdemServicoRejeitarOrcamentoDto { TokenGuid = id });
        return Ok(new { Message = $"Orçamento {id} rejeitado" });
    }

    /// <summary>
    /// Inicia o diagnóstico da ordem
    /// </summary>
    /// <param name="dto">Dados do diagnóstico</param>
    /// <response code="200">Diagnóstico iniciado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarDiagnostico")]
    public async Task<IActionResult> IniciarDiagnostico([FromBody] OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        await _service.IniciarDiagnostico(dto);
        return Ok(new { Message = "Diagnóstico iniciado!" });
    }

    /// <summary>
    /// Finaliza o diagnóstico e envia para aprovação
    /// </summary>
    /// <param name="dto">Dados do diagnóstico</param>
    /// <response code="200">Diagnóstico finalizado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarDiagnostico")]
    public async Task<IActionResult> FinalizarDiagnostico([FromBody] OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        await _service.FinalizarDiagnostico(dto);
        return Ok(new { Message = "Diagnóstico finalizado e orçamento enviado!" });
    }

    /// <summary>
    /// Inicia execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarExecucao")]
    public async Task<IActionResult> IniciarExecucao([FromBody] OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        await _service.IniciarExecucao(dto);
        return Ok(new { Message = "Execução iniciada!" });
    }

    /// <summary>
    /// Finaliza execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarExecucao")]
    public async Task<IActionResult> FinalizarExecucao([FromBody] OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        await _service.FinalizarExecucao(dto);
        return Ok(new { Message = "Execução finalizada!" });
    }

    /// <summary>
    /// Realiza entrega do veículo ao cliente
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("EntregarVeiculo")]
    public async Task<IActionResult> EntregarVeiculo([FromBody] OrdemServicoEntregarVeiculoDto dto)
    {
        await _service.EntregarVeiculo(dto);
        return Ok(new { Message = "Veículo entregue!" });
    }

    /// <summary>
    /// Remove uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem a ser removida</param>
    /// <response code="204">Ordem removida com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] OrdemServicoDeleteDto dto)
    {
        await _service.Deletar(dto);
        return NoContent();
    }
}
