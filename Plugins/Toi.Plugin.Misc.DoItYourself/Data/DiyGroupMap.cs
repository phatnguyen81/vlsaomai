using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.DoItYourself.Domain;

namespace Toi.Plugin.Misc.DoItYourself.Data
{
    public class DiyGroupMap : EntityTypeConfiguration<DiyGroup>
    {
        public DiyGroupMap()
        {
            this.ToTable("DiyGroup");
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
