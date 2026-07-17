using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OrdemServicoPresenter : IOrdemServicoPresenter
{
    public OutputOrdemServicos Present(string message, IEnumerable<OrdemServicoResponseDto> ordemServicos)
    {
        return new OutputOrdemServicos
        {
            Message = message,
            OrdemServicos = ordemServicos
        };
    }

    public OutputOrdemServico Present(string message, OrdemServicoResponseDto ordemServico)
    {
        return new OutputOrdemServico
        {
            Message = message,
            OrdemServico = ordemServico
        };
    }

    public OutputMessageOrdemServico Present(string message)
    {
        return new OutputMessageOrdemServico
        {
            Message = message,
        };
    }
}
