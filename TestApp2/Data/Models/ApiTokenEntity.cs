using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp2.Data.Models;

public class ApiTokenEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("API key")]
    public string ApiToken => Id.ToString("N");

    [DisplayName("Активен до")]
    public DateTime ExpiresAt { get; set; }

    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AppUserEntity AppUser { get; set; }
}
