using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicSoftAPI.DTOs.Reservation
{
    public class AddReservationMultipleDurationsDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Type { get; set; }

        public DateOnly? Day { get; set; }

        public TimeOnly DrFromTime { get; set; }

        public TimeOnly DrToTime { get; set; }

        public decimal? Cost { get; set; }

        public int? ReservationNo { get; set; }

        public int? PatientId { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
