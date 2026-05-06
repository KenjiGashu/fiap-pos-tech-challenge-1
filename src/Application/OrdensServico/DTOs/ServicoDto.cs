namespace Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class ServicoCreateDto
{
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
}

public class ServicoResponseDto
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
}

public class ServicoUpdateDto
{
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
}
