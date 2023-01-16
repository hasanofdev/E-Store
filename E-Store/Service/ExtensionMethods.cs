using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Service;

internal static class ExtensionMethods
{
    public static string ToStringForSecureStr(this SecureString @this) => new NetworkCredential("", @this).Password;
}
