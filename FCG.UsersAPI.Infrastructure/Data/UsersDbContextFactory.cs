using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FCG.UsersAPI.Infrastructure.Data;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=FCG_Users;User Id=sa;Password=Fcg2024Test!;Encrypt=False;TrustServerCertificate=True");

        return new UsersDbContext(optionsBuilder.Options);
    }
}
