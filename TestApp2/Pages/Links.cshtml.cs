using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestApp2.Helper;
using TestApp2.Models;
using TestApp2.Services;

namespace TestApp2.Pages;

public class LinksModel : AuthorizedPageModel
{
    private readonly ShortLinkService _shortLinkService;

    public LinksModel(ShortLinkService shortLinkService) 
        => _shortLinkService = shortLinkService;

    [BindProperty]
    public string CreateUrl { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public IList<LinkStatModel> LinkEntity { get;set; } = new List<LinkStatModel>();

    public async Task OnGetAsync()
    {
        CreateUrl = string.Empty;
        LinkEntity = await _shortLinkService.GetAll(UserId);
    }

    public async Task OnPostAsync()
    {
        if (Uri.TryCreate(CreateUrl, UriKind.Absolute, out var uri))
            await _shortLinkService.Post(uri, UserId);
        else
            ErrorMessage = $"Не корректная ссылка: '{CreateUrl}'";

        await OnGetAsync();
    }
}
