using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaApi.Context;
using PetopiaApi.Models;

namespace PetopiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public AppointmentsController(PetopiaContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(long id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(ApointmentRequestModel appointmentRequest)
        {
            Appointment appointment = new Appointment()
            {
                ClinicId = appointmentRequest.ClinicId,
                Contact = appointmentRequest.Contact,
                Name = appointmentRequest.Name,
                Date = appointmentRequest.Date,
                From = appointmentRequest.From,
                To = appointmentRequest.To,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentId }, appointment);
        }

        [AllowAnonymous]
        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments(int userId)
        {
            try
            {
                
                var query = from appointment in _context.Appointments
                            join clinic in _context.Clinics on appointment.ClinicId equals clinic.ClinicId
                            join user in _context.Users on clinic.UserId equals user.UserId
                            where !appointment.IsDeleted && user.UserId == userId
                            select new
                            {
                                appointment.Contact,
                                appointment.Name,
                                appointment.Date,
                            };

                var result = await query.ToListAsync();

                if (result == null || result.Count == 0)
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

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
