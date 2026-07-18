namespace Gashu.SistemaMecanica.Application.Metricas.Services;

using Gashu.SistemaMecanica.Application.Metricas.Services;

public interface IMetricaOrdemServicoService
{
    public Task<IEnumerable<MetricaOrdemServicoResponseDto>> GetAll();
    public Task SalvaMetricaOrdemServico(SalvarMetricaOrdemServicoDto dto);
    public Task<int> TempoMedioOrdemServico(TempoMedioOrdemServicoDto dto);
    public Task<int> TempoTotalOrdemServico(TempoTotalOrdemServicoDto dto);
    public Task<int> TempoMedioAtendimentos();
}
