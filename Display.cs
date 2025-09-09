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
                       ])
               ); ;
    }
}
