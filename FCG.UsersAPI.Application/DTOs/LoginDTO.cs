using System.ComponentModel.DataAnnotations;

namespace FCG.UsersAPI.Application.DTOs;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}
