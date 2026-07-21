namespace Gashu.SistemaMecanica.API.OrdensServico.API;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de Ordens de Serviço
/// </summary>
[ApiController]
[Route("api/OrdemServico")]
public class OrdemServicoAPI : ControllerBase
{
    private readonly IOrdemServicoController _controller;
    private readonly ILogger<OrdemServicoAPI> _logger;
    private readonly string Domain = "OrdemServico";

    public OrdemServicoAPI(IOrdemServicoController controller, ILogger<OrdemServicoAPI> logger)
    {
        _controller = controller;
        _logger = logger;
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
        try
        {
            var output = await _controller.GetAll();
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "Get", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.ListaOrdensServicos();
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "ListaOrdensServicos", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.GetById(id);
            return Ok(output.OrdemServico);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "ObterPorId", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Busca ordens de serviço por cliente
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <response code="200">Ordens encontradas</response>
    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> ObterPorIdCliente(Guid id)
    {
        try
        {
            var output = await _controller.GetByClientId(id);
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "ObterPorIdCliente", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Create(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "Post", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Cria uma nova ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem (cliente e veículo)</param>
    /// <response code="200">Ordem criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("criarOrdemServico")]
    public async Task<IActionResult> CriarComTodosOsDados([FromBody] OrdemServicoCreateDtoTodosDados dto)
    {
        try
        {
            var output = await _controller.CriarComTodosOsDados(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "CriarComTodosOsDados", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.AdicionaPecas(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "AdicionaPecas", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.AdicionaServicos(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "AdicionaServicos", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.EnviarOrcamento(dto);
            _logger.LogDebug("[{Domain}][{Endpoint}] orcamento da OS {OrdemServicoId} enviado com sucesso!", Domain, "AprovarOrcamento", dto.OrdemServicoId);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "EnviarOrcamento", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Aprova um orçamento
    /// </summary>
    /// <param name="id">Token de aprovação</param>
    /// <response code="200">Orçamento aprovado</response>
    [HttpGet("AprovarOrcamento/{id}")]
    public async Task<IActionResult> AprovarOrcamento(Guid id)
    {
        try
        {
            var output = await _controller.AprovarOrcamento(id);
            _logger.LogDebug("[{Domain}][{Endpoint}] orcamento da OS {OrdemServicoId} aprovado com sucesso!", Domain, "AprovarOrcamento", id);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "AprovarOrcamento", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Rejeita um orçamento
    /// </summary>
    /// <param name="id">Token de rejeição</param>
    /// <response code="200">Orçamento rejeitado</response>
    [HttpGet("RejeitarOrcamento/{id}")]
    public async Task<IActionResult> RejeitarOrcamento(Guid id)
    {
        try
        {
            var output = await _controller.RejeitarOrcamento(id);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "RejeitarOrcamento", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.IniciarDiagnostico(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "IniciarDiagnostico", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.FinalizarDiagnostico(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "FinalizarDiagnostico", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Inicia execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarExecucao")]
    public async Task<IActionResult> IniciarExecucao([FromBody] OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.IniciarExecucao(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "IniciarExecucao", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Finaliza execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarExecucao")]
    public async Task<IActionResult> FinalizarExecucao([FromBody] OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.FinalizarExecucao(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "FinalizarExecucao", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Realiza entrega do veículo ao cliente
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("EntregarVeiculo")]
    public async Task<IActionResult> EntregarVeiculo([FromBody] OrdemServicoEntregarVeiculoDto dto)
    {
        try
        {
            var output = await _controller.EntregarVeiculo(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "EntregarVeiculo", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Delete(dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{Domain}][{Endpoint}] ex {ex.Message}. {ex.StackTrace}", Domain, "Delete", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }
}
