using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;
using TestApp2.Helper;
using TestApp2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.WebHost.UseKestrel();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDefaultIdentity<AppUserEntity>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ShortLinkService>();
builder.Services.AddScoped<ApiAuthService>();
builder.Services.AddControllers();

var app = builder.Build();

DbHelper.InitDb(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
