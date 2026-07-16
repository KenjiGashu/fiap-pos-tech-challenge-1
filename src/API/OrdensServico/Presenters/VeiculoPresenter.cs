using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class VeiculoPresenter : IVeiculoPresenter
{
    public OutputVeiculos Present(string message, IEnumerable<VeiculoResponseDto> veiculos)
    {
        return new OutputVeiculos
        {
            Message = message,
            Veiculos = veiculos
        };
    }

    public OutputVeiculo Present(string message, VeiculoResponseDto veiculo)
    {
        return new OutputVeiculo
        {
            Message = message,
            Veiculo = veiculo
        };
    }

    public OutputMessageVeiculo Present(string message)
    {
        return new OutputMessageVeiculo
        {
            Message = message,
        };
    }
}
