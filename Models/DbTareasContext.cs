using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project_task_netangular.Models;

public partial class DbTareasContext : DbContext
{
    public DbTareasContext()
    {
    }

    public DbTareasContext(DbContextOptions<DbTareasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Idtarea).HasName("tarea_pkey");

            entity.ToTable("tarea");

            entity.Property(e => e.Idtarea).HasColumnName("idtarea");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
