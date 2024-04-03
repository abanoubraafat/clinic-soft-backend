using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ClinicSoftAPI.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public string? Type { get; set; }

    public long? NationalId { get; set; }

    public virtual ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Receptionist>? Receptionists { get; set; } = new List<Receptionist>();
}
