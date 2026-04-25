namespace Application.OrdensServico.Interfaces;

using Application.OrdensServico.DTOs;

public interface IOrdemServicoService
{
    public Task<IEnumerable<OrdemServicoResponseDto>> GetAll();
    public Task<OrdemServicoResponseDto> ObterPorId(Guid id);
    public Task<ListaOrdemServicoResponseDto> ObterPorIdCliente(Guid clienteId);
    public Task Criar(OrdemServicoCreateDto dto);
    public Task Deletar(OrdemServicoDeleteDto dto);
    public Task AdicionarPecas(OrdemServicoAdicionaPecaDto dto);
    public Task AdicionarServicos(OrdemServicoAdicionaServicoDto dto);
    public Task EnviarOrcamento(OrdemServicoEnviarOrcamentoDto dto);
    public Task AprovarOrcamento(OrdemServicoAprovarOrcamentoDto dto);
    public Task RejeitarOrcamento(OrdemServicoRejeitarOrcamentoDto dto);
    public Task IniciarDiagnostico(OrdemServicoIniciarDiagnosticoOrcamentoDto dto);
    public Task FinalizarDiagnostico(OrdemServicoFinalizarDiagnosticoOrcamentoDto dto);
    public Task IniciarExecucao(OrdemServicoIniciarExecucaoOrcamentoDto dto);
    public Task FinalizarExecucao(OrdemServicoFinalizarExecucaoOrcamentoDto dto);
    public Task EntregarVeiculo(OrdemServicoEntregarVeiculoDto dto);
}
