namespace API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;

[ApiController]
[Route("api/[controller]")]
public class OrdemServicoController : ControllerBase
{
    private readonly OrdemServicoService _service;

    public OrdemServicoController(OrdemServicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDto dto)
    {
        await _service.Criar(dto);
        return Ok();
    }

    [HttpPost("AdicionaPeca")]
    public async Task<IActionResult> AdicionaPecas([FromBody] OrdemServicoAdicionaPecaDto dto)
    {
        await _service.AdicionarPecas(dto);
        return Ok();
    }

    [HttpPost("AdicionaServico")]
    public async Task<IActionResult> AdicionaServicos([FromBody] OrdemServicoAdicionaServicoDto dto)
    {
        await _service.AdicionarServicos(dto);
        return Ok();
    }

    [HttpPost("EnviarOrcamento")]
    public async Task<IActionResult> EnviarOrcamento([FromBody] OrdemServicoEnviarOrcamentoDto dto)
    {
        await _service.EnviarOrcamento(dto);
        return Ok(new {Message = $"orcamento enviado!"});
    }

    [HttpPost("IniciarDiagnostico")]
    public async Task<IActionResult> IniciarDiagnostico([FromBody] OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        await _service.IniciarDiagnostico(dto);
        return Ok(new {Message = $"iniciado diagnostico!"});
    }

    [HttpPost("FinalizarDiagnostico")]
    public async Task<IActionResult> FinalizarDiagnostico([FromBody] OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        await _service.FinalizarDiagnostico(dto);
        return Ok(new {Message = $"finalizado diagnostico! Enviado orçamento para Aprovação da Revisao"});
    }

    [HttpPost("IniciarExecucao")]
    public async Task<IActionResult> IniciarExecucao([FromBody] OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        await _service.IniciarExecucao(dto);
        return Ok(new {Message = $"iniciado execucao!"});
    }

    [HttpPost("FinalizarExecucao")]
    public async Task<IActionResult> FinalizarExecucao([FromBody] OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        await _service.FinalizarExecucao(dto);
        return Ok(new {Message = $"Finalizado execucao!"});
    }

    [HttpPost("EntregarVeiculo")]
    public async Task<IActionResult> EntregarVeiculo([FromBody] OrdemServicoEntregarVeiculoDto dto)
    {
        await _service.EntregarVeiculo(dto);
        return Ok(new {Message = $"Entrega de Veiculo Feita!"});
    }

    [HttpGet("AprovarOrcamento/{id}")]
    public async Task<IActionResult> AprovarOrcamento(Guid id)
    {
			  await _service.AprovarOrcamento(new OrdemServicoAprovarOrcamentoDto{TokenGuid = id});
        return Ok(new {Message = $"Orcamento {id} aprovado com sucesso"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(OrdemServicoDeleteDto dto)
    {
        await _service.Deletar(dto);
        return NoContent();
    }
}
