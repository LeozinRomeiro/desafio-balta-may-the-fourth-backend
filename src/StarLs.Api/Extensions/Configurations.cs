using Microsoft.EntityFrameworkCore;
using StarLs.Infra.Context;

namespace StarLs.Api.Extensions;

public static class Configurations
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connString = "DataSource=app.db;Cache=Shared";

        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlite(connString, b => b.MigrationsAssembly("StarLs.Api"));
        });
    }
}
