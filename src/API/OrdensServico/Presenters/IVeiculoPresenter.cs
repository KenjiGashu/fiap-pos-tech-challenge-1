namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class OutputVeiculos
{
    public IEnumerable<VeiculoResponseDto> Veiculos { get; set; }
    public string Message { get; set; }
}

public class OutputVeiculo
{
    public VeiculoResponseDto Veiculo { get; set; }
    public string Message { get; set; }
}

public class OutputMessageVeiculo
{
    public string Message { get; set; }
}

public interface IVeiculoPresenter
{
    public OutputVeiculos Present(string message, IEnumerable<VeiculoResponseDto> veiculos);
    public OutputVeiculo Present(string message, VeiculoResponseDto veiculo);
    public OutputMessageVeiculo Present(string message);
}
