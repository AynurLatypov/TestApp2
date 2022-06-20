using Microsoft.EntityFrameworkCore;
using TestApp2.Data;

namespace TestApp2.Helper;

public static class DbInit
{
    public static void Init(IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}
