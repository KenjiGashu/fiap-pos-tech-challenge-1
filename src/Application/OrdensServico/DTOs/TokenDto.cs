namespace Application.OrdensServico.DTOs;

public class TokenResponseDto
{
		public Guid Id { get; set; }
		public string GuidToken { get; set; }
    public string HashedToken { get; set; }
 		public DateTime ExpirationDate { get; set; }
    public Guid OrdemServicoId { get; set; }
	  public bool Consumido { get; set; }
}
