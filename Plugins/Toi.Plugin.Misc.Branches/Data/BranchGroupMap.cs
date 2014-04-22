using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Branches.Domain;

namespace Toi.Plugin.Misc.Branches.Data
{
    public class BranchGroupMap : EntityTypeConfiguration<BranchGroup>
    {
        public BranchGroupMap()
        {
            this.ToTable("BranchGroup");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Description).IsMaxLength();
            this.Property(c => c.MetaKeywords).HasMaxLength(400);
            this.Property(c => c.MetaDescription);
            this.Property(c => c.MetaTitle).HasMaxLength(400);
            this.Property(c => c.SeName).HasMaxLength(200);

        }
    }
}
