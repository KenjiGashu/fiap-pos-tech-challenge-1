namespace Tests.Domain.OrdensServico;

using global::Domain.OrdensServico.Entities;

public class OrdemServicoTests
{

    private OrdemServico os;
    private Guid clienteId;
    private Guid veiculoId;

    public OrdemServicoTests()
	  {
        clienteId = Guid.NewGuid();
        veiculoId = Guid.NewGuid();

        os = new OrdemServico(clienteId, veiculoId);
    }

	  [Fact]
	  public async Task OrdemServico_AdicionaServicoTest()
	  {
        var servicoId = Guid.NewGuid();
        decimal preco = 12;
        var nome = "troca oleo";

        os.AdicionarServico(servicoId, preco, nome);

        Assert.Equal(preco, os.Total);
        Assert.Equal(1, os.OrdemServicoServicos.Count());
    }

    [Fact]
	  public async Task OrdemServico_WhenStateEmDiagnostico_AdicionaServicoShould_DeveAprovarDeNovo_True()
	  {
        var servicoId = Guid.NewGuid();
        decimal preco = 12;
        var nome = "troca oleo";

        os.Status = StatusOrdemServico.EmDiagnostico;

        os.AdicionarServico(servicoId, preco, nome);

        Assert.Equal(preco, os.Total);
        Assert.Equal(true, os.deveAprovarDeNovo);
        Assert.Equal(1, os.OrdemServicoServicos.Count());
    }

    [Fact]
	  public async Task OrdemServico_WhenStateEmDiagnostico_AdicionaPecaShould_DeveAprovarDeNovo_True()
	  {
        var pecaId = Guid.NewGuid();
        decimal preco = 12;
        int quantidade = 1;
        var nome = "ipad";

        os.Status = StatusOrdemServico.EmDiagnostico;

        os.AdicionarPeca(pecaId, preco, quantidade, nome);

        Assert.Equal(preco, os.Total);
        Assert.Equal(true, os.deveAprovarDeNovo);
        Assert.Equal(1, os.OrdemServicoPecas.Count());
    }

	  [Fact]
	  public async Task OrdemServico_AdicionaPecaShouldThrownWhenInvalidQuantidade()
	  {
        var pecaId = Guid.NewGuid();
        decimal preco = 12;
        int quantidade = 0;
        var nome = "ipad";

        Assert.Throws<Exception>(() => os.AdicionarPeca(pecaId, preco, quantidade, nome));
    }

	  [Fact]
	  public async Task OrdemServico_AdicionaPecaTest()
	  {
        var pecaId = Guid.NewGuid();
        decimal preco = 12;
        int quantidade = 1;
        var nome = "ipad";

        os.AdicionarPeca(pecaId, preco, quantidade, nome);

        Assert.Equal(preco, os.Total);
        Assert.Equal(1, os.OrdemServicoPecas.Count());
    }

    [Fact]
	  public async Task OrdemServico_RecalcularTotalShouldProperlyCalculate()
	  {
        var pecaId = Guid.NewGuid();
        decimal preco = 12;
        int quantidade = 1;
        var nome = "ipad";

        var servicoId = Guid.NewGuid();
        decimal precoServico = 12;
        var nomeServico = "troca oleo";

        os.AdicionarPeca(pecaId, preco, quantidade, nome);
        os.AdicionarServico(servicoId, precoServico, nomeServico);

        Assert.Equal(preco+precoServico, os.Total);
        Assert.Equal(1, os.OrdemServicoPecas.Count());
        Assert.Equal(1, os.OrdemServicoServicos.Count());
    }

	  [Fact]
	  public async Task OrdemServico_AprovarOrcamentoTest_WhenAguardandoAprovacao_ShouldGoToOrcamentoAprovado()
	  {
        os.Status = StatusOrdemServico.AguardandoAprovacao;

        os.AprovarOrcamento();

        Assert.Equal( StatusOrdemServico.OrcamentoAprovado, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_AprovarOrcamentoTest_WhenAguardandoAprovacaoRevisao_ShouldGoToAguardandoMecanico()
	  {
        os.Status = StatusOrdemServico.AguardandoAprovacaoRevisao;

        os.AprovarOrcamento();

        Assert.Equal( StatusOrdemServico.AguardandoMecanico, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_IniciarDiagnosticoTest()
	  {
        os.IniciarDiagnostico();

        Assert.Equal( StatusOrdemServico.EmDiagnostico, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_FinalizarDiagnosticoTest()
	  {
        os.FinalizarDiagnostico();

        Assert.Equal( StatusOrdemServico.AguardandoMecanico, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_FinalizarDiagnosticoTest_WhenDeveAprovarDeNovo_ShouldGoToAprovacao()
	  {
        os.deveAprovarDeNovo = true;

        os.FinalizarDiagnostico();

        Assert.Equal( StatusOrdemServico.AguardandoAprovacaoRevisao, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_IniciarExecucaoTest()
	  {
        os.IniciarExecucao();

        Assert.Equal( StatusOrdemServico.EmExecucao, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_FinalizarExecucaoTest()
	  {
        os.FinalizarExecucao();

        Assert.Equal( StatusOrdemServico.Finalizada, os.Status);
    }

    [Fact]
	  public async Task OrdemServico_EntergarVeiculoTest()
	  {
        os.EntregarVeiculo();

        Assert.Equal( StatusOrdemServico.Entregue, os.Status);
    }

}
