using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Data
{
    public class ArticleGroupMap : EntityTypeConfiguration<ArticleGroup>
    {
        public ArticleGroupMap()
        {
            this.ToTable("ArticleGroup");
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
