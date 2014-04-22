using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.Services.Domain;

namespace Toi.Plugin.Misc.Services.Data
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            this.ToTable("Service");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Full);
            
        }
    }
}
