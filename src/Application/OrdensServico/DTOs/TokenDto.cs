namespace Application.OrdensServico.DTOs;

public class TokenResponseDto
{
		public required Guid Id { get; set; }
		public required string GuidToken { get; set; }
    public required string HashedToken { get; set; }
 		public required DateTime ExpirationDate { get; set; }
    public required Guid OrdemServicoId { get; set; }
	  public required bool Consumido { get; set; }
}
