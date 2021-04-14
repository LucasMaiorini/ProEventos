using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexts
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speecher> Speechers { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<SpeecherEvent> SpeecherEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sets N:N keys
            modelBuilder.Entity<SpeecherEvent>().HasKey(SpeecherEvent =>
                new { SpeecherEvent.EventId, SpeecherEvent.SpeecherId });

            //Sets cascade delete 
            modelBuilder.Entity<Event>().HasMany(e => e.SocialNetworks)
                .WithOne(sn => sn.Event)
                .OnDelete(DeleteBehavior.Cascade);

                //Sets cascade delete 
            modelBuilder.Entity<Speecher>().HasMany(s => s.SocialNetworks)
                .WithOne(sn => sn.Speecher)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
