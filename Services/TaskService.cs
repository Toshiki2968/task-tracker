using System.Text.Json;
using TaskTracker.Interfaces;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        private static string filePath = "task.json";
        public void AddTask(string description)
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
            var tasks = GetTasksFromJson();
            int newId = GetNextId(tasks);
            var newTask = new Task
            {
                Id = newId,
                Description = description,
                Status = Status.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            tasks.Add(newTask);

            string json2 = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json2);
        }
        public void UpdateTask(int id, string description)
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
            var tasks = GetTasksFromJson();
            var target = tasks.Find((task) => task.Id == id);
            if (target == null)
            {
                Console.WriteLine("タスクが見つかりませんでした");
                return;
            }
            target.Description = description;
        }
        public void DeleteTask(int id)
        {
            var tasks = GetTasksFromJson();
            tasks.RemoveAll((task) => task.Id == id);
        }
        public void SetStatus(Status status, int id)
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
            var tasks = GetTasksFromJson();
            var target = tasks.Find((task) => task.Id == id);
            if (target == null)
            {
                Console.WriteLine("タスクが見つかりませんでした");
                return;
            }
            target.Status = status;
        }
        public List<Task> GetAllTasks()
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
            return  GetTasksFromJson();
        }
        public List<Task> GetTaskByStatus(Status status)
        {
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
            var tasks = GetTasksFromJson();
            return tasks.FindAll((task) => task.Status == status);
        }

        private static int GetNextId(List<Task> tasks)
        {
            return tasks.Count > 0 ? tasks[^1].Id + 1 : 1; // 最後のID + 1、リストが空の場合は1
        }

        private static List<Task> GetTasksFromJson()
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
        }
    }

}