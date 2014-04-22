using Nop.Core.Configuration;

namespace Toi.Plugin.Misc.DoItYourself
{
    public class DiySettings : ISettings
    {
        public int DiyProjectThumbPictureSize { get; set; }

        public int DefaultDiyGroupId { get; set; }

        public int HotestProjectId { get; set; }
    }
}
