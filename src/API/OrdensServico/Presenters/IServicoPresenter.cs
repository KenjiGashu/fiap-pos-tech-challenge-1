namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class OutputServicos
{
    public IEnumerable<ServicoResponseDto> Servicos;
    public string Message;
}

public class OutputServico
{
    public ServicoResponseDto Servico;
    public string Message;
}

public class OutputMessageServico
{
    public string Message;
}

public interface IServicoPresenter
{
    public OutputServicos Present(string message, IEnumerable<ServicoResponseDto> servicos);
    public OutputServico Present(string message, ServicoResponseDto servico);
    public OutputMessageServico Present(string message);
}
