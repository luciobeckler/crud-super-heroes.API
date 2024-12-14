using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Heroi> Herois { get; set; }
        public DbSet<SuperPoderes> SuperPoderes { get; set; }
        public DbSet<HeroiSuperPoder> HeroisSuperPoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da tabela HeroiSuperPoder (chave composta)
            modelBuilder.Entity<HeroiSuperPoder>()
                .HasKey(hsp => new { hsp.HeroiId, hsp.SuperPoderId });

            modelBuilder.Entity<HeroiSuperPoder>()
                .HasOne<Heroi>()
                .WithMany()
                .HasForeignKey(hsp => hsp.HeroiId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HeroiSuperPoder>()
                .HasOne<SuperPoderes>()
                .WithMany()
                .HasForeignKey(hsp => hsp.SuperPoderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração opcional: Índice único no NomeHeroi
            modelBuilder.Entity<Heroi>()
                .HasIndex(h => h.NomeHeroi)
                .IsUnique();
        }
    }
}
