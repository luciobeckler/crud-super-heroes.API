using crud_super_heroes.API.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)  // Passa as opções para o construtor base (DbContext)
    {
    }
    public DbSet<Heroi> Herois { get; set; }
    public DbSet<SuperPoderes> SuperPoderes { get; set; }
    public DbSet<HeroiSuperPoder> HeroisSuperPoderes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HeroiSuperPoder>()
            .HasKey(hs => new { hs.HeroiId, hs.SuperPoderId });

        modelBuilder.Entity<HeroiSuperPoder>()
            .HasOne(hs => hs.Heroi)
            .WithMany(h => h.HeroisSuperPoderes)
            .HasForeignKey(hs => hs.HeroiId);

        modelBuilder.Entity<HeroiSuperPoder>()
            .HasOne(hs => hs.SuperPoder)
            .WithMany(s => s.HeroisSuperPoderes)
            .HasForeignKey(hs => hs.SuperPoderId);
    }
}
