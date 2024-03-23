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
    public class PetShopItemsController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public PetShopItemsController(PetopiaContext context)
        {
            _context = context;
        }

        // GET: api/PetShopItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetShopItem>>> GetPetShopItems()
        {
            return await _context.PetShopItems.ToListAsync();
        }

        [AllowAnonymous]
        [HttpPost("GetAllItems")]
        public async Task<ActionResult<IEnumerable<PetShopItem>>> GetAllItems( int page, int pageSize, PetShopItemRequestFilterModel filter)
        {
            try
            {
                int skip = page * pageSize;

                int totalCount = await _context.PetShopItems
                    .CountAsync(x => !x.IsDeleted && x.ItemType == filter.ItemType);

                var result = await _context.PetShopItems
                    .Where(x => !x.IsDeleted && x.ItemType == filter.ItemType)
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

        // GET: api/PetShopItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetShopItem>> GetPetShopItem(long id)
        {
            var petShopItem = await _context.PetShopItems.FindAsync(id);

            if (petShopItem == null)
            {
                return NotFound();
            }

            return petShopItem;
        }

        // PUT: api/PetShopItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetShopItem(long id, PetShopItem petShopItem)
        {
            if (id != petShopItem.PetShopItemsId)
            {
                return BadRequest();
            }

            _context.Entry(petShopItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetShopItemExists(id))
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

        // POST: api/PetShopItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetShopItem>> PostPetShopItem(PetShopItemRequestModel petShopItemRequest)
        {
            PetShopItem petShopItem = new PetShopItem()
            {
                Heading = petShopItemRequest.Heading,
                Description = petShopItemRequest.Description,
                Price = petShopItemRequest.Price,
                ItemType = petShopItemRequest.ItemType,
                ImageUrl1 = petShopItemRequest.ImageUrl1,
                ImageUrl2 = petShopItemRequest.ImageUrl2,
                ImageUrl3 = petShopItemRequest.ImageUrl3,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };


            _context.PetShopItems.Add(petShopItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPetShopItem", new { id = petShopItem.PetShopItemsId }, petShopItem);
        }

        // DELETE: api/PetShopItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetShopItem(long id)
        {
            var petShopItem = await _context.PetShopItems.FindAsync(id);
            if (petShopItem == null)
            {
                return NotFound();
            }

            _context.PetShopItems.Remove(petShopItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetShopItemExists(long id)
        {
            return _context.PetShopItems.Any(e => e.PetShopItemsId == id);
        }
    }
}
