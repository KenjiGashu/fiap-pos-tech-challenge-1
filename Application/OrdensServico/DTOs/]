namespace Application.OrdensServico.DTOs;
using Application.Estoque.DTOs;

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

    public List<OrdemServicoServicoDto> Servicos { get; set; }
    public List<OrdemServicoPecaDto> Pecas { get; set; }
}


public class OrdemServicoAtualizaStatusDto
{
    public Guid OrdemServicoId;
}

public class OrdemServicoPecaDto
{
    public Guid Id;
    public decimal Preco;
    public int Quantidade;
}

public class OrdemServicoServicoDto
{
    public Guid Id;
    public decimal Preco;
}

public class OrdemServicoAdicionaPecaDto
{
    public Guid OrdemServicoId;
    public List<OrdemServicoPecaDto> Pecas { get; set; }
}

public class OrdemServicoAdicionaServicoDto
{
    public Guid OrdemServicoId;
    public List<OrdemServicoServicoDto> Servicos { get; set; }
}
