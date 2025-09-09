using Spectre.Console;

namespace TaskOrganizer;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Descritpion { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public bool IsFinish = false;

    public void AddTask()
    {
        Title = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter title of your task: "));

        Descritpion = AnsiConsole.Prompt(
             new TextPrompt<string>("Enter descrtiption of your task"));

        TaskPriority = AnsiConsole.Prompt(
             new SelectionPrompt<TaskPriority>()
                .Title("Choose priority of your task")
                .PageSize(10)
                .MoreChoicesText("Move up and down to reveal more options")
                .AddChoices(Enum.GetValues<TaskPriority>()));        
    }

    public void UpdateTask()
    {
        Console.WriteLine("You choiced UpdateTask option");
    }

    public void DeleteTask()
    {
        Console.WriteLine("You choiced DeleteTask option");
    }

    public override string ToString()
    {
        return $"Title: {Title}, Descritpion: {Descritpion}, TaskPriority: {TaskPriority}, Is finished: {IsFinish}";
    }
}