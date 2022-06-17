using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;
using TestApp2.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUserEntity>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/{shortLink}", async ctx =>
{
    var db = ctx.RequestServices.GetRequiredService<ApplicationDbContext>();
    var sh = ctx.GetRouteValue("shortLink")?.ToString() ?? throw new Exception();
    var link = await db.Links.FindAsync(GuidHelper.FromShortString(sh));
    if (link == null)
    {
        ctx.Response.Redirect("/notfound");
        return;
    }

    var ip = ctx.Connection.RemoteIpAddress?.ToString();
    var agent = ctx.Request.Headers.UserAgent.ToString();
    var history = new LinkEntryEntity { IpAddress = ip, UserAgent = agent, Link = link };
    db.LinkHistory.Add(history);
    await db.SaveChangesAsync();
    ctx.Response.Redirect(link.Url);
});

app.Run();
