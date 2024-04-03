using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftAPI.Models;

public partial class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Type { get; set; }

    public DateOnly Day { get; set; }

    public TimeOnly? FromTime { get; set; }

    public TimeOnly? ToTime { get; set; }

    public decimal Cost { get; set; }

    public int? ReservationNo { get; set; }

    public int? PatientId { get; set; }

    public virtual Patient? Patient { get; set; }
}
