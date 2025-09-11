using System.Linq.Expressions;
using Spectre.Console;

namespace TaskOrganizer;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public bool IsFinish = false;

    public void AddTask()
    {
        Id = TaskData.ListOfTasks.Count + 1;

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

        TaskData.ListOfTasks.Add(this);
    }

    public void UpdateTask()
    {
        Console.WriteLine("You choiced UpdateTask option");
    }

    public void DeleteTask()
    {
        var ides = TaskData.ListOfTasks
            .Select(t => t.Id)
            .ToList();

        var taskDeleteId = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Choose task to delete")
                .MoreChoicesText("[grey](Move up and down to reval more tasks)[/]")
                .AddChoices(ides));

        TaskData.ListOfTasks.RemoveAll(x => x.Id == taskDeleteId);
        CheckId();
    }

    private void CheckId()
    {
        for (int i = 0; i < TaskData.ListOfTasks.Count; i++)
        {
            TaskData.ListOfTasks[i].Id = i + 1;
        }
    }

    public override string ToString()
    {
        return $"Id: {Id} Title: {Title}, Description: {Description}, TaskPriority: {TaskPriority}, Is finished: {IsFinish}";
    }
}