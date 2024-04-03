using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftAPI.Models;

public partial class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public decimal? Electricity { get; set; }

    public decimal? Rent { get; set; }

    public decimal? Tools { get; set; }

    public decimal? Salaries { get; set; }

    public decimal? Others { get; set; }

    public DateOnly? Month { get; set; }

    public int? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
