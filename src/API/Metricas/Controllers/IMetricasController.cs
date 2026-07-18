namespace Gashu.SistemaMecanica.API.Metricas.Controllers;

using Gashu.SistemaMecanica.API.Metricas.Presenters;

/// <summary>
/// Controller responsável pelo calculo de metricas
/// </summary>
public interface IMetricasController
{
    /// <summary>
    /// Lista todas as métricas registradas
    /// </summary>
    public Task<OutputMetricas> GetAll();
    
    /// <summary>
    /// Calcula o tempo médio entre mudanças de status de uma ordem de serviço
    /// </summary>
    public Task<OutputMetricasSegundos> TempoMedio(Guid id);

    /// <summary>
    /// Calcula o tempo total de execução de uma ordem de serviço
    /// </summary>
    public Task<OutputMetricasSegundos> TempoTotal(Guid id);

    /// <summary>
    /// Calcula o tempo médio total de execução de todas as ordens de serviço
    /// </summary>
    public Task<OutputMetricasSegundos> TempoMedioAtendimentos();
}
