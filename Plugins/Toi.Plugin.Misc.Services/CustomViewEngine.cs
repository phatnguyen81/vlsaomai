using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.Services
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.Services/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.Services/Views/{1}/{0}.cshtml" };
        }
    }
}
