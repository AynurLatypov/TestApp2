namespace TestApp2.Helper;

public static class GuidHelper
{
    public static string ToShortString(Guid guid)
    {
        var base64Guid = Convert.ToBase64String(guid.ToByteArray());

        // Replace URL unfriendly characters
        base64Guid = base64Guid.Replace('+', '-').Replace('/', '_');

        // Remove the trailing ==
        return base64Guid[..^2];
    }

    public static Guid FromShortString(string str)
    {
        str = str.Replace('_', '/').Replace('-', '+');
        var byteArray = Convert.FromBase64String(str + "==");
        return new Guid(byteArray);
    }
}
