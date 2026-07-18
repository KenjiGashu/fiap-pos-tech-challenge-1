namespace Gashu.SistemaMecanica.Infrastructure.Notificacao.Services;

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Gashu.SistemaMecanica.Application.Gateway;

public class CanalNotificacaoEmail : ICanalNotificacao
{
    public async Task EnviarMensagem(string para, string titulo, string corpo)
    {
			string? remetente = Environment.GetEnvironmentVariable("FIAP_POS_EMAIL");
			string? senha = Environment.GetEnvironmentVariable("FIAP_POS_APP_PASSWORD");

			if(remetente == null || senha == null)
			{
				throw new Exception("Notification feature not properly configured");
			}
			
			var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(remetente, senha),
				EnableSsl = true,
			};
		
			var email = new MailMessage
			{
				From = new MailAddress(remetente),
				Subject = titulo,
				Body = corpo,
				IsBodyHtml = false,
			};
		
			email.To.Add(para);
		
			try
			{
				smtp.Send(email);
				Console.WriteLine("Email enviado com sucesso!");
			}
			catch (Exception ex)
			{
				throw ex;
			}

    }
}
