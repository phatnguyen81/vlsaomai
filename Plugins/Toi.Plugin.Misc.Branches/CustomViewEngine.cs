using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.Branches
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.Branches/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.Branches/Views/{1}/{0}.cshtml" };
        }
    }
}
