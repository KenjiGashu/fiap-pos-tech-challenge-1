using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class ServicoPresenter : IServicoPresenter
{
    public OutputServicos Present(string message, IEnumerable<ServicoResponseDto> servicos)
    {
        return new OutputServicos
        {
            Message = message,
            Servicos = servicos
        };
    }

    public OutputServico Present(string message, ServicoResponseDto servico)
    {
        return new OutputServico
        {
            Message = message,
            Servico = servico
        };
    }

    public OutputMessageServico Present(string message)
    {
        return new OutputMessageServico
        {
            Message = message,
        };
    }
}
