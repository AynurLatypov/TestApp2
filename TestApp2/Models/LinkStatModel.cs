using System.ComponentModel;
using TestApp2.Helper;

namespace TestApp2.Models;

public class LinkStatModel
{
    public int Id { get; set; }

    [DisplayName("Короткая ссылка")]
    public string ShortId => ShortLinkHelper.ToString(Id);

    [DisplayName("Ссылка")]
    public string Url { get; set; }

    [DisplayName("Количество переходов")]
    public int Count { get; set; }
}
