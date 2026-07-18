namespace Gashu.SistemaMecanica.Application.Notificacao.Services;

public class ItemOrcamentoDto
{
	public required string Nome { get; set; }
	public required decimal Preco { get; set; }
	public required int Quantidade { get; set; }

}

public class AprovacaoOrcamentoDto
{
	  public required Guid OrdemServicoId { get; set; }
	  public required string TokenGuid { get; set; }
	  public required IEnumerable<ItemOrcamentoDto> Servicos { get; set; }
	  public required IEnumerable<ItemOrcamentoDto> Pecas { get; set; }
	  public required decimal Total { get; set; }
	  public required string NomeCliente { get; set; }
	  public required string Destinatario { get; set; }

}

public class AprovarOrcamentoDto
{
    public required Guid Guid;
}
