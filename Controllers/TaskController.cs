using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanTasksController : ControllerBase
    {
        private readonly TaskContext _context;
        public KanbanTasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<KanbanTask>> GetAllTasks()
        {
            return Ok(_context.Tasks.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<KanbanTask> GetTask(int id)

        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<KanbanTask> CreateKanbanTask(KanbanTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, [FromBody] KanbanTask task)
        {
            var foundTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (foundTask == null)
            {
                return NotFound();
            }

            foundTask.Title = task.Title;
            foundTask.Status = task.Status;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var toDelete = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (toDelete == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(toDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
