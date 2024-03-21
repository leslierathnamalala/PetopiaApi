using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetopiaApi.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookingId { get; set; }
        public string UserId { get; set; }
        public DateTime? BookedDate { get; set; }
        public string VehicleId { get; set; }
        public TimeSpan? BookedTime { get; set; }
    }
}
