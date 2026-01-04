using System;
using System.Collections.Generic;
using Labb3_School.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_School.Data;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeDetail> GradeDetails { get; set; }

    public virtual DbSet<GradesLastMonth> GradesLastMonths { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = ERIK; Database = SchoolDB; Integrated Security = True; Trust Server Certificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC07286393DE");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Program).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC077E43B899");

            entity.Property(e => e.Course1)
                .HasMaxLength(100)
                .HasColumnName("Course");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3213E83FC5305C2E");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Department1)
                .HasMaxLength(100)
                .HasColumnName("Department");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId });

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Grade1)
                .HasMaxLength(3)
                .HasColumnName("Grade");
            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");

            entity.HasOne(d => d.StudentCourse).WithOne(p => p.Grade)
                .HasForeignKey<Grade>(d => new { d.StudentId, d.CourseId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_StudentCourse");
        });

        modelBuilder.Entity<GradeDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GradeDetails");

            entity.Property(e => e.AverageGrade)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Course).HasMaxLength(100);
            entity.Property(e => e.HighestGrade)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LowestGrade)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GradesLastMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GradesLastMonth");

            entity.Property(e => e.Course).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Grade).HasMaxLength(3);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC0718B85C67");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.HireDate).HasColumnName("Hire_Date");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("fk_staff_department");

            entity.HasMany(d => d.Classes).WithMany(p => p.Staff)
                .UsingEntity<Dictionary<string, object>>(
                    "StaffClass",
                    r => r.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StaffClass_Class"),
                    l => l.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StaffClass_Staff"),
                    j =>
                    {
                        j.HasKey("StaffId", "ClassId");
                        j.ToTable("StaffClass");
                        j.IndexerProperty<int>("StaffId").HasColumnName("Staff_Id");
                        j.IndexerProperty<int>("ClassId").HasColumnName("Class_Id");
                    });
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC078779C8A6");

            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClass");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId });

            entity.ToTable("StudentCourse");

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourse_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourse_Student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
