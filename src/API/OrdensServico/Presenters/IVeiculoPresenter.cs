namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class OutputVeiculos
{
    public IEnumerable<VeiculoResponseDto> Veiculos;
    public string Message;
}

public class OutputVeiculo
{
    public VeiculoResponseDto Veiculo;
    public string Message;
}

public class OutputMessageVeiculo
{
    public string Message;
}

public interface IVeiculoPresenter
{
    public OutputVeiculos Present(string message, IEnumerable<VeiculoResponseDto> veiculos);
    public OutputVeiculo Present(string message, VeiculoResponseDto veiculo);
    public OutputMessageVeiculo Present(string message);
}
