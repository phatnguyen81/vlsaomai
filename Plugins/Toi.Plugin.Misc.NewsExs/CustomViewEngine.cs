using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.NewsExs
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.NewsExs/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.NewsExs/Views/{1}/{0}.cshtml" };
        }
    }
}
