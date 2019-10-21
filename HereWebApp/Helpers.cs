using System;

namespace HereWebApp
{
    public static class Helpers
    {
        public static string ToCamelCase(this string s1)
        {
            return char.ToLowerInvariant(s1[0]) + s1.Substring(1);
        }
    }
}
