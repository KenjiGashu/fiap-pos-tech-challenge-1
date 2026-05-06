namespace Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

public class VeiculoCreateDto
{
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Ano { get; set; }
}

public class VeiculoUpdateDto
{
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Ano { get; set; }
}

public class VeiculoResponseDto
{
    public required Guid Id { get; set; }
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Ano { get; set; }
}
