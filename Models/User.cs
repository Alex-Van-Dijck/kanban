using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public ICollection<KanbanTask> Tasks { get; set; } = new List<KanbanTask>();

    public User()
    {
    }
}
