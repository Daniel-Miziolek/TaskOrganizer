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

    public void UpdateTask(List<TaskItem> tasks)
    {
        if (!tasks.Any())
        {
            AnsiConsole.MarkupLine("[red]Your tasks list is empty[/]. Press any key to continue.");
            Console.ReadKey();
            return;
        }

        var taskToEdit = AnsiConsole.Prompt(
            new SelectionPrompt<TaskItem>()
                .Title("Choose task to edit")
                .UseConverter(t => $"{t.Id}: {t.Title}")
                .AddChoices(tasks));

        taskToEdit.Title = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter title of your task: ")
                .AllowEmpty()
                .DefaultValue(taskToEdit.Title));

        taskToEdit.Description = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter description of your task: ")
                .AllowEmpty()
                .DefaultValue(taskToEdit.Description));

        taskToEdit.TaskPriority = AnsiConsole.Prompt(
            new SelectionPrompt<TaskPriority>()
                .PageSize(10)
                .Title("Choose priority of your task")
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<TaskPriority>()
                    .OrderBy(p => p != taskToEdit.TaskPriority)));
        
        taskToEdit.IsFinish = AnsiConsole.Prompt(
            new TextPrompt<bool>("Is the task finished?: ")
                .AddChoices(new[] { true, false})
                .DefaultValue(taskToEdit.IsFinish)
                .WithConverter(choice => choice ? "Finished" : "Not finished")
                .AllowEmpty());
    }

    public void DeleteTask(List<TaskItem> tasks)
    {
        if (!tasks.Any())
        {
            AnsiConsole.MarkupLine("[red]Your tasks list is empty[/]. Press any key to continue.");
            Console.ReadKey();
            return;
        }

        var toDelete = AnsiConsole.Prompt(
            new MultiSelectionPrompt<TaskItem>()
                .Title("Choose task(s) to delete")
                .UseConverter(t => $"{t.Id}: {t.Title}")
                .MoreChoicesText("[grey](Move up and down to reveal more tasks)[/]")
                .AddChoices(tasks));

        tasks.RemoveAll(t => toDelete.Contains(t));
        CheckId(tasks);             
    }

    private void CheckId(List<TaskItem> tasks)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }
    }

    public override string ToString() => $"Id: {Id} Title: {Title}, Description: {Description}, TaskPriority: {TaskPriority}, Is finished: {IsFinish}"; 
}