using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Toi.Plugin.Misc.Branches
{
    public class BranchSettings : ISettings
    {
        public int BranchThumbPictureSize { get; set; }

        public int DefaultBranchGroupId { get; set; }
    }
}
