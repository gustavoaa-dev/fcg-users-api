namespace FCG.UsersAPI.Application.DTOs;

public class UsuarioResponseDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
