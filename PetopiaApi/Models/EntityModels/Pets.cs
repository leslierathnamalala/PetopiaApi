using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetopiaApi.Models
{
    public class Pets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PetId { get; set; }
        public required long PetOwnerId { get; set; }
        public required string Name { get; set; }
        public DateTime DOB { get; set; }
       
    }
}
