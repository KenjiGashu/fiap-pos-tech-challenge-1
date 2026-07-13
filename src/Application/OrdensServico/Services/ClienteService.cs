using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

namespace Gashu.SistemaMecanica.Application.OrdensServico.Services;

public class ClienteService : IClienteService
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
            Email = c.Usuario.Email,
            Cpf = c.Cpf,
            Cnpj = c.Cnpj,
            TipoPessoa = c.TipoPessoa
        });
    }

    public async Task<ClienteResponseDto> GetById(Guid id)
    {
        var cliente = await _repository.ObterPorId(id);
        if(cliente == null)
            throw new Exception("Cliente nao encontrado");

        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Usuario.Email,
            Cpf = cliente.Cpf,
            Cnpj = cliente.Cnpj,
            TipoPessoa = cliente.TipoPessoa

        };
    }

    public async Task<ClienteResponseDto> GetByNome(string nome)
    {
        var cliente = await _repository.ObterPorNome(nome);
        if(cliente == null)
            throw new Exception("Cliente nao encontrado");

        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Usuario.Email,
            Cpf = cliente.Cpf,
            Cnpj = cliente.Cnpj,
            TipoPessoa = cliente.TipoPessoa
        };
    }

    public async Task<ClienteResponseDto> GetByCpf(string cpf)
    {
        var cliente = await _repository.ObterPorCpf(cpf);
        if(cliente == null)
            throw new Exception("Cliente nao encontrado");

        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Usuario.Email,
            Cpf = cliente.Cpf,
            Cnpj = cliente.Cnpj,
            TipoPessoa = cliente.TipoPessoa
        };
    }

    public async Task<ClienteResponseDto> GetByCnpj(string cnpj)
    {
        var cliente = await _repository.ObterPorCnpj(cnpj);
        if(cliente == null)
            throw new Exception("Cliente nao encontrado");

        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Usuario.Email,
            Cpf = cliente.Cpf,
            Cnpj = cliente.Cnpj,
            TipoPessoa = cliente.TipoPessoa
        };
    }

    public async Task Create(ClienteCreateDto dto)
    {
        var cliente = new Cliente(dto.Nome, dto.Cpf, dto.Cnpj, dto.TipoPessoa);
        cliente.UsuarioId = dto.UsuarioId;
        Console.WriteLine($"adicionando cliente nome: {dto.Nome} usuario {dto.UsuarioId}");
        await _repository.Adicionar(cliente);
    }

    public async Task Update(Guid id, ClienteUpdateDto dto)
    {
        var cliente = await _repository.ObterPorId(id);

        if (cliente == null)
            throw new Exception("Cliente não encontrado");

        cliente.Atualizar(dto.Nome,  dto.Cpf, dto.Cnpj, dto.TipoPessoa);

        await _repository.Atualizar(cliente);
    }

    public async Task Delete(Guid id)
    => await _repository.Remover(id);
}
