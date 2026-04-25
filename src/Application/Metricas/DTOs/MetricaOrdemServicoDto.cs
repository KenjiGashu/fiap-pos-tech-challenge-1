namespace Application.Metricas.DTOs;

using Domain.Metricas.Entities;

public class SalvarMetricaOrdemServicoDto
{
    public Guid OrdemServicoId { get; set; }
    public string Status { get; set; } = "";
}

public class TempoMedioOrdemServicoDto
{
    public Guid OrdemServicoId { get; set; }
}

public class TempoTotalOrdemServicoDto
{
    public Guid OrdemServicoId { get; set; }
}

public class MetricaOrdemServicoResponseDto
{
    public Guid OrdemServicoId { get; set; }
    public StatusOrdemServico Status { get; set; }
    public DateTime DateTime { get; set; }
}
