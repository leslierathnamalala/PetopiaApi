namespace PetopiaApi.Models
{
    public class ApointmentRequestModel
    {
        public long ClinicId { get; set; }
        public required string Contact { get; set; }
        public required string Name { get; set; }
        public required string Date { get; set; }
        public required string? From { get; set; }
        public required string? To { get; set; }

    }
}
