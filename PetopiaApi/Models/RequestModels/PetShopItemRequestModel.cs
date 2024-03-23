namespace PetopiaApi.Models
{
    public class PetShopItemRequestModel
    {
        public required string Heading { get; set; }
        public required string Description { get; set; }
        public required string ItemType { get; set; }
        public required string Price { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }

    }
}
