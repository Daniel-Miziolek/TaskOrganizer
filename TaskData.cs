using System.Text.Json;

namespace TaskOrganizer;

class TaskData
{
    public List<TaskItem> ListOfTasks { get; set; } = new();

    private static string FilePath = "task_data.json";

    public void SaveToFile()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(FilePath, JsonSerializer.Serialize(this, options));
    }

    public static TaskData LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<TaskData>(json) ?? new TaskData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Could not load data: {ex.Message}");
            }
        }
        return new TaskData();
    }
}