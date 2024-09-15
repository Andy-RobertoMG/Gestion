using System;
using System.Collections.Generic;
using Gestion.Server.Data.GestionModels;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Server.Data;

public partial class GestionContext : DbContext
{
    public GestionContext()
    {
    }

    public GestionContext(DbContextOptions<GestionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("GestionConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Goal>()
        .Property(g => g.Percentage)
        .HasPrecision(5, 2);
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E77B8AF2C1B");

            entity.Property(e => e.AssigmentName)
                .HasMaxLength(80)
                .HasColumnName("Name");
            entity.Property(e => e.AssignmentStatus).HasMaxLength(10);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Goal).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.GoalId)
                .HasConstraintName("FK__Assignmen__GoalI__45F365D3");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__Goals__8A4FFFD1913D513C");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GoalName).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
