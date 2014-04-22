using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.Projects
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.Projects/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.Projects/Views/{1}/{0}.cshtml" };
        }
    }
}
