namespace Application.Metricas.DTOs;

using System.Text.Json.Serialization;
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusOrdemServico Status { get; set; }
    public DateTime DateTime { get; set; }
}
