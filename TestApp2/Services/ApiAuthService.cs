using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;

namespace TestApp2.Services;

public class ApiAuthService
{
    private readonly AppDbContext _db;

    public ApiAuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string?> GetUserId(string apiKey)
    {
        var tokenId = Guid.Parse(apiKey);

        var token = await _db.ApiTokens.FindAsync(tokenId);
        return token?.UserId;
    }

    public async Task<IList<ApiTokenEntity>> GetAll(string userId) 
        => await _db.ApiTokens.Where(a => a.UserId == userId).ToListAsync();

    public async Task CreateToken(string userId)
    {
        var token = new ApiTokenEntity
        {
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            Id = Guid.NewGuid()
        };

        _db.ApiTokens.Add(token);
        await _db.SaveChangesAsync();
    }
}
