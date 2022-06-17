using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp2.Data.Models;

public class LinkEntryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public virtual LinkEntity Link { get; set; }

    [DisplayName("Время")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DisplayName("IP адрес")]
    public string? IpAddress { get; set; }

    [DisplayName("user-agent")]
    public string? UserAgent { get; set; }
}
