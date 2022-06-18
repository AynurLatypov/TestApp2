using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp2.Helper;

namespace TestApp2.Data.Models;

public class LinkEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(2048)]
    [DisplayName("Ссылка")]
    public string Url { get; set; }

    [NotMapped]
    [DisplayName("Короткая ссылка")]
    public string ShortUrl => ShortLinkHelper.ToString(Id);

    [DisplayName("Создано")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string CreateById { get; set; }

    [ForeignKey(nameof(CreateById))]
    public virtual AppUserEntity CreateBy { get; set; }

    public virtual List<LinkEntryEntity> History { get; set; } = new List<LinkEntryEntity>();
}
