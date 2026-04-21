using System.Security.Cryptography;
using System.Text;

namespace Domain.OrdensServico.Entities;

public class Token
{
    public Guid Id { get; set; }
    public string GuidToken { get; set; }
    public string HashedToken { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid OrdemServicoId { get; set; }
    public bool Consumido { get; set; }

    public Token(string guidToken, DateTime expirationDate, Guid ordemServicoId)
    {
        GuidToken = guidToken;
        ExpirationDate = expirationDate;
        OrdemServicoId = ordemServicoId;
        HashedToken = ComputeSha256Hash(guidToken);
        Consumido = false;
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

    public bool IsValid()
    {
        return !Consumido && !IsExpired();
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpirationDate;
    }

    public void ConsumirToken()
    {
        this.Consumido = true;
    }
}
