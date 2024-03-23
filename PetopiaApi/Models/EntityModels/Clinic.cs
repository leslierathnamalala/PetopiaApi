﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetopiaApi.Models
{
    public class Clinic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClinicId { get; set; }
        public long UserId { get; set; }
        public required string ClinicName { get; set; }
        public required string ImageUrl { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public required string Type { get; set; }
        public required string Services { get; set; }
        public required string Contact { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public bool IsTwentyFourSeven { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
