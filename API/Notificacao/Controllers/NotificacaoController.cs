

using Application.Notificacao.Interfaces;
using Application.OrdensServico.Services;
using Domain.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotificacaoController : ControllerBase
{
    private readonly INotificacaoService _service;
    private readonly OrdemServicoService _osService;
    private readonly ClienteService _clienteService;
    private readonly IOrdemServicoRepository _osrepo;
    private readonly IClienteRepository _clienterepo;

    public NotificacaoController(INotificacaoService service, OrdemServicoService osService, ClienteService clienteService,
                                                                 IOrdemServicoRepository osrepo, IClienteRepository clienterepo)
    {
        _service = service;
        _osService = osService;
        _clienteService = clienteService;
        _osrepo = osrepo;
        _clienterepo = clienterepo;
    }

    // [HttpGet]
    // public async Task<IActionResult> Get()
    //     => Ok(await _service.GetAll());

    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(Guid id)
    // {
    //     var peca = await _service.GetById(id);
    //     if (peca == null) return NotFound();
    //     return Ok(peca);
    // }

    [HttpGet("mensagemCorpo/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var os = await _osrepo.ObterPorId(id);
        var cliente = await _clienterepo.ObterPorId(os.ClienteId);

        Console.WriteLine($"os: {os} cliente: {cliente}");

        var resposta = _service.MontaCorpoEmail(os, cliente, "a", "b");

        return Ok(resposta);
    }

    //     [HttpPost]
    //     public async Task<IActionResult> Post([FromBody] PecaCreateDto dto)
    //     {
    //         await _service.Create(dto);
    //         return Ok();
    //     }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(Guid id, [FromBody] PecaUpdateDto dto)
    // {
    // 	await _service.Update(id, dto);
    // 	return Ok();
    // }

    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> Delete(Guid id)
    //     {
    //         await _service.Delete(id);
    //         return Ok();
    //     }

    // }

}
