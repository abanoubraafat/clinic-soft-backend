using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftAPI.Models;

public partial class Receptionist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecepId { get; set; }

    public TimeOnly? StartShiftTime { get; set; }

    public TimeOnly? EndShiftTime { get; set; }

    public DateOnly? StartWorkingDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
