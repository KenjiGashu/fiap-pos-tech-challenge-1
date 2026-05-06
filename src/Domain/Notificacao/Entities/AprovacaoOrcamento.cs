using System.Text;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

namespace Gashu.SistemaMecanica.Domain.Notificacao.Entities;

public class AprovacaoOrcamento
{
    public string Mensagem { get; set; }
    public string Titulo { get; set; }
    private readonly int numColumns = 45;
    public string TokenGuid { get; set; }
    public IEnumerable<ItemOrcamento> Servicos { get; set; }
    public IEnumerable<ItemOrcamento> Pecas { get; set; }
    public decimal Total { get; set; }
    public Guid OrdemServicoId { get; set; }
    public string NomeCliente { get; set; }
    public string Destinatario { get; set; }

    public AprovacaoOrcamento(Guid ordemServicoId,
                              string tokenGuid,
                              IEnumerable<ItemOrcamento> servicos,
                              IEnumerable<ItemOrcamento> pecas,
                              decimal total,
                              string nomeCliente,
                              string destinatario)
    {
        OrdemServicoId = ordemServicoId;
        TokenGuid = tokenGuid;
        Servicos = servicos;
        Pecas = pecas;
        Total = total;
        NomeCliente = nomeCliente;
        Destinatario = destinatario;

        Mensagem = MontaCorpoMensagem();
        Titulo = $"Orçamento {OrdemServicoId.ToString()}";
    }

    public string MontaStringServicosEPecas(string nome, decimal preco, TipoItemOrcamento tipo, int quantidade = 0)
    {
        int size = nome.Length + preco.ToString().Length + 2;

        // tem que colocar quantidade na linha?
        if(tipo == TipoItemOrcamento.Peca)
        {
            size += quantidade.ToString().Length + 3; // + dois espacos brancos ao redor + 'x' colocado na frente
        }
                
        int diff = 0;
        if(size < numColumns)
        {
            diff = numColumns - size;
        }

        string quantidadeString = tipo == TipoItemOrcamento.Peca ? " x" + quantidade.ToString() + " " : "";

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
                    sb.Append(MontaStringServicosEPecas(servico.Nome, servico.Preco, servico.Tipo));
                }

        sb.Append("\n\n");

        sb.Append($"Peças:\n");
        foreach(var peca in Pecas)
                {
                    sb.Append(MontaStringServicosEPecas(peca.Nome, peca.Preco, peca.Tipo, peca.Quantidade));
                }

        sb.Append("\n\n");
        sb.Append($"Total: R${Total}\n\n");

        sb.Append($"Para aprovar Orçamento clique no link abaixo:\n");
        sb.Append(GeraLinkAprovacao() + "\n\n");

        sb.Append($"Para rejeitar Orçamento clique no link abaixo:\n");
        sb.Append(GeraLinkRejeicao() + "\n\n");

        sb.Append($"Att, uma mecanica qualquer");
        return sb.ToString();
    }

    public string GeraLinkAprovacao()
    {
        return "http://localhost:5129/api/ordemServico/aprovarOrcamento/" + TokenGuid;
    }

    private string GeraLinkRejeicao()
    {
        return "http://localhost:5129/api/ordemServico/rejeitarOrcamento/" + TokenGuid;
    }
}
