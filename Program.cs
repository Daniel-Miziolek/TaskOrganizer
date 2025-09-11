using Spectre.Console;
using Spectre.Console.Cli;

namespace TaskOrganizer
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Display.DisplayTasksList();
                
                TaskItem task = new();

                switch (Display.MainChoice())
                {
                    case "Add Task":
                        task.AddTask();
                        Console.WriteLine(task);
                        break;
                    case "Update Task":
                        task.UpdateTask();
                        break;
                    case "Delete Task":
                        task.DeleteTask();
                        break;
                    case "Close the program":
                        return;
                }
            }            
        }
    }
}