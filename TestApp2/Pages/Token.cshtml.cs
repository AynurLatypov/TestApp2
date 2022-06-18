using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;
using TestApp2.Helper;
using TestApp2.Services;

namespace TestApp2.Pages
{
    public class TokenModel : AuthorizedPageModel
    {
        private readonly ApiAuthService _service;

        public TokenModel(ApiAuthService service)
        {
            _service = service;
        }

        public IList<ApiTokenEntity> ApiTokenEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(UserId))
                return;

            ApiTokenEntity = await _service.GetAll(UserId);
        }

        public async Task OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserId))
                return;
            
            await _service.CreateToken(UserId);
            ApiTokenEntity = await _service.GetAll(UserId);
        }
    }
}
