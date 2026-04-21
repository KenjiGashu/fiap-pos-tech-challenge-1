namespace Application.OrdensServico.DTOs;
using Domain.OrdensServico.Entities;

public class ClienteCreateDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
}


public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
}

public class ClienteUpdateDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
}
