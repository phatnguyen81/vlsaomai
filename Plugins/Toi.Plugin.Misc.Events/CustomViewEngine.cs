using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.Events
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.Events/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.Events/Views/{1}/{0}.cshtml" };
        }
    }
}
