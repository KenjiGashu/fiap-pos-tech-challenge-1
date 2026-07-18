using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;

namespace Gashu.SistemaMecanica.Application.OrdensServico.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _repo;

    public VeiculoService(IVeiculoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<VeiculoResponseDto>> GetAll()
    {
        var veiculos = await _repo.ObterTodos();

        return veiculos.Select(v => new VeiculoResponseDto
        {
            Id = v.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Ano = v.Ano
        });
    }

    public async Task<VeiculoResponseDto> GetById(Guid id)
    {
        var v = await _repo.ObterPorId(id);
        if (v == null)
            throw new Exception("Veiculo nao encontrado");

        return new VeiculoResponseDto
        {
            Id = v.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Ano = v.Ano
        };
    }

    public async Task<VeiculoResponseDto> GetByPlaca(string placa)
    {
        var v = await _repo.ObterPorPlaca(placa);
        if (v == null)
            throw new Exception("Placa de veiculo nao encontrada");

        return new VeiculoResponseDto
        {
            Id = v.Id,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            Ano = v.Ano
        };
    }

    public async Task Create(VeiculoCreateDto dto)
    {
        var veiculo = new Veiculo(dto.Placa, dto.Marca, dto.Modelo, dto.Ano);
        await _repo.Adicionar(veiculo);
    }

    public async Task Update(Guid id, VeiculoUpdateDto dto)
    {
        var veiculo = await _repo.ObterPorId(id);
        if (veiculo == null)
            throw new Exception("Veículo não encontrado");

        veiculo.Atualizar(dto.Placa, dto.Marca, dto.Modelo, dto.Ano);
        await _repo.Atualizar(veiculo);
    }

    public async Task Delete(Guid id)
    {
        await _repo.Remover(id);
    }
}
