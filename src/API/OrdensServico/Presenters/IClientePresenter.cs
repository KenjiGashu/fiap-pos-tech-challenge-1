using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OutputClientes
{
    public IEnumerable<ClienteResponseDto> Clientes;
    public string Message;
}

public class OutputCliente
{
    public ClienteResponseDto? Cliente;
    public string Message;
}

public class OutputMessage
{
    public string Message;
}

public interface IClientePresenter
{
    public OutputClientes Present(string message, IEnumerable<ClienteResponseDto> clientes);
    public OutputCliente Present(string message, ClienteResponseDto cliente);
    public OutputMessage Present(string message);
}
