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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1USG4GQ; DataBase=Gestion;TrustServerCertificate=true; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E77B8AF2C1B");

            entity.Property(e => e.Assignment1)
                .HasMaxLength(80)
                .HasColumnName("Assignment");
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
