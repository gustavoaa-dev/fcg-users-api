using FCG.UsersAPI.Domain.Entities;

namespace FCG.UsersAPI.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> ObterPorEmail(string email);
    Task<User?> ObterPorId(Guid id);
    Task<IEnumerable<User>> ObterTodos();
    Task Adicionar(User user);
    Task Salvar();
}
