using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI.Models;

public partial class BookingClinicsContext : DbContext
{
    public BookingClinicsContext()
    {
    }

    public BookingClinicsContext(DbContextOptions<BookingClinicsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Availability>(entity =>
        {
            entity.ToTable("Availability");

            entity.Property(e => e.Day)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");
            entity.Property(e => e.FromTime).HasColumnName("From_Time");
            entity.Property(e => e.ToTime).HasColumnName("To_Time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Availabilities)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Availabil__Docto__5070F446");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {

            entity.ToTable("Doctor");

            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Doctor__User_Id__4AB81AF0");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_Id");
            entity.Property(e => e.Electricity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Others).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Rent).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Salaries).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tools).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Expenses__Doctor__4D94879B");
        });

        modelBuilder.Entity<Operation>(entity =>
        {

            entity.ToTable("Operation");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Operations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Operation__Patie__59063A47");
        });

        modelBuilder.Entity<Patient>(entity =>
        {

            entity.ToTable("Patient");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Blood_Type");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LName");
            entity.Property(e => e.Medications).HasColumnType("text");
            entity.Property(e => e.NationalId).HasColumnName("National_Id");
            entity.Property(e => e.Notes).HasColumnType("text");
            modelBuilder.Entity<Patient>()
               .HasIndex(p => p.NationalId)
               .IsUnique();
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.ToTable("Receptionist");

            entity.Property(e => e.EndShiftTime).HasColumnName("End_Shift_Time");
            entity.Property(e => e.StartShiftTime).HasColumnName("Start_Shift_Time");
            entity.Property(e => e.StartWorkingDate).HasColumnName("Start_Working_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Receptionists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reception__User___47DBAE45");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {

            entity.ToTable("Reservation");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.ReservationNo).HasColumnName("Reservation_No");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reservati__Patie__5535A963");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LName");
            entity.Property(e => e.NationalId).HasColumnName("National_Id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
