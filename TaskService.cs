using Spectre.Console;

namespace TaskOrganizer;

class TaskService
{
    public void AddTask(List<TaskItem> tasks)
    {
        var taskItem = new TaskItem
        {
            Id = tasks.Count + 1,

            Title = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter title of your task: ")),

            Description = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter descrtiption of your task")),

            TaskPriority = AnsiConsole.Prompt(
            new SelectionPrompt<TaskPriority>()
                .Title("Choose priority of your task")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<TaskPriority>())),

            IsFinish = false
        };

        tasks.Add(taskItem);
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
            new SelectionPrompt<bool>()
                .Title("Is the task finished?")
                .AddChoices(taskToEdit.IsFinish, !taskToEdit.IsFinish)
                .UseConverter(choice => choice ? "Finished" : "Not finished"));
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

}