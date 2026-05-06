namespace Application.OrdensServico.DTOs;

using System.Text.Json.Serialization;
using Domain.OrdensServico.Entities;

public class ClienteCreateDto
{
    public required string Nome { get; set; }
    public required Guid UsuarioId { get; set; }
    public required string Cpf { get; set; }
    public required string Cnpj { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required TipoPessoa TipoPessoa { get; set; }
}


public class ClienteResponseDto
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Cpf { get; set; }
    public required string Cnpj { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required TipoPessoa TipoPessoa { get; set; }
}

public class ClienteUpdateDto
{
    public required string Nome { get; set; }
    public required string Cpf { get; set; }
    public required string Cnpj { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required TipoPessoa TipoPessoa { get; set; }
}
