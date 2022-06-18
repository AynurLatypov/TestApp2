using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TestApp2.Helper;

namespace TestApp2.Pages;

public class AuthorizedPageModel : PageModel
{
    private readonly Lazy<string> _userId;
    public string UserId => _userId.Value;

    public AuthorizedPageModel()
    {
        _userId = new Lazy<string>(() => User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
