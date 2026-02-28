using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanTasksController : ControllerBase
    {
        // In-memory storage for now (replace with database later)
        private static List<KanbanTask> _tasks = new();

        [HttpGet]
        public ActionResult<IEnumerable<KanbanTask>> GetAllTasks()
        {
            return Ok(_tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<KanbanTask> GetTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<KanbanTask> CreateKanbanTask(KanbanTask task)
        {
            task.Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, [FromBody] KanbanTask task)
        {
            var foundTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (foundTask == null)
            {
                return NotFound();
            }

            foundTask.Title = task.Title;
            foundTask.Status = task.Status;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var toDelete = _tasks.FirstOrDefault(t => t.Id == id);
            if (toDelete == null)
            {
                return NotFound();
            }

            _tasks.Remove(toDelete);
            return NoContent();
        }
    }
}
