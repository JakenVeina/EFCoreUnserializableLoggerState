using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreUnserializableLoggerState;

public static class EntryPoint
{
    public static async Task Main()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder
            .AddProvider(new MyLoggerProvider()));

        await using var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>()
            .UseLoggerFactory(loggerFactory)
            .Options);

        await context.Database.MigrateAsync();

        var parent = new MyParentEntity(
            id:     1,
            name:   "Test Parent");

        var child = new MyChildEntity(
            id:         1,
            name:       "Test Child",
            parentId:   parent.Id);

        context.Add(parent);
        context.Add(child);

        await context.SaveChangesAsync();
    }
}
