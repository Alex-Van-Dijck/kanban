namespace KanbanBoard.Models;

public class KanbanTask
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public KanbanStatus Status { get; set; } = KanbanStatus.Todo;

    public KanbanTask(int id, string title, KanbanStatus status)
    {
        Id = id;
        Title = title;
        Status = status;
    }

    public KanbanTask(KanbanTask task)
    {
        Id = task.Id;
        Title = task.Title;
        Status = task.Status;
    }
    //The api will set the Id's itself
    public KanbanTask(string title, KanbanStatus status)
    {
        Title = title;
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
