namespace Application.OrdensServico.Services;

using Application.OrdensServico.DTOs;
using Domain.OrdensServico.Entities;
using Domain.OrdensServico.Interfaces;

public class ServicoService
{
    private readonly IServicoRepository _repo;

    public ServicoService(IServicoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ServicoResponseDto>> GetAll()
    {
        var lista = await _repo.ObterTodos();

        return lista.Select(s => new ServicoResponseDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco
        });
    }

	public async Task<ServicoResponseDto?> GetById(Guid id){
		var servico = await _repo.ObterPorId(id);

		return new ServicoResponseDto
		{
			Id = servico.Id,
			Nome = servico.Nome,
			Preco = servico.Preco
		};
	}

    public async Task Create(ServicoCreateDto dto)
    {
        var servico = new Servico(dto.Nome, dto.Preco);
        await _repo.Adicionar(servico);
    }

	public async Task Update(Guid id, ServicoUpdateDto dto)
	{
    var servico = await _repo.ObterPorId(id);

    if (servico == null)
        throw new Exception("Cliente não encontrado");

    servico.Atualizar(dto.Nome, dto.Preco);

    await _repo.Atualizar(servico);
	}


    public async Task Delete(Guid id)
        => await _repo.Remover(id);
}
