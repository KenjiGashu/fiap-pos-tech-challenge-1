namespace Gashu.SistemaMecanica.API.Metricas.Presenters;

using Application.Metricas.Services;

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputMetricas
{
    /// <summary>
    /// lista de metricas
    /// </summary>
    public IEnumerable<MetricaOrdemServicoResponseDto> Metricas;

    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;
}

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputMetricasSegundos
{
    /// <summary>
    /// tempo
    /// </summary>
    public int Tempo;

    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;
}

/// <summary>
/// Adaptor que tem a função de Presenter do clean arch
/// </summary>
public interface IMetricasPresenter
{
    /// <summary>
    /// Converter retorno do controller para outputMetricas
    /// </summary>
    public OutputMetricas Present(string message, IEnumerable<MetricaOrdemServicoResponseDto> metricas);
    
    /// <summary>
    /// Converter retorno do controller para OutputMetricasSegundos
    /// </summary>
    public OutputMetricasSegundos Present(string message, int tempo);
}
