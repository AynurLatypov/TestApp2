using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;
using TestApp2.Helper;
using TestApp2.Models;

namespace TestApp2.Services;

public class ShortLinkService
{
    private readonly AppDbContext _db;

    public ShortLinkService(AppDbContext db) => _db = db;

    public async Task<IList<LinkStatModel>> GetAll(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return new List<LinkStatModel>();

        var links = await _db.Links
            .Where(x => x.CreateBy.Id == userId)
            .Select(x => new { x.Id, x.Url, Count = x.History.Count() })
            .ToListAsync();

        return links.ConvertAll(x => new LinkStatModel
        {
            Id = x.Id,
            Url = x.Url,
            Count = x.Count
        });
    }

    public async Task<LinkEntity> Post(Uri url, string userId)
    {
        var link = new LinkEntity
        {
            Url = url.ToString(),
            CreateById = userId,
            CreatedAt = DateTime.UtcNow,
        };
        _db.Links.Add(link);
        await _db.SaveChangesAsync();
        return link;
    }

    public async Task<string?> GetFullLink(string shortLink)
    {
        var id = ShortLinkHelper.ToInt32(shortLink);

        if (!id.HasValue)
            return null;

        var link = await _db.Links.FindAsync(id.Value);
        return link?.Url;
    }

    public async Task Track(string shortLink, string? ip, string? userAgent)
    {
        var id = ShortLinkHelper.ToInt32(shortLink);
        if (!id.HasValue)
            return;

        var history = new LinkEntryEntity { IpAddress = ip, UserAgent = userAgent, LinkId = id.Value };
        _db.LinkHistory.Add(history);
        await _db.SaveChangesAsync();
    }
}
