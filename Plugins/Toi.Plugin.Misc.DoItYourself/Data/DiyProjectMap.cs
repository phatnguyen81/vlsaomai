using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.DoItYourself.Domain;

namespace Toi.Plugin.Misc.DoItYourself.Data
{
    public class DiyProjectMap : EntityTypeConfiguration<DiyProject>
    {
        public DiyProjectMap()
        {
            this.ToTable("DiyProject");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Short);
            this.Property(bp => bp.Full);
            this.Property(bp => bp.MetaKeywords).HasMaxLength(400);
            this.Property(bp => bp.MetaTitle).HasMaxLength(400);

        }
    }
}
