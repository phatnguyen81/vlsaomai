using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;

namespace Toi.Plugin.Misc.Branches.Models
{
    public class BranchGroupNavigationModel : BaseNopModel, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public BranchGroupNavigationModel()
        {
            BranchGroups = new List<BranchGroupModel>();
        }

        public int CurrentBranchGroupId { get; set; }
        public List<BranchGroupModel> BranchGroups { get; set; }
        public class BranchGroupModel : BaseNopEntityModel
        {
            public BranchGroupModel()
            {
                SubBranchGroups = new List<BranchGroupModel>();
            }

            public string Name { get; set; }

            public string SeName { get; set; }

            public List<BranchGroupModel> SubBranchGroups { get; set; }
        }
    }
}
