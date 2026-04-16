namespace Application.OrdensServico.DTOs;
using Application.Estoque.DTOs;
using Domain.OrdensServico.Entities;

public class OrdemServicoCreateDto
{
    public Guid ClienteId { get; set; }
    public Guid VeiculoId { get; set; }

    // public List<OrdemServicoServicoDto> Servicos { get; set; }
    // public List<OrdemServicoPecaDto> Pecas { get; set; }
}

public class OrdemServicoResponseDto
{
		public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid VeiculoId { get; set; }
	  public decimal Total { get; set; }
	  public StatusOrdemServico Status { get; set; }

    public List<OrdemServicoServicoDto> Servicos { get; set; }
    public List<OrdemServicoPecaDto> Pecas { get; set; }
}


public class OrdemServicoAtualizaStatusDto
{
	public Guid OrdemServicoId { get; set; }
}

public class OrdemServicoPecaDto
{
	public Guid PecaId { get; set; }
	public decimal Preco { get; set; }
	public int Quantidade { get; set; }
	public string Nome { get; set; }
}

public class OrdemServicoServicoDto
{
	public Guid ServicoId { get; set; }
	public string Nome { get; set; }
	public decimal Preco { get; set; }
}

public class OrdemServicoAdicionaPecaDto
{
	  public Guid OrdemServicoId { get; set; }
    public List<OrdemServicoPecaDto> Pecas { get; set; }
}

public class OrdemServicoAdicionaServicoDto
{
	  public Guid OrdemServicoId { get; set; }
    public List<OrdemServicoServicoDto> Servicos { get; set; }
}

public class OrdemServicoEnviaOrcamentoDto
{
	public Guid OrdemServicoId { get; set; }
}

public class OrdemServicoDeleteDto
{
	public Guid Id { get; set; }
}
