using System.ComponentModel;

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

                TaskService taskService = new();

                switch (Display.MainChoice())
                {
                    case "Add Task":
                        taskService.AddTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        break;
                    case "Update Task":
                        taskService.UpdateTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        break;
                    case "Delete Task":
                        taskService.DeleteTask(taskData.ListOfTasks);
                        taskData.SaveToFile();
                        break;
                    case "Sort Table":
                        taskService.SortTable(taskData.ListOfTasks);
                        break;
                    case "Close the program":
                        return;
                }
                Console.Clear();
            }
        }
    }
}