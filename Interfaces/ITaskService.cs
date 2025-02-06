using System.Xml.Serialization;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        void AddTask(string description);
        void UpdateTask(int id, string description);
        void DeleteTask(int id);
        void SetStatus(Status status, int id);
        List<Task> GetAllTasks();
        List<Task> GetTaskByStatus(Status status);
    }
}