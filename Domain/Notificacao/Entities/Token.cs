namespace Domain.Notificacao.Entities;

public class Token
{
	  public Guid Id { get; set; }
    public string HashedToken { get; set; }
 		public DateTime ExpirationDate { get; set; }
    public Guid OrdemServicoId { get; set; }

	  public Token(string hashedToken, DateTime expirationDate, Guid ordemServicoId)
	  {
        HashedToken = hashedToken;
        ExpirationDate = expirationDate;
        OrdemServicoId = ordemServicoId;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpirationDate;
    }
}
