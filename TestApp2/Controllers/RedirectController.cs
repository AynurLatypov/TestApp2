using Microsoft.AspNetCore.Mvc;
using TestApp2.Services;

namespace TestApp2.Controllers;

[Route("/")]
public class RedirectController : Controller
{
    private readonly ShortLinkService _service;

    public RedirectController(ShortLinkService service) 
        => _service = service;

    [HttpGet("{shortId}")]
    public async Task<IActionResult> Get([FromRoute] string shortId)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers.UserAgent.ToString();

        var link = await _service.GetFullLink(shortId);
        if (link != null)
            await _service.Track(shortId, ip, userAgent);

        return Redirect(link ?? "/NotFound");
    }
}
