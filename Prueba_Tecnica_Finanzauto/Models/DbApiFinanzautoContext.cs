using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Tecnica_Finanzauto.Models;

public partial class DbApiFinanzautoContext : DbContext
{
    public DbApiFinanzautoContext()
    {
    }

    public DbApiFinanzautoContext(DbContextOptions<DbApiFinanzautoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //=> optionsBuilder.UseSqlServer("Server=Jonathan_Borda; Database=db_Api_Finanzauto; Integrated security=true; Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC07A7A6C253");

            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser).HasMaxLength(100);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationUser).HasMaxLength(100);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__Modific__286302EC");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grades__3214EC072A56BC12");

            entity.Property(e => e.Grade1).HasColumnName("Grade");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__CourseId__2C3393D0");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__StudentI__2D27B809");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0795047068");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationUser).HasMaxLength(100);
            entity.Property(e => e.Names).HasMaxLength(100);
            entity.Property(e => e.Surnames).HasMaxLength(100);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teachers__3214EC07CF805D66");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUser).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.ModificationUser).HasMaxLength(100);
            entity.Property(e => e.Names).HasMaxLength(100);
            entity.Property(e => e.Surnames).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
