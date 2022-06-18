using System.Text;

namespace TestApp2.Helper;

public static class ShortLinkHelper
{
    public static string ToString(int id)
    {
        var bytes = Encoding.UTF8.GetBytes($"{id:000000}");
        return Convert.ToBase64String(bytes);
    }

    public static int? ToInt32(string str)
    {
        try
        {
            var bytes = Convert.FromBase64String(str);
            var number = Encoding.UTF8.GetString(bytes);
            return Convert.ToInt32(number);
        }
        catch(FormatException)
        {
            return null;
        }
    }
}
