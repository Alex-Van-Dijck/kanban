using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TaskContext _context;

        public UsersController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Include(u => u.Tasks).FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("by-username/{username}")]
        public ActionResult<User> GetByUsername(string username)
        {
            var user = _context.Users.Include(u => u.Tasks)
                .FirstOrDefault(u => u.Name == username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (foundUser == null) { return NotFound(); }

            foundUser.Name = user.Name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var foundUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (foundUser == null) return NotFound();

            _context.Users.Remove(foundUser);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
