using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.Articles
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.Articles/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.Articles/Views/{1}/{0}.cshtml" };
        }
    }
}
