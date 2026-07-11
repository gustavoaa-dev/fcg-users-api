namespace FCG.UsersAPI.Application.DTOs;

public class TokenResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
}
