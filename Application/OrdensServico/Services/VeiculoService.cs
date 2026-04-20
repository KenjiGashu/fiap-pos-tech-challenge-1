using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;

namespace Application.OrdensServico.Services;

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
        if (v == null) return null;

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
