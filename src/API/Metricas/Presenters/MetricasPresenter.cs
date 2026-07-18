using Gashu.SistemaMecanica.Application.Metricas.Services;

namespace Gashu.SistemaMecanica.API.Metricas.Presenters;

public class MetricasPresenter : IMetricasPresenter
{
    public OutputMetricas Present(string message, IEnumerable<MetricaOrdemServicoResponseDto> metricas)
    {
        return new OutputMetricas
        {
            Metricas = metricas,
            Message = message
        };
    }

    public OutputMetricasSegundos Present(string message, int tempo)
    {
        return new OutputMetricasSegundos
        {
            Message = message,
            Tempo = tempo
        };
    }
}
