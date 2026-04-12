using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;
using Application.OrdensServico.DTOs;

namespace Application.OrdensServico.Services;

public class ClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

	public async Task<IEnumerable<ClienteResponseDto>> GetAll()
	{
    var clientes = await _repository.ObterTodos();

    return clientes.Select(c => new ClienteResponseDto
    {
        Id = c.Id,
        Nome = c.Nome,
        Email = c.Email,
				Cpf = c.Cpf,
				Cnpj = c.Cnpj,
				TipoPessoa = c.TipoPessoa
    });
	}
	
    // public async Task<IEnumerable<Cliente>> GetAll()
    //     => await _repository.ObterTodos();

    public async Task<Cliente?> GetById(Guid id)
        => await _repository.ObterPorId(id);

	public async Task Create(ClienteCreateDto dto)
	{
    var cliente = new Cliente(dto.Nome, dto.Email, dto.Cpf, dto.Cnpj, dto.TipoPessoa);
    await _repository.Adicionar(cliente);
	}

	public async Task Update(Guid id, ClienteUpdateDto dto)
	{
    var cliente = await _repository.ObterPorId(id);

    if (cliente == null)
        throw new Exception("Cliente não encontrado");

    cliente.Atualizar(dto.Nome, dto.Email, dto.Cpf, dto.Cnpj, dto.TipoPessoa);

    await _repository.Atualizar(cliente);
	}

    // public async Task Create(string nome, string email)
    // {
    //     var cliente = new Cliente(nome, email);
    //     await _repository.Adicionar(cliente);
    // }


    public async Task Delete(Guid id)
        => await _repository.Remover(id);
}
