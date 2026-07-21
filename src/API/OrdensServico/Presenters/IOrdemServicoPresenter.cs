using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OutputOrdemServicos
{
    public string Message { get; set; }
    public IEnumerable<OrdemServicoResponseDto> OrdemServicos { get; set; }
}

public class OutputOrdemServico
{
    public string Message { get; set; }
    public OrdemServicoResponseDto OrdemServico { get; set; }
}

public class OutputMessageOrdemServico
{
    public string Message { get; set; }
}

public interface IOrdemServicoPresenter
{
    public OutputOrdemServicos Present(string message, IEnumerable<OrdemServicoResponseDto> ordemServicos);
    public OutputOrdemServico Present(string message, OrdemServicoResponseDto ordemServico);
    public OutputMessageOrdemServico Present(string message);
}
