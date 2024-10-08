﻿// <auto-generated />
using System;
using Gestion.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gestion.Server.Migrations
{
    [DbContext(typeof(GestionContext))]
    [Migration("20240913031306_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestion.Server.Data.GestionModels.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignmentId"));

                    b.Property<string>("AssignmentStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("GoalId")
                        .HasColumnType("int");

                    b.Property<bool>("IsImportant")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Assignment");

                    b.HasKey("AssignmentId")
                        .HasName("PK__Assignme__32499E77B8AF2C1B");

                    b.HasIndex("GoalId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Gestion.Server.Data.GestionModels.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalId"));

                    b.Property<int>("CompletedTasks")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("TotalTasks")
                        .HasColumnType("int");

                    b.HasKey("GoalId")
                        .HasName("PK__Goals__8A4FFFD1913D513C");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Gestion.Server.Data.GestionModels.Assignment", b =>
                {
                    b.HasOne("Gestion.Server.Data.GestionModels.Goal", "Goal")
                        .WithMany("Assignments")
                        .HasForeignKey("GoalId")
                        .HasConstraintName("FK__Assignmen__GoalI__45F365D3");

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("Gestion.Server.Data.GestionModels.Goal", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
