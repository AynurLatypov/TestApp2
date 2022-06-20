using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Helper;
using TestApp2.Models;
using TestApp2.Services;

namespace TestApp2.Controllers;

[ApiController]
[Route("/api/links")]
public class LinkController : Controller
{
    private readonly ShortLinkService _linkService;
    private readonly AppDbContext _db;
    private readonly ApiAuthService _api;

    public LinkController(ShortLinkService linkService, AppDbContext db, ApiAuthService api)
    {
        _linkService = linkService;
        _db = db;
        _api = api;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string apikey)
    {
        var userId = await _api.GetUserId(apikey);
        if (userId == null)
            return Unauthorized();

        var links = await _linkService.GetAll(userId);
        return Ok(links);
    }

    [HttpGet("{shortLink}")]
    public async Task<IActionResult> Get([FromRoute]string shortLink, [FromQuery] string apikey)
    {
        var id = ShortLinkHelper.ToInt32(shortLink);

        if (!id.HasValue)
            return NotFound();

        var userId = await _api.GetUserId(apikey);
        if (userId == null)
            return Unauthorized();

        var link = await _db.Links
            .Include(x => x.History)
            .FirstOrDefaultAsync(x => x.Id == id && userId == x.CreateBy.Id);

        if (link == null)
        {
            return NotFound();
        }

        link.History.ForEach(x => x.Link = null);

        return Ok(link);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UrlRequest model, [FromQuery] string apikey)
    {
        var userId = await _api.GetUserId(apikey);
        if (userId == null)
            return Unauthorized();
        
        var link = await _linkService.Post(model.Url, userId);
        return Ok(link);
    }
}
