using Nop.Web.Framework.Themes;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.DoItYourself/Views/{1}/{0}.cshtml" };
            ViewLocationFormats = new[] { "~/Plugins/Misc.DoItYourself/Views/{1}/{0}.cshtml" };
        }
    }
}
