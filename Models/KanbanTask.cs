using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoard.Models;

public class KanbanTask
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "TITLE_REQUIRED")]
    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    [Required]
    public KanbanStatus Status { get; set; } = KanbanStatus.Todo;

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    [Required]
    public User User { get; set; } = null!;

    // JSON deserializer creates empty objects, then sets everything
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
