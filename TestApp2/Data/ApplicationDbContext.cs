using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data.Models;

namespace TestApp2.Data;

public class AppDbContext : IdentityDbContext<AppUserEntity>
{
    public DbSet<LinkEntity> Links { get; set; }

    public DbSet<LinkEntryEntity> LinkHistory { get; set; }

    public DbSet<ApiTokenEntity> ApiTokens { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

}