using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaApi.Context;
using PetopiaApi.Models;

namespace PetopiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicsController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public ClinicsController(PetopiaContext context)
        {
            _context = context;
        }

        // GET: api/Clinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> GetClinic(long id)
        {
            var clinic = await _context.Clinics.FindAsync(id);

            if (clinic == null)
            {
                return NotFound();
            }

            return clinic;
        }

        [AllowAnonymous]
        [HttpGet("GetAllClinics")]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetAllClinics(int page, int pageSize)
        {
            try
            {
                int skip = page * pageSize;



                var query = from clinic in _context.Clinics
                            join user in _context.Users on clinic.UserId equals user.UserId
                            where !clinic.IsDeleted
                            select new
                            {
                                Clinic = clinic,
                                UserName = user.FirstName + " " + user.LastName,
                                UserImage = user.UserImage,
                            };


                int totalCount = await query.CountAsync();

                var result = await query
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Payload = result,
                    TotalItems = totalCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetClinicByUserId")]
        public async Task<ActionResult<Users>> GetTestById(long userId)
        {
            try
            {
                var result = await _context.Clinics.FirstOrDefaultAsync(x => x.UserId == userId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Clinics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinic(long id, Clinic clinic)
        {
            if (id != clinic.ClinicId)
            {
                return BadRequest();
            }

            _context.Entry(clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clinics
        [HttpPost]
        public async Task<ActionResult<Clinic>> PostClinic(ClinicRequestModel clinicRequest)
        {

            Clinic clinic = new Clinic()
            {
                UserId = clinicRequest.UserId,
                ClinicName = clinicRequest.ClinicName,
                ImageUrl = clinicRequest.ImageUrl,
                Address1 = clinicRequest.Address1,
                Address2 = clinicRequest.Address2,
                Address3 = clinicRequest.Address3,
                Type = clinicRequest.Type,
                Services = clinicRequest.Services,
                Contact = clinicRequest.Contact,
                Email = clinicRequest.Email,
                Website = clinicRequest.Website,
                From = clinicRequest.From,
                To = clinicRequest.To,
                IsTwentyFourSeven = clinicRequest.IsTwentyFourSeven,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
            };

            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinic", new { id = clinic.ClinicId }, clinic);
        }

        // DELETE: api/Clinics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(long id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            clinic.IsDeleted = true;
            _context.Entry(clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ClinicExists(long id)
        {
            return _context.Clinics.Any(e => e.ClinicId == id);
        }
    }
}
