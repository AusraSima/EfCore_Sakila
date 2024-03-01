using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SakilaDbContext : DbContext
    {
        public SakilaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                 .HasMany(actor => actor.Films)
                 .WithMany(film => film.Actors)
                 .UsingEntity<Dictionary<string, object>>(
                "film_actor",
                j => j.HasOne<Film>().WithMany().HasForeignKey("film_id"),
                j => j.HasOne<Actor>().WithMany().HasForeignKey("actor_id"),
                j => j.HasKey("actor_id", "film_id")
                );
        }
    }
}
