using System.Text;
using Domain.OrdensServico.Entities;

namespace Domain.Notificacao.Entities;

public class AprovacaoOrcamento
{
    public string Mensagem { get; set; }
	  public string Titulo { get; set; }
	  private readonly int numColumns = 45;
	  public Token Token { get; set; }
	  public IEnumerable<ItemOrcamento> Servicos { get; set; }
	  public IEnumerable<ItemOrcamento> Pecas { get; set; }
	  public decimal Total { get; set; }
	  public Guid OrdemServicoId { get; set; }
	  public string NomeCliente { get; set; }
	  public string Destinatario { get; set; }

    public AprovacaoOrcamento(Guid ordemServicoId,
															Token t,
															IEnumerable<ItemOrcamento> servicos,
															IEnumerable<ItemOrcamento> pecas,
															decimal total,
															string nomeCliente,
															string destinatario)
	{
        OrdemServicoId = ordemServicoId;
        Token = t;
        Servicos = servicos;
        Pecas = pecas;
        Total = total;
        NomeCliente = nomeCliente;
        Destinatario = destinatario;

        MontaCorpoMensagem();
				Titulo = $"Orçamento {OrdemServicoId.ToString()}";
    }

    public string MontaStringServicosEPecas(string nome, decimal preco, int quantidade = 0)
		{
        int size = nome.Length + preco.ToString().Length + 2;

				// tem que colocar quantidade na linha?
				if(quantidade > 0)
				{
					size += quantidade.ToString().Length + 3; // + dois espacos brancos ao redor + 'x' colocado na frente
        }
				
        int diff = 0;
        if(size < numColumns)
				{
            diff = numColumns - size;
        }

        string quantidadeString = quantidade > 0 ? " x" + quantidade.ToString() + " " : "";

        return nome + new string(' ', diff) + quantidadeString + "R$" + preco.ToString() + "\n";
    }

		public string MontaCorpoMensagem()
		{
        var sb = new StringBuilder();
        sb.Append("Ola, " + NomeCliente + "\n");
        sb.Append($"Aqui está o orçamento do pedido {OrdemServicoId}\n\n");
        sb.Append($"Serviços:\n");
				foreach(var servico in Servicos)
				{
            sb.Append(MontaStringServicosEPecas(servico.Nome, servico.Preco));
				}

        sb.Append("\n\n");

        sb.Append($"Peças:\n");
				foreach(var peca in Pecas)
				{
					sb.Append(MontaStringServicosEPecas(peca.Nome, peca.Preco, peca.Quantidade));
				}

        sb.Append("\n\n");
        sb.Append($"Total: R${Total}\n\n");

        sb.Append($"Para aprovar Orçamento clique no link abaixo:\n");
        sb.Append(GeraLinkAprovacao() + "\n\n");

        sb.Append($"Para rejeitar Orçamento clique no link abaixo:\n");
        sb.Append(GeraLinkRejeicao() + "\n\n");

        sb.Append($"Att, uma mecanica qualquer");
        Mensagem = sb.ToString();
        return Mensagem;
    }

		public string GeraLinkAprovacao()
		{
        return "http://localhost:5129/api/ordemServico/aprovarOrcamento/" + Token.GuidToken;
    }

    private string GeraLinkRejeicao()
		{
        return "http://localhost:5129/api/ordemServico/rejeitarOrcamento/" + Token.GuidToken;
    }
}
