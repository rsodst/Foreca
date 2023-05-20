using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Foreca.Infrastructure.Database.Database;

// Утилитарный тип, обеспечивающий создание контекста при работе с миграциями через dotnet-ef
public class ContextFactory : IDesignTimeDbContextFactory<ForecaContext>
{
    public ForecaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ForecaContext>()
            .UseNpgsql("User ID=;Password=;Host=localhost;Port=5432;Database=;Pooling=true;");

        return new ForecaContext(optionsBuilder.Options);
    }
}