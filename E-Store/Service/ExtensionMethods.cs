using System.Net;
using System.Security;

namespace E_Store.Service;

internal static class ExtensionMethods
{
    public static string ToStringForSecureStr(this SecureString @this) => new NetworkCredential("", @this).Password;
}
