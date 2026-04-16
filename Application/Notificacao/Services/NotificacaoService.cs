namespace Application.Notificacao.Service;

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Notificacao.Interfaces;
using Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;
using Domain.Notificacao.Interfaces;
using System.Security.Cryptography;

public class NotificacaoService : INotificacaoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IClienteRepository _clienteRepo;
    private readonly INotificacaoRepository _notificacaoRepo;
    private readonly int numColumns = 40;

    public NotificacaoService(IOrdemServicoRepository repo, IClienteRepository clienteRepo, INotificacaoRepository notificacaoRepository)
    {
        _repo = repo;
        _clienteRepo = clienteRepo;
        _notificacaoRepo = notificacaoRepository;
    }

		public string MontaSubject(OrdemServico os)
		{
        return $"Orçamento da Ordem Servico {os.Id}";
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

        return nome + new string(' ', diff) + quantidadeString + "R$" + preco.ToString();
    }

    public string MontaCorpoEmail(OrdemServico os, Cliente cliente, string linkAprovacao, string linkRejeicao)
		{
        var sb = new StringBuilder();
        sb.Append("Ola, " + cliente.Nome + "\n");
        sb.Append($"Aqui está o orçamento do pedido {os.Id}\n\n");
        sb.Append($"Serviços:\n");
				foreach(var servico in os.OrdemServicoServicos)
				{

            Console.WriteLine($"teste: servico {servico} {servico.Id} {servico.Servico} ");
            sb.Append(MontaStringServicosEPecas(servico.Servico.Nome, servico.Preco));
				}

        sb.Append("\n\n");

        sb.Append($"Peças:\n");
				foreach(var peca in os.OrdemServicoPecas)
				{
					sb.Append(MontaStringServicosEPecas(peca.Peca.Nome, peca.Preco, peca.Quantidade));
				}

        sb.Append("\n\n");
        sb.Append($"Total: R${os.Total}\n\n");

        sb.Append($"Para aprovar Orçamento clique no link abaixo:\n");
        sb.Append(linkAprovacao + "\n\n");

        sb.Append($"Para rejeitar Orçamento clique no link abaixo:\n");
        sb.Append(linkRejeicao + "\n\n");

        sb.Append($"Att, uma mecanica qualquer");
        return sb.ToString();
    }

    public static string ComputeSha256Hash(string rawData)
		{
			using (var sha256 = SHA256.Create())
			{
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
				
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
					builder.Append(b.ToString("x2"));
        }
				
        return builder.ToString();
			}
		}

		public string GeraToken()
		{
				string token = Guid.NewGuid().ToString("n");

        return token;
    }

		public void SalvaTokenAprovacao(string token, Guid ordemServicoId)
		{
        string? salt = Environment.GetEnvironmentVariable("FIAP_POS_SALT");

				if(salt == null)
				{
					throw new Exception("Configure salt");
        }
				
        string hashedToken = ComputeSha256Hash(token + salt);
        _notificacaoRepo.SalvaToken(hashedToken, ordemServicoId);
    }

		public string GeraLinkAprovacao(string token)
		{
        return "http://localhost:5129/api/ordemServico/aprovarOrcamento/" + token;
    }

    private string GeraLinkRejeicao(string token)
		{
        return "http://localhost:5129/api/ordemServico/rejeitarOrcamento/" + token;
    }
	
    public async Task EnviarOrcamento(Guid ordemServicoId)
    {
        var os = await _repo.ObterPorId(ordemServicoId);

        if (os == null)
        {
            throw new Exception("ordem servico invalida");
        }

        var cliente = await _clienteRepo.ObterPorId(os.ClienteId);

        string? remetente = Environment.GetEnvironmentVariable("FIAP_POS_EMAIL");
        string? senha = Environment.GetEnvironmentVariable("FIAP_POS_APP_PASSWORD");

				if(remetente == null || senha == null)
				{
            throw new Exception("Notification feature not properly configured");
        }

				if(cliente == null)
				{
            throw new Exception("Cliente invalido");
        }

        Console.WriteLine($"cliente::: {cliente} {cliente.Id} {cliente.Nome}");

        string token = GeraToken();
        SalvaTokenAprovacao(token, os.Id);

        string linkAprovacao = GeraLinkAprovacao(token);
        string linkRejeicao = GeraLinkRejeicao(token);

        string corpoEmail = MontaCorpoEmail(os, cliente, linkAprovacao, linkRejeicao);

        var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(remetente, senha),
				EnableSsl = true,
			};
		
			var email = new MailMessage
			{
				From = new MailAddress(remetente),
				Subject = MontaSubject(os),
				Body = corpoEmail,
				IsBodyHtml = false,
			};
		
			email.To.Add(cliente.Email);
		
			try
			{
				smtp.Send(email);
				Console.WriteLine("Email enviado com sucesso!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Erro: " + ex.Message);
			}
    }
}
