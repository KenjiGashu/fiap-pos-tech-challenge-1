namespace Tests.Domain.Notificacao;

using global::Domain.Notificacao.Entities;
using global::Domain.OrdensServico.Entities;

public class AprovacaoOrcamentoTestes
{
    AprovacaoOrcamento ao;
    Guid ordemServicoId = Guid.NewGuid();
    string tokenGuid = Guid.NewGuid().ToString("n");
    Token token;
    List<ItemOrcamento> servicos = new List<ItemOrcamento>();
	  List<ItemOrcamento> pecas = new List<ItemOrcamento>();
    decimal total;
    string nomeCliente = "nome do cliente";
    string destinatario = "email do cliente";

    public AprovacaoOrcamentoTestes()
	  {
			  token = new Token(tokenGuid, DateTime.Now.AddDays(1), ordemServicoId);

        var servico = new ItemOrcamento("troca oleo", 12, 0, TipoItemOrcamento.Servico);
        servicos.Add(servico);

        var peca = new ItemOrcamento("pneu", 10, 1, TipoItemOrcamento.Peca);
        pecas.Add(peca);

        total = 22;

        ao = new AprovacaoOrcamento(ordemServicoId, token.GuidToken, servicos, pecas, total, nomeCliente, destinatario);
    }

    [Fact]
	  public async Task MontaStringServicosEPecasTeste_LinhaServico()
	  {
        var servico = ao.Servicos.First();
        var expected = "troca oleo                               R$12\n";

        var result = ao.MontaStringServicosEPecas(servico.Nome, servico.Preco, servico.Tipo, servico.Quantidade);

        Assert.Contains(expected, result);
    }

    [Fact]
	  public async Task MontaStringServicosEPecasTeste_LinhaPeca()
	  {
        var peca = ao.Pecas.First();
        var expected = "pneu                                  x1 R$10\n";

        var result = ao.MontaStringServicosEPecas(peca.Nome, peca.Preco, peca.Tipo, peca.Quantidade);

        Assert.Equal(expected, result);
    }

	  [Fact]
	  public async Task MontaCorpoMensagemTeste()
	  {
        var expected = $"Ola, nome do cliente\n" +
				 $"Aqui está o orçamento do pedido {ordemServicoId}\n\n" +
					$"Serviços:\n" +
					$"troca oleo                               R$12\n\n\n" +
					$"Peças:\n" +
					$"pneu                                  x1 R$10\n\n\n" +
					$"Total: R$22\n\n" +
					$"Para aprovar Orçamento clique no link abaixo:\n" + 
					$"http://localhost:5129/api/ordemServico/aprovarOrcamento/{tokenGuid}\n\n" +
					$"Para rejeitar Orçamento clique no link abaixo:\n" +
					$"http://localhost:5129/api/ordemServico/rejeitarOrcamento/{tokenGuid}\n\n" +
					$"Att, uma mecanica qualquer";

        var result = ao.MontaCorpoMensagem();

        Assert.Equal(expected, result);
    }
}
