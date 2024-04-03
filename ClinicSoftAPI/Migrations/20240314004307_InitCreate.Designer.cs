﻿// <auto-generated />
using System;
using ClinicSoftAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicSoftAPI.Migrations
{
    [DbContext(typeof(BookingClinicsContext))]
    [Migration("20240314004307_InitCreate")]
    partial class InitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClinicSoftAPI.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("Day")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("date");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("Doctor_Id");

                    b.Property<TimeOnly?>("FromTime")
                        .HasColumnType("time")
                        .HasColumnName("From_Time");

                    b.Property<DateOnly?>("Month")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("ToTime")
                        .HasColumnType("time")
                        .HasColumnName("To_Time");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Availability", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    b.HasKey("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("Doctor_Id");

                    b.Property<decimal?>("Electricity")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateOnly?>("Month")
                        .HasColumnType("date");

                    b.Property<decimal?>("Others")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("Rent")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("Salaries")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("Tools")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int")
                        .HasColumnName("Patient_Id");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Operation", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<string>("BloodType")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Blood_Type");

                    b.Property<string>("Condition")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Fname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("FName");

                    b.Property<string>("Lname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("LName");

                    b.Property<string>("Medications")
                        .HasColumnType("text");

                    b.Property<int?>("NationalId")
                        .HasColumnType("int")
                        .HasColumnName("National_Id");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.HasKey("PatientId");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Receptionist", b =>
                {
                    b.Property<int>("RecepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecepId"));

                    b.Property<TimeOnly?>("EndShiftTime")
                        .HasColumnType("time")
                        .HasColumnName("End_Shift_Time");

                    b.Property<TimeOnly?>("StartShiftTime")
                        .HasColumnType("time")
                        .HasColumnName("Start_Shift_Time");

                    b.Property<DateOnly?>("StartWorkingDate")
                        .HasColumnType("date")
                        .HasColumnName("Start_Working_Date");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    b.HasKey("RecepId");

                    b.HasIndex("UserId");

                    b.ToTable("Receptionist", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateOnly?>("Day")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("FromTime")
                        .HasColumnType("time");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int")
                        .HasColumnName("Patient_Id");

                    b.Property<int?>("ReservationNo")
                        .HasColumnType("int")
                        .HasColumnName("Reservation_No");

                    b.Property<TimeOnly?>("ToTime")
                        .HasColumnType("time");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Reservation", (string)null);
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Fname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("FName");

                    b.Property<string>("Lname")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("LName");

                    b.Property<long?>("NationalId")
                        .HasColumnType("bigint")
                        .HasColumnName("National_Id");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Availability", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.Doctor", "Doctor")
                        .WithMany("Availabilities")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Availabil__Docto__5070F446");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Doctor", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.User", "User")
                        .WithMany("Doctors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Doctor__User_Id__4AB81AF0");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Expense", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.Doctor", "Doctor")
                        .WithMany("Expenses")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Expenses__Doctor__4D94879B");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Operation", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.Patient", "Patient")
                        .WithMany("Operations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Operation__Patie__59063A47");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Receptionist", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.User", "User")
                        .WithMany("Receptionists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Reception__User___47DBAE45");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Reservation", b =>
                {
                    b.HasOne("ClinicSoftAPI.Models.Patient", "Patient")
                        .WithMany("Reservations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Reservati__Patie__5535A963");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Doctor", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.Patient", b =>
                {
                    b.Navigation("Operations");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("ClinicSoftAPI.Models.User", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Receptionists");
                });
#pragma warning restore 612, 618
        }
    }
}
