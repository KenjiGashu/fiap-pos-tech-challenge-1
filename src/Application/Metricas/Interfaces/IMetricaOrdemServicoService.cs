namespace Application.Metricas.Interfaces;

using Application.Metricas.DTOs;

public interface IMetricaOrdemServicoService
{
    public Task<IEnumerable<MetricaOrdemServicoResponseDto>> GetAll();
    public Task SalvaMetricaOrdemServico(SalvarMetricaOrdemServicoDto dto);
    public Task<int> TempoMedioOrdemServico(TempoMedioOrdemServicoDto dto);
    public Task<int> TempoTotalOrdemServico(TempoTotalOrdemServicoDto dto);
}
