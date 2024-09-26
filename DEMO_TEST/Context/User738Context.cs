using System;
using System.Collections.Generic;
using DEMO_TEST.Models;
using Microsoft.EntityFrameworkCore;

namespace DEMO_TEST.Context;

public partial class User738Context : DbContext
{
    public User738Context()
    {
    }

    public User738Context(DbContextOptions<User738Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Punktvidasci> Punktvidascis { get; set; }

    public virtual DbSet<SpisokTovar> SpisokTovars { get; set; }

    public virtual DbSet<Zakaz> Zakazs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.2.159;Port=5432;Database=user738;Username=user738;password=14053");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pk");

            entity.ToTable("order", "DemoTest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdTovar).HasColumnName("id_tovar");
            entity.Property(e => e.IdZakaz).HasColumnName("id_zakaz");

            entity.HasOne(d => d.IdTovarNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdTovar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_spisoktovar_fk");

            entity.HasOne(d => d.IdZakazNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdZakaz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_zakaz_fk");
        });

        modelBuilder.Entity<Punktvidasci>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("punktvidasci_pk");

            entity.ToTable("punktvidasci", "DemoTest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<SpisokTovar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("newtable_pk");

            entity.ToTable("spisokTovar", "DemoTest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Manufacturer)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Opisanie)
                .HasColumnType("character varying")
                .HasColumnName("opisanie");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.Prise).HasColumnName("prise");
        });

        modelBuilder.Entity<Zakaz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("zakaz_pk");

            entity.ToTable("zakaz", "DemoTest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Datez).HasColumnName("datez");
            entity.Property(e => e.IdPunktvidachi).HasColumnName("id_punktvidachi");
            entity.Property(e => e.Kod).HasColumnName("kod");
            entity.Property(e => e.Numberzakaz).HasColumnName("numberzakaz");
            entity.Property(e => e.Punktvidachi)
                .HasColumnType("character varying")
                .HasColumnName("punktvidachi");
            entity.Property(e => e.Sostavzakaz)
                .HasColumnType("character varying")
                .HasColumnName("sostavzakaz");
            entity.Property(e => e.Summaskidka).HasColumnName("summaskidka");
            entity.Property(e => e.Summazakaz).HasColumnName("summazakaz");

            entity.HasOne(d => d.IdPunktvidachiNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdPunktvidachi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zakaz_punktvidasci_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
