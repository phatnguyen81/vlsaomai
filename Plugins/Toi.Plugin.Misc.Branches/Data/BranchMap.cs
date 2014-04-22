using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Branches.Domain;

namespace Toi.Plugin.Misc.Branches.Data
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            this.ToTable("Branch");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Short).IsRequired();
            this.Property(bp => bp.Full).IsRequired();
            this.Property(bp => bp.MetaKeywords).HasMaxLength(400);
            this.Property(bp => bp.MetaTitle).HasMaxLength(400);
            this.Property(bp => bp.Latitude).HasPrecision(18,5);
            this.Property(bp => bp.Longitude).HasPrecision(18, 5);

        }
    }
}
