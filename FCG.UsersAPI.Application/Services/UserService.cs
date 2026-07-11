using FCG.UsersAPI.Application.DTOs;
using FCG.UsersAPI.Domain.Entities;
using FCG.UsersAPI.Domain.Enums;
using FCG.UsersAPI.Domain.Events;
using FCG.UsersAPI.Domain.Interfaces;
using MassTransit;

namespace FCG.UsersAPI.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UserService(IUserRepository userRepository, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<UsuarioResponseDTO> CriarUsuario(CriarUsuarioDTO dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto), "Os dados do usuário são obrigatórios.");

        if (!User.EmailValido(dto.Email))
            throw new ArgumentException("O e-mail informado é inválido.");

        if (!User.SenhaValida(dto.Senha))
            throw new ArgumentException("A senha deve ter no mínimo 8 caracteres, com pelo menos uma letra, um número e um caractere especial.");

        var usuarioExistente = await _userRepository.ObterPorEmail(dto.Email);
        if (usuarioExistente is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var user = new User(dto.Nome, dto.Email, senhaHash, UserRole.Usuario);

        await _userRepository.Adicionar(user);
        await _userRepository.Salvar();

        await _publishEndpoint.Publish(new UserCreatedEvent
        {
            UserId = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            DataCadastro = user.DataCadastro
        });

        return MapearParaResponse(user);
    }

    public async Task<IEnumerable<UsuarioResponseDTO>> ObterTodos()
    {
        var usuarios = await _userRepository.ObterTodos();
        return usuarios.Select(MapearParaResponse).ToList();
    }

    private static UsuarioResponseDTO MapearParaResponse(User user)
    {
        return new UsuarioResponseDTO
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Role = user.Role.ToString(),
            DataCadastro = user.DataCadastro
        };
    }
}
