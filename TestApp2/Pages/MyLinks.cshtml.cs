using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestApp2.Data;
using TestApp2.Models;

namespace TestApp2.Pages;

public class MyLinksModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public MyLinksModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<LinkStatModel> LinkEntity { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (_context.Links == null)
        {
            Redirect("/");
            return;
        }

        var links = await _context.Links
            .Where(x => x.CreateBy.Id == userId)
            .Select(x => new { x.Id, x.Url, Count = x.History.Count() })
            .ToListAsync();

        LinkEntity = links.ConvertAll(x => new LinkStatModel
        {
            Id = x.Id,
            Url = x.Url,
            Count = x.Count
        });
    }
}
