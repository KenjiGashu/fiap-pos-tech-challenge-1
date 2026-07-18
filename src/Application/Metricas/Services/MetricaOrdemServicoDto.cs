namespace Gashu.SistemaMecanica.Application.Metricas.Services;

using System.Text.Json.Serialization;
using Gashu.SistemaMecanica.Domain.Metricas.Entities;

public class SalvarMetricaOrdemServicoDto
{
    public required Guid OrdemServicoId { get; set; }
    public required string Status { get; set; } = "";
}

public class TempoMedioOrdemServicoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class TempoTotalOrdemServicoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class MetricaOrdemServicoResponseDto
{
    public required Guid OrdemServicoId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required StatusOrdemServico Status { get; set; }
    public required DateTime DateTime { get; set; }
}
