using FCG.UsersAPI.Application.DTOs;
using FCG.UsersAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCG.UsersAPI.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO dto)
    {
        try
        {
            var tokenResponse = await _authService.GerarToken(dto);
            return Ok(tokenResponse);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
