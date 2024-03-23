using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetopiaApi.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AppointmentId { get; set; }
        public long ClinicId { get; set; }
        public required string Contact { get; set; }
        public required string Name { get; set; }
        public required string Date { get; set; }
        public required string? From { get; set; }
        public required string? To { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
