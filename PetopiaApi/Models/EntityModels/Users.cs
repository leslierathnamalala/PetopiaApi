using PetopiaApi.Models.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetopiaApi.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public required string Username { get; set; }
        public required string Nic { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public int? ZipCode { get; set; }
        public UserType UserType { get; set; }
    }
}
