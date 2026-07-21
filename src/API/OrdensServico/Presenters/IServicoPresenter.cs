namespace Gashu.SistemaMecanica.API.OrdensServico.Presenters;

using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class OutputServicos
{
    public IEnumerable<ServicoResponseDto> Servicos { get; set; }
    public string Message { get; set; }
}

public class OutputServico
{
    public ServicoResponseDto Servico { get; set; }
    public string Message { get; set; }
}

public class OutputMessageServico
{
    public string Message { get; set; }
}

public interface IServicoPresenter
{
    public OutputServicos Present(string message, IEnumerable<ServicoResponseDto> servicos);
    public OutputServico Present(string message, ServicoResponseDto servico);
    public OutputMessageServico Present(string message);
}
