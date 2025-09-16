using Spectre.Console;
using Spectre.Console.Cli;

namespace TaskOrganizer
{
    class Program
    {
        static void Main()
        {
            var taskData = TaskData.LoadFromFile();

            while (true)
            {
                Display.DisplayTasksList(taskData.ListOfTasks);

                TaskItem task = new();

                switch (Display.MainChoice())
                {
                    case "Add Task":
                        task.AddTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        Console.Clear();
                        break;
                    case "Update Task":
                        task.UpdateTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        Console.Clear();
                        break;
                    case "Delete Task":
                        task.DeleteTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        Console.Clear();
                        break;
                    case "Close the program":
                        return;
                }
            }            
        }
    }
}