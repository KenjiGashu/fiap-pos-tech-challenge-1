namespace Application.Teste.Services;

using Application.Teste.Interfaces;
using Application.Teste.DTOs;
using Domain.Teste.Entities;
using Domain.Teste.Interfaces;
using Domain.Teste.Entities;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _repo;

    public PessoaService(IPessoaRepository repo)
    {
        _repo = repo;
    }

		public async Task<IEnumerable<Blog>> GetAllBlogs()
    {
        var lista = await _repo.ObterTodosBlogs();
        return lista;
    }

    public async Task<IEnumerable<PessoaResponseDto>> GetAll()
    {
        var lista = await _repo.ObterTodos();
				
        return lista.Select(p => new PessoaResponseDto{
            Nome = p.Nome,
						Pedidos = p.Pedidos?.Select(pedido => new PedidoDto{
								Data = pedido.Data,
							}).ToList() ?? new List<PedidoDto>()
        });
    }

    public async Task<PessoaResponseDto?> GetById(Guid id)
		{
			var pessoa = await _repo.ObterPorId(id);
        return new PessoaResponseDto
        {
            Nome = pessoa.Nome
        };
    }

    public async Task Create(PessoaDto dto)
    {
			var pessoa = new Pessoa{
				PessoaId = Guid.NewGuid(),
				Nome = dto.Nome
			};
      await _repo.Adicionar(pessoa);
    }

	public async Task Update(Guid id, PessoaDto dto)
	{
    var pessoa = await _repo.ObterPorId(id);

    if (pessoa == null)
        throw new Exception("Cliente não encontrado");

    pessoa.Atualizar(dto.Nome);

    await _repo.Atualizar(pessoa);
	}

    // public async Task Create(string nome, string email)
    // {
    //     var cliente = new Cliente(nome, email);
    //     await _repository.Adicionar(cliente);
    // }

    public async Task Delete(Guid id)
        => await _repo.Remover(id);


		public async Task AdicionaPedido(Guid pessoaId, PedidoDto dto)
		{
        Console.WriteLine($"[AdicionaPedido] {pessoaId}");
        var novoPedido = new Pedido
        {
            PedidoId = dto.PedidoId,
            Data = dto.Data
        };

        await _repo.AdicionaPedido(pessoaId, novoPedido);
    }

		public async Task AdicionaPedidos(Guid pessoaId, IEnumerable<PedidoDto> dtos)
		{
        var novoPedidos = dtos.Select(p => new Pedido
        {
            PedidoId = p.PedidoId,
            Data = p.Data
        });
        await _repo.AdicionaPedidos(pessoaId, novoPedidos);
    }

}
