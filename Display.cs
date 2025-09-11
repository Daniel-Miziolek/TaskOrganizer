using Spectre.Console;

namespace TaskOrganizer;

public class Display
{
    public static string MainChoice()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose one of the options")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices([
                    "Add Task", "Update Task", "Delete Task", "Close the program"
                ]));
    }

    public static void DisplayTasksList()
    {
        var table = new Table().Border(TableBorder.Heavy);

        table.AddColumn("ID");
        table.AddColumn("TITLE");
        table.AddColumn("DESCRIPTION");
        table.AddColumn("PRIORITY");
        table.AddColumn("IS FINISH");

        foreach (var task in TaskData.ListOfTasks)
        {
            table.AddRow(task.Id.ToString(), task.Title, task.Description, task.TaskPriority.ToString(), task.IsFinish.ToString());
        }

        AnsiConsole.Write(table);
    }
}
