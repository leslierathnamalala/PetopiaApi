using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetopiaApi.Models;

namespace PetopiaApi.Context
{
    public class PetopiaContext : IdentityDbContext
    {
        public PetopiaContext(DbContextOptions<PetopiaContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Pets> Pets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<PetShopItem> PetShopItems { get; set; }
    }
}
