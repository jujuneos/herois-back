using Microsoft.EntityFrameworkCore;
using SuperHerois.Models;

namespace SuperHerois.Context
{
    public class SuperHeroisDbContext : DbContext
    {
        public SuperHeroisDbContext(DbContextOptions<SuperHeroisDbContext> context) : base(context)
        { }

        public DbSet<Heroi>? Herois { get; set; }
        public DbSet<SuperPoder>? Superpoderes { get; set; }
        public DbSet<HeroisSuperPoderes>? HeroisSuperPoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroisSuperPoderes>()
                .HasKey(pk => new { pk.HeroiId, pk.SuperPoderId });

            base.OnModelCreating(modelBuilder);
        }
    }
}