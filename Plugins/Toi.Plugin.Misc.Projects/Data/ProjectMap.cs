using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.Projects.Domain;

namespace Toi.Plugin.Misc.Projects.Data
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            this.ToTable("Project");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Full);
            
        }
    }
}
