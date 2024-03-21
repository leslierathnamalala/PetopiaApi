namespace PetopiaApi.Models
{
    public class AdvertisementRequestModel
    {
        public long UserId { get; set; }
        public required string Heading { get; set; }
        public required string Description { get; set; }
        public required string PetType { get; set; }
        public required string Breed { get; set; }
        public required string Contact { get; set; }
        public required string Price { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public required bool IsPro { get; set; }
  
    }
}
