FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FCG.UsersAPI.sln .
COPY FCG.UsersAPI.API/FCG.UsersAPI.API.csproj FCG.UsersAPI.API/
COPY FCG.UsersAPI.Application/FCG.UsersAPI.Application.csproj FCG.UsersAPI.Application/
COPY FCG.UsersAPI.Domain/FCG.UsersAPI.Domain.csproj FCG.UsersAPI.Domain/
COPY FCG.UsersAPI.Infrastructure/FCG.UsersAPI.Infrastructure.csproj FCG.UsersAPI.Infrastructure/

RUN dotnet restore

COPY . .

RUN dotnet publish FCG.UsersAPI.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "FCG.UsersAPI.API.dll"]
