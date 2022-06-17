using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data.Models;

namespace TestApp2.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserEntity>
    {
        public DbSet<LinkEntity> Links { get; set; }

        public DbSet<LinkEntryEntity> LinkHistory { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}