using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI.Models;

public partial class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PatientId { get; set; }

    public string? Condition { get; set; }

    public string? BloodType { get; set; }

    public string? Medications { get; set; }

    public string? Notes { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

   // [Index(IsUnique = true)]
    public long? NationalId { get; set; }

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
