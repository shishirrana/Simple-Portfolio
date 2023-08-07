using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace com.portfolio.website.Utilities
{
    public static class MySession
    {


        public static string GetValue(this HttpContext context, string key)
        {
            return context.Session.GetString("key");

        }
        public static void SetValue(this HttpContext context, string key, string value)
        {
            context.Session.SetString("key", value);

        }
    }
}



