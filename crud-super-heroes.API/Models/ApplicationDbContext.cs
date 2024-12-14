using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Heroi> Herois { get; set; }
        public DbSet<SuperPoder> SuperPoderes { get; set; }
        public DbSet<HeroiSuperPoder> HeroisSuperPoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para HeroiSuperPoder
            modelBuilder.Entity<HeroiSuperPoder>()
                .HasKey(hsp => new { hsp.HeroiId, hsp.SuperPoderId });

            modelBuilder.Entity<HeroiSuperPoder>()
                .HasOne(hsp => hsp.Heroi)
                .WithMany(h => h.HeroisSuperPoderes)
                .HasForeignKey(hsp => hsp.HeroiId);

            modelBuilder.Entity<HeroiSuperPoder>()
                .HasOne(hsp => hsp.SuperPoder)
                .WithMany(sp => sp.HeroisSuperPoderes)
                .HasForeignKey(hsp => hsp.SuperPoderId);

            // Garantir unicidade do nome do herói
            modelBuilder.Entity<Heroi>()
                .HasIndex(h => h.Nome)
                .IsUnique();

            // Seed data opcional para SuperPoderes
            modelBuilder.Entity<SuperPoder>().HasData(
                new SuperPoder { Id = 1, Nome = "Força", Descricao = "Super força" },
                new SuperPoder { Id = 2, Nome = "Velocidade", Descricao = "Super velocidade" }
            );
        }
    }
}
