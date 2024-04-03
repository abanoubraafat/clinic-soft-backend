using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftAPI.Models;

public partial class Doctor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DoctorId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual User? User { get; set; }
}
