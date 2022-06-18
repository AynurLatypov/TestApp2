using System.ComponentModel.DataAnnotations;

namespace TestApp2.Models;

public class UrlRequest
{
    [Required]
    public Uri Url { get; set; }
}
