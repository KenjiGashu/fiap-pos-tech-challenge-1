using System.Security.Cryptography;
using System.Text;
using Domain.OrdensServico.Entities;

namespace Application.Notificacao.Interfaces;

public interface INotificacaoService
{
    public string MontaSubject(OrdemServico os);


    public string MontaStringServicosEPecas(string nome, decimal preco, int quantidade = 0);


    public string MontaCorpoEmail(OrdemServico os, Cliente cliente, string linkAprovacao, string linkRejeicao);


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


    public string GeraToken();


    public void SalvaTokenAprovacao(string token, Guid ordemServicoId);

    public string GeraLinkAprovacao(string token);
    public Task EnviarOrcamento(Guid ordemServicoId);
}
