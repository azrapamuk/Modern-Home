using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModernHome.Models;

namespace ModernHome.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Artikal> Artikal { get; set; }
        public DbSet<Dimenzije> Dimenzije { get; set; }
        public DbSet<Kartica> Kartica { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }
        public DbSet<StavkaNarudzbe> StavkaNarudzbe { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artikal>().ToTable("Artikal");
            modelBuilder.Entity<Dimenzije>().ToTable("Dimenzije");
            modelBuilder.Entity<Kartica>().ToTable("Kartica");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Korpa>().ToTable("Korpa");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Ocjena>().ToTable("Ocjena");
            modelBuilder.Entity<StavkaNarudzbe>().ToTable("StavkaNarudzbe");
            base.OnModelCreating(modelBuilder);
        }
    }
}
