using Gashu.SistemaMecanica.API.OrdensServico.Presenters;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

public interface IOrdemServicoController
{
    public Task<OutputOrdemServicos> GetAll();
    public Task<OutputOrdemServicos> ListaOrdensServicos();
    public Task<OutputOrdemServico> GetById(Guid id);
    public Task<OutputOrdemServicos> GetByClientId(Guid id);
    public Task<OutputMessageOrdemServico> Create(OrdemServicoCreateDto dto);
    public Task<OutputMessageOrdemServico> CriarComTodosOsDados(OrdemServicoCreateDtoTodosDados dto);
    public Task<OutputMessageOrdemServico> AdicionaPecas(OrdemServicoAdicionaPecaDto dto);
    public Task<OutputMessageOrdemServico> AdicionaServicos(OrdemServicoAdicionaServicoDto dto);
    public Task<OutputMessageOrdemServico> EnviarOrcamento(OrdemServicoEnviarOrcamentoDto dto);
    public Task<OutputMessageOrdemServico> AprovarOrcamento(Guid id);
    public Task<OutputMessageOrdemServico> RejeitarOrcamento(Guid id);
    public Task<OutputMessageOrdemServico> IniciarDiagnostico(OrdemServicoIniciarDiagnosticoOrcamentoDto dto);
    public Task<OutputMessageOrdemServico> FinalizarDiagnostico(OrdemServicoFinalizarDiagnosticoOrcamentoDto dto);
    public Task<OutputMessageOrdemServico> IniciarExecucao(OrdemServicoIniciarExecucaoOrcamentoDto dto);
    public Task<OutputMessageOrdemServico> FinalizarExecucao(OrdemServicoFinalizarExecucaoOrcamentoDto dto);
    public Task<OutputMessageOrdemServico> EntregarVeiculo(OrdemServicoEntregarVeiculoDto dto);
    public Task<OutputMessageOrdemServico> Delete(OrdemServicoDeleteDto dto);
}
