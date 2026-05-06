namespace Gashu.SistemaMecanica.Domain.Metricas.Entities;

public enum StatusOrdemServico
{
    Recebida,
    OrcamentoAprovado,
    OrcamentoRejeitado,
    EmDiagnostico,
    AguardandoAprovacao,
    AguardandoAprovacaoRevisao,
    AguardandoMecanico,
    EmExecucao,
    Finalizada,
    Entregue
}

public class MetricaOrdemServico
{
    public Guid Id { get; set; }
    public StatusOrdemServico Status { get; set; }
    public DateTime DateTime { get; set; }

    public Guid OrdemServicoId { get; set; }

    public MetricaOrdemServico(Guid ordemServicoId, StatusOrdemServico status, DateTime dateTime)
    {
        this.OrdemServicoId = ordemServicoId;
        this.Status = status;
        this.DateTime = dateTime;
    }
}
