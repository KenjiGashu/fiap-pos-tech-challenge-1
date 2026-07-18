namespace Gashu.SistemaMecanica.Application.OrdensServico.DTOs;

using System.Text.Json.Serialization;
using Gashu.SistemaMecanica.Application.Estoque.Services;
using Gashu.SistemaMecanica.Application.Identidade.Services;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public class OrdemServicoCreateDto
{
    public required Guid ClienteId { get; set; }
    public required Guid VeiculoId { get; set; }
}

public class OrdemServicoCreateDtoTodosDados
{
    public required CriarUsuarioDto Usuario  { get; set; }
    public required ClienteCreateDto Cliente  { get; set; }
    public required VeiculoCreateDto Veiculo { get; set; }
    public required List<OrdemServicoPecaDto> Pecas { get; set; }
    public required List<OrdemServicoServicoDto> Servicos { get; set; }
}

public class OrdemServicoResponseDto
{
    public required Guid Id { get; set; }
    public required Guid ClienteId { get; set; }
    public required Guid VeiculoId { get; set; }
    public required decimal Total { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required StatusOrdemServico Status { get; set; }

    public required List<OrdemServicoServicoDto> Servicos { get; set; }
    public required List<OrdemServicoPecaDto> Pecas { get; set; }
}

public class ListaOrdemServicoResponseDto
{
    public required List<OrdemServicoResponseDto> OrdemServicos { get; set; }
}


public class OrdemServicoAtualizaStatusDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoPecaDto
{
    public required Guid PecaId { get; set; }
    public required decimal Preco { get; set; }
    public required int Quantidade { get; set; }
    public required string Nome { get; set; }
}

public class OrdemServicoServicoDto
{
    public required Guid ServicoId { get; set; }
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
}

public class OrdemServicoAdicionaPecaDto
{
    public required Guid OrdemServicoId { get; set; }
    public required List<OrdemServicoPecaDto> Pecas { get; set; }
}

public class OrdemServicoAdicionaServicoDto
{
    public required Guid OrdemServicoId { get; set; }
    public required List<OrdemServicoServicoDto> Servicos { get; set; }
}

public class OrdemServicoEnviarOrcamentoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoIniciarDiagnosticoOrcamentoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoFinalizarDiagnosticoOrcamentoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoIniciarExecucaoOrcamentoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoFinalizarExecucaoOrcamentoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoEntregarVeiculoDto
{
    public required Guid OrdemServicoId { get; set; }
}

public class OrdemServicoAprovarOrcamentoDto
{
    public required Guid TokenGuid { get; set; }
}

public class OrdemServicoRejeitarOrcamentoDto
{
    public required Guid TokenGuid { get; set; }
}

public class OrdemServicoDeleteDto
{
    public required Guid Id { get; set; }
}
