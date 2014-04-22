using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.NewsExs.Domain;

namespace Toi.Plugin.Misc.NewsExs.Data
{
    public class NewsExMap : EntityTypeConfiguration<NewsEx>
    {
        public NewsExMap()
        {
            this.ToTable("NewsEx");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Full);
            
        }
    }
}
