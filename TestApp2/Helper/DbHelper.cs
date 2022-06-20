using Microsoft.EntityFrameworkCore;
using TestApp2.Data;

namespace TestApp2.Helper;

public class DbHelper
{
    public static void InitDb(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}
