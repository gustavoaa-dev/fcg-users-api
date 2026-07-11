using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FCG.UsersAPI.Infrastructure.Data;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=FCG_Users;Trusted_Connection=True;TrustServerCertificate=True");

        return new UsersDbContext(optionsBuilder.Options);
    }
}
