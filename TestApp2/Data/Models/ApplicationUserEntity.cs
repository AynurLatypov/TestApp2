using Microsoft.AspNetCore.Identity;

namespace TestApp2.Data.Models;

public class AppUserEntity : IdentityUser
{
    public virtual List<LinkEntity> Links { get; set; } = new List<LinkEntity>();
    public virtual List<ApiTokenEntity> Tokens { get; set; } = new List<ApiTokenEntity>();
}
