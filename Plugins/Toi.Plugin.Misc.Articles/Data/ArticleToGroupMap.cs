using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toi.Plugin.Misc.Articles.Domain;

namespace Toi.Plugin.Misc.Articles.Data
{
    public class ArticleToGroupMap : EntityTypeConfiguration<ArticleToGroup>
    {
        public ArticleToGroupMap()
        {
            this.ToTable("ArticleToGroup");
            this.HasKey(pc => pc.Id);

            this.HasRequired(pc => pc.ArticleGroup)
                .WithMany()
                .HasForeignKey(pc => pc.ArticleGroupId);


            this.HasRequired(pc => pc.Article)
                .WithMany(p => p.ArticleToGroups)
                .HasForeignKey(pc => pc.ArticleId);
        }
    }
}
