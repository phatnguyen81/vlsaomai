using System.Data.Entity.ModelConfiguration;
using Toi.Plugin.Misc.Events.Domain;

namespace Toi.Plugin.Misc.Events.Data
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            this.ToTable("Event");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Title).IsRequired();
            this.Property(bp => bp.Full);
            
        }
    }
}
