namespace FCG.Shared.Events;

public class UserCreatedEvent
{
    public Guid UserId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
