namespace TaskOrganizer;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public bool IsFinish { get; set; }

    public override string ToString() => $"Id: {Id} Title: {Title}, Description: {Description}, TaskPriority: {TaskPriority}, Is finished: {IsFinish}"; 
}