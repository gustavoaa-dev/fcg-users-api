# FCG Users API

Microsserviço responsável pelo cadastro, autenticação e autorização de usuários da plataforma FCG (Fiap Cloud Games).

## Funcionalidades

- Cadastro de novos usuários
- Autenticação via JWT (JSON Web Token)
- Controle de acesso por perfis (Admin e Usuario)
- Publicação de evento `UserCreatedEvent` ao cadastrar um novo usuário

## Tecnologias

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core + SQL Server
- JWT Bearer Authentication
- MassTransit + RabbitMQ
- Swagger / OpenAPI

## Como executar

### Pré-requisitos

- .NET SDK 8
- SQL Server (local ou container)
- RabbitMQ (local ou container)

### Executar localmente

```bash
# Nenhuma credencial é versionada: a string de conexão e a chave JWT vêm do ambiente
# (no cluster, dos Secrets do Kubernetes). Sem elas a API sobe, mas não conecta no banco.
export ConnectionStrings__DefaultConnection='Server=127.0.0.1;Database=FCG_Users;User Id=sa;Password=<sua-senha>;TrustServerCertificate=True'
export Jwt__SecretKey='<a mesma chave usada pelo catalog-api e pelo Kong>'

dotnet run --project FCG.UsersAPI.API
```

A API estará disponível em `http://localhost:5105`.

### Com Docker

```bash
docker build -t fcg-users-api .
docker run -p 5001:8080 fcg-users-api
```

### Com Docker Compose

No repositório [fcg-orchestration](https://github.com/gustavoaa-dev/fcg-orchestration), execute:

```bash
docker-compose up -d
```

## Variáveis de ambiente

O `appsettings.json` **não** carrega senha nem chave JWT: as credenciais vêm só daqui — no cluster, dos Secrets do Kubernetes (ver [fcg-orchestration](https://github.com/gustavoaa-dev/fcg-orchestration), seção *Segredos*).

| Variável | Descrição | Padrão |
|---|---|---|
| `RABBITMQ_HOST` | Host do RabbitMQ | `localhost` |
| `ConnectionStrings__DefaultConnection` | String de conexão SQL Server | — |
| `Jwt__SecretKey` | Chave de assinatura JWT | — |
| `Jwt__Issuer` | Emissor do token JWT | `FCG.UsersAPI` |
| `Jwt__Audience` | Audiência do token JWT | `FCG.Client` |
| `Jwt__ExpiracaoHoras` | Tempo de expiração do token | `8` |

## Endpoints

| Método | Rota | Autenticação | Descrição |
|---|---|---|---|
| POST | `/api/usuarios` | Pública | Cadastrar usuário |
| GET | `/api/usuarios` | Admin | Listar usuários |
| POST | `/api/auth/login` | Pública | Login e obter token JWT |
