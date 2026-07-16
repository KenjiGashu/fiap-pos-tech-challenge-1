using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;


public class ClientePresenter : IClientePresenter
{
    public OutputClientes Present(string message, IEnumerable<ClienteResponseDto> clientes)
    {
        return new OutputClientes
        {
            Clientes = clientes,
            Message = message
        };
    }

    public OutputCliente Present(string message, ClienteResponseDto cliente)
    {
        return new OutputCliente
        {
            Message = message,
            Cliente = cliente
        };
    }

    public OutputMessage Present(string message)
    {
        return new OutputMessage
        {
           Message = message
        };
    }
}
