using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tododotnet6prac.Models;

public partial class TodoContext : DbContext
{
    public TodoContext()
    {
    }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Todo> Todos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Division>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Division");

            entity.Property(e => e.Test)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("test");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employee");

            entity.Property(e => e.T)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("JobTitle");

            entity.Property(e => e.JobTitle1)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Owner");

            entity.Property(e => e.O)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Todo");

            entity.Property(e => e.Tod)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
