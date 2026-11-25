using App.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

public class CendresContext : DbContext {
    public DbSet<Zona> Zonas => Set<Zona>();
    public DbSet<Foguera> Fogueras => Set<Foguera>();

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        options.UseLazyLoadingProxies().UseSqlite("Data Source=cendres.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Zona>()
            .Property(z => z.Bioma)
            .HasConversion<string>();

        modelBuilder.Entity<Zona>()
            .HasOne(z => z.Foguera)
            .WithOne(f => f.Zona)
            .HasForeignKey<Zona>("FogueraId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Zona>()
            .HasOne(z => z.Regio)
            .WithMany(z => z.Subzones)
            .HasForeignKey("CandidaturaPrincipalId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}