using System.Linq.Expressions;
using Spectre.Console;

namespace TaskOrganizer;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public bool IsFinish { get; set; }

    public void AddTask(List<TaskItem> tasks)
    {
        Id = tasks.Count + 1;

        Title = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter title of your task: "));

        Description = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter descrtiption of your task"));

        TaskPriority = AnsiConsole.Prompt(
            new SelectionPrompt<TaskPriority>()
                .Title("Choose priority of your task")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<TaskPriority>()));

        IsFinish = false;

        tasks.Add(this);
    }

    public void UpdateTask()
    {
        Console.WriteLine("You choiced UpdateTask option");
    }

    public void DeleteTask(List<TaskItem> tasks)
    {
        if (tasks.Count > 0)
        {
            var ides = tasks
                .Select(t => t.Id)
                .ToList();

            var taskDeleteId = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Choose task to delete")
                    .MoreChoicesText("[grey](Move up and down to reval more tasks)[/]")
                    .AddChoices(ides));

            tasks.RemoveAll(x => x.Id == taskDeleteId);
            CheckId(tasks);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Your tasks list is empty[/]. Press any key to conitinue");
            Console.ReadKey();
            return;            
        }        
    }

    private void CheckId(List<TaskItem> tasks)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }
    }

    public override string ToString()
    {
        return $"Id: {Id} Title: {Title}, Description: {Description}, TaskPriority: {TaskPriority}, Is finished: {IsFinish}";
    }
}