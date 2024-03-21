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
using PetopiaApi.Models.Utils;

namespace PetopiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdvertisementsController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public AdvertisementsController(PetopiaContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("GetAllAds")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdsByUserId(int page, int pageSize)
        {
            try
            {
                int skip = page * pageSize;

                int totalCount = await _context.Advertisements
                    .CountAsync(x => !x.IsDeleted);

                var result = await _context.Advertisements
                    .Where(x => !x.IsDeleted)
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

        [HttpGet("GetAdsByUserId")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdsByUserId(long userId, int page, int pageSize)
        {
            try
            {
                int skip = page * pageSize;

                int totalCount = await _context.Advertisements
                    .CountAsync(x => x.UserId == userId && !x.IsDeleted);

                var result = await _context.Advertisements
                    .Where(x => x.UserId == userId && !x.IsDeleted)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(long id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);

            if (advertisement == null)
            {
                return NotFound();
            }
            else
            {
                if(advertisement.Reactions != null)
                {
                    advertisement.Reactions = advertisement.Reactions + 1;
                }
                else
                {
                    advertisement.Reactions = 1;
                }

                _context.Entry(advertisement).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return advertisement;
        }

        [HttpPost]
        public async Task<ActionResult<Advertisement>> PostAdvertisement(AdvertisementRequestModel advertisementRequest)
        {
            try
            {
                Advertisement advertisement = new Advertisement()
                {
                    UserId = advertisementRequest.UserId,
                    Heading = advertisementRequest.Heading,
                    Description = advertisementRequest.Description,
                    PetType = advertisementRequest.PetType,
                    Breed = advertisementRequest.Breed,
                    Contact = advertisementRequest.Contact,
                    Price = advertisementRequest.Price,
                    IsPro = advertisementRequest.IsPro,
                    ImageUrl1 = advertisementRequest.ImageUrl1,
                    ImageUrl2 = advertisementRequest.ImageUrl2,
                    ImageUrl3 = advertisementRequest.ImageUrl3,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                };

                _context.Advertisements.Add(advertisement);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAdvertisement", new { id = advertisement.AdvertisementId }, advertisement);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool AdvertisementExists(long id)
        {
            return _context.Advertisements.Any(e => e.AdvertisementId == id);
        }
    }
}
