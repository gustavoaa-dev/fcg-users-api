using System.ComponentModel.DataAnnotations;

namespace FCG.UsersAPI.Application.DTOs;

public class CriarUsuarioDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Senha { get; set; } = string.Empty;
}
