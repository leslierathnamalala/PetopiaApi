using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        private readonly PetopiaContext _context;

        public UsersController(PetopiaContext context)
        {
            _context = context;
        }

        [HttpPost("GetUserByEmail")]
        public async Task<ActionResult<Users>> GetTestById(GetUserModel model)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(x => x.Username == model.Email);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }
    }
}
