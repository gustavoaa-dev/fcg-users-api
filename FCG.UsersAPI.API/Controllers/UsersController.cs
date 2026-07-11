using FCG.UsersAPI.Application.DTOs;
using FCG.UsersAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.UsersAPI.API.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDTO>> Criar([FromBody] CriarUsuarioDTO dto)
    {
        try
        {
            var usuario = await _userService.CriarUsuario(dto);
            return Created(string.Empty, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> ObterTodos()
    {
        try
        {
            var usuarios = await _userService.ObterTodos();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
