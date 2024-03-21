﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaApi.Context;
using PetopiaApi.Models;

namespace PetopiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public PetsController(PetopiaContext context)
        {
            _context = context;
        }

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pets>>> GetPets()
        {
            return await _context.Pets.ToListAsync();
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pets>> GetPets(long id)
        {
            var pets = await _context.Pets.FindAsync(id);

            if (pets == null)
            {
                return NotFound();
            }

            return pets;
        }

        // PUT: api/Pets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPets(long id, Pets pets)
        {
            if (id != pets.PetId)
            {
                return BadRequest();
            }

            _context.Entry(pets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetsExists(id))
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

        // POST: api/Pets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pets>> PostPets(Pets pets)
        {
            _context.Pets.Add(pets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPets", new { id = pets.PetId }, pets);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePets(long id)
        {
            var pets = await _context.Pets.FindAsync(id);
            if (pets == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetsExists(long id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}
