using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OutputOrdemServicos
{
    public string Message;
    public IEnumerable<OrdemServicoResponseDto> OrdemServicos;
}

public class OutputOrdemServico
{
    public string Message;
    public OrdemServicoResponseDto OrdemServico;
}

public class OutputMessageOrdemServico
{
    public string Message;
}

public interface IOrdemServicoPresenter
{
    public OutputOrdemServicos Present(string message, IEnumerable<OrdemServicoResponseDto> ordemServicos);
    public OutputOrdemServico Present(string message, OrdemServicoResponseDto ordemServico);
    public OutputMessageOrdemServico Present(string message);
}
