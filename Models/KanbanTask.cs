using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Models;

public class KanbanTask
{
    public int Id { get; set; }

    [Required(ErrorMessage = "TITLE_REQUIRED")]
    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    [Required]
    public KanbanStatus Status { get; set; } = KanbanStatus.Todo;

    public KanbanTask(int id, string title, string description, KanbanStatus status)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
    }

    public KanbanTask(KanbanTask task)
    {
        Id = task.Id;
        Title = task.Title;
        Description = task.Description;
        Status = task.Status;
    }
    public KanbanTask(string title, string description, KanbanStatus status)
    {
        Title = title;
        Description = description;
        Status = status;
    }

    public KanbanTask()
    {
    }
}
public enum KanbanStatus
{
    Todo,
    InProgress,
    Done
}
