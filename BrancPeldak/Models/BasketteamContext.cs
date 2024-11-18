using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrancPeldak.Models;

public partial class BasketteamContext : DbContext
{
    public BasketteamContext()
    {
    }

    public BasketteamContext(DbContextOptions<BasketteamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Matchdatum> Matchdata { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;database=basketteam;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Matchdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("matchdata");

            entity.HasIndex(e => e.PlayerId, "PlayerId");

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Fault).HasColumnType("int(11)");
            entity.Property(e => e.Goal).HasColumnType("int(11)");
            entity.Property(e => e.PlayerId).HasMaxLength(36);
            entity.Property(e => e.SubbedIn)
                .HasColumnType("datetime")
                .HasColumnName("Subbed_In");
            entity.Property(e => e.SubbedOut)
                .HasColumnType("datetime")
                .HasColumnName("Subbed_Out");
            entity.Property(e => e.Try).HasColumnType("int(11)");
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany(p => p.Matchdata)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("matchdata_ibfk_1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.Property(e => e.Id).HasMaxLength(36);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Height).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(37);
            entity.Property(e => e.Weight).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
