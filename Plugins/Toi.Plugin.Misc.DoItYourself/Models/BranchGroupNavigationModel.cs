using System;
using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Toi.Plugin.Misc.DoItYourself.Models
{
    public class DiyGroupNavigationModel : BaseNopModel, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public DiyGroupNavigationModel()
        {
            DiyGroups = new List<DiyGroupModel>();
        }

        public int CurrentDiyGroupId { get; set; }
        public List<DiyGroupModel> DiyGroups { get; set; }
        public class DiyGroupModel : BaseNopEntityModel
        {
            public DiyGroupModel()
            {
                SubDiyGroups = new List<DiyGroupModel>();
            }

            public string Name { get; set; }

            public string SeName { get; set; }

            public List<DiyGroupModel> SubDiyGroups { get; set; }
        }
    }
}
