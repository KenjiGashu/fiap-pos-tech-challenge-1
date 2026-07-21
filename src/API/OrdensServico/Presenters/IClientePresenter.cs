using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OutputClientes
{
    public IEnumerable<ClienteResponseDto> Clientes { get; set; }
    public string Message { get; set; }
}

public class OutputCliente
{
    public ClienteResponseDto? Cliente { get; set; }
    public string Message { get; set; }
}

public class OutputMessage
{
    public string Message { get; set; }
}

public interface IClientePresenter
{
    public OutputClientes Present(string message, IEnumerable<ClienteResponseDto> clientes);
    public OutputCliente Present(string message, ClienteResponseDto cliente);
    public OutputMessage Present(string message);
}
