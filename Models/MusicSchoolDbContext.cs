using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Tecnica_Italika.Models;

public partial class MusicSchoolDbContext : DbContext
{
    public MusicSchoolDbContext()
    {
    }

    public MusicSchoolDbContext(DbContextOptions<MusicSchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MusicSchoolDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.SchoolId, "IX_Students_SchoolId");

            entity.HasOne(d => d.School).WithMany(p => p.Students).HasForeignKey(d => d.SchoolId);

            entity.HasMany(d => d.Teachers).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentTeacher",
                    r => r.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("StudentId", "TeacherId");
                        j.ToTable("StudentTeachers");
                        j.HasIndex(new[] { "TeacherId" }, "IX_StudentTeachers_TeacherId");
                    });
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasIndex(e => e.SchoolId, "IX_Teachers_SchoolId");

            entity.HasOne(d => d.School).WithMany(p => p.Teachers).HasForeignKey(d => d.SchoolId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
