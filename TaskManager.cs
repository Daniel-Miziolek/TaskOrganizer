using Spectre.Console;
using Spectre.Console.Cli;

namespace TaskOrganizer;

public class TaskManager
{
    public static void DisplayTasksList()
    {
        var table = new Table().Border(TableBorder.Heavy);

        if (TaskData.ListOfTasks.Count == 0)
        {
            AnsiConsole.WriteLine("You task list is empty");
        }
    }
}