namespace Gashu.SistemaMecanica.Application.Metricas.Services;

using Gashu.SistemaMecanica.Application.Metricas.Services;
using Gashu.SistemaMecanica.Application.Metricas.Interfaces;
using Gashu.SistemaMecanica.Domain.Metricas.Entities;
using Gashu.SistemaMecanica.Domain.Metricas.Interfaces;
using System.Linq;

public class MetricaOrdemServicoService : IMetricaOrdemServicoService
{
    readonly IMetricaOrdemServicoRepository _repo;

    public MetricaOrdemServicoService(IMetricaOrdemServicoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<MetricaOrdemServicoResponseDto>> GetAll()
    {
        var metrics = await _repo.ObterTodos();
        return metrics.Select(m => new MetricaOrdemServicoResponseDto
        {
            OrdemServicoId = m.OrdemServicoId,
            DateTime = m.DateTime,
            Status = m.Status
        });
    }

    public async Task SalvaMetricaOrdemServico(SalvarMetricaOrdemServicoDto dto)
    {
        var result = Enum.TryParse<StatusOrdemServico>(dto.Status, out var status);
        if (!result)
            throw new FormatException("Invalid Status");
        var metrica = new MetricaOrdemServico(dto.OrdemServicoId, status, DateTime.UtcNow);
        await _repo.Adicionar(metrica);
    }

    public async Task<int> TempoMedioOrdemServico(TempoMedioOrdemServicoDto dto)
    {
        var atualizacoesOrdemServico = await _repo.ObterPorOrdemServicoId(dto.OrdemServicoId);
        var eventosOrdenados = atualizacoesOrdemServico.OrderBy(m => m.DateTime).ToList();

        var diferencas = new List<TimeSpan>();

        for (int i = 1; i < eventosOrdenados.Count; i++)
        {
            var atual = eventosOrdenados[i].DateTime;
            var anterior = eventosOrdenados[i - 1].DateTime;

            Console.WriteLine($"[tempo medio...] diff[{(atual-anterior).Seconds}] ");

            diferencas.Add(atual - anterior);
        }

        return (int)diferencas.Average(d => d.Seconds);
    }

    public async Task<int> TempoTotalOrdemServico(TempoTotalOrdemServicoDto dto)
    {
        var atualizacoesOrdemServico = await _repo.ObterPorOrdemServicoId(dto.OrdemServicoId);
        var timeSpanStatus = atualizacoesOrdemServico.OrderBy(m => m.DateTime)
            .Zip(atualizacoesOrdemServico.OrderBy(x => x.DateTime).Skip(1),
                 (anterior, atual) => new { TimeSpan =  atual.DateTime - anterior.DateTime });

        return timeSpanStatus.Sum(d => d.TimeSpan.Seconds);
    }

    public async Task<int> TempoMedioAtendimentos()
    {
        var atualizacoesOrdemServico = await _repo.ObterTodos();
        var groupedMetricas = atualizacoesOrdemServico.GroupBy(mos => mos.OrdemServicoId);
        var media = 0;
        var numGrupos = groupedMetricas.Count();

        Console.WriteLine($"Tempo medio atendimento:\n numGrupos {numGrupos}");

        foreach(var grupoMetricas in groupedMetricas)
        {
						var timeSpanStatus = grupoMetricas.OrderBy(m => m.DateTime)
            .Zip(grupoMetricas.OrderBy(x => x.DateTime).Skip(1),
                 (anterior, atual) => new { TimeSpan =  atual.DateTime - anterior.DateTime });

            var soma = timeSpanStatus.Sum(d => d.TimeSpan.Seconds);

            Console.WriteLine($"OrdemServicoID: {grupoMetricas.Key}  Tempo: {soma}");
            media += soma;
        }

        return media / numGrupos;
    }
}
