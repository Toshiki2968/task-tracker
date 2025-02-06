using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Interfaces;
using TaskTracker.Services;

// サービスのDI登録
var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var _taskService = serviceProvider.GetService<ITaskService>();

Console.WriteLine("コマンドを入力してください");
var command = Console.ReadLine();

if (command == null) return;
var commands = command.Split(" ");

switch (commands[0])
{
    case "add":
        _taskService?.AddTask(commands[1].Replace("\"", ""));
        break;
    case "update":
        Int32.TryParse(commands[1], out var updateTaskId);
        if (updateTaskId == 0) return;
        _taskService?.UpdateTask(updateTaskId, commands[1].Replace("\"", ""));
        break;
    case "delete":
        Int32.TryParse(commands[1], out var deleteTaskId);
        if (deleteTaskId == 0) return;
        _taskService?.DeleteTask(deleteTaskId);
        break;
    case "mark-in-progress":
        Int32.TryParse(commands[1], out var updateProgressTaskId);
        if (updateProgressTaskId == 0) return;
        _taskService?.SetStatus(Status.InProgress, updateProgressTaskId);
        break;
    case "mark-done":
        Int32.TryParse(commands[1], out var updateDoneTaskId);
        if (updateDoneTaskId == 0) return;
        _taskService?.SetStatus(Status.Done, updateDoneTaskId);
        break;
    case "list":
        if (commands.Count() == 1)
        {
            var allTasks = _taskService?.GetAllTasks();
            if (allTasks == null) return;
            foreach (var task in allTasks)
            {
                Console.WriteLine(task.Description);
            }
            return;
        }
        switch (commands[1])
        {
            case "done":
                var doneTasks = _taskService?.GetTaskByStatus(Status.Done);
                if (doneTasks == null) return;
                foreach (var task in doneTasks)
                {
                    Console.WriteLine(task.Description);
                }
                break;
            case "todo":
                var todoTasks = _taskService?.GetTaskByStatus(Status.Todo);
                if (todoTasks == null) return;
                foreach (var task in todoTasks)
                {
                    Console.WriteLine(task.Description);
                }
                break;
            case "in-progress":
                var progressTasks = _taskService?.GetTaskByStatus(Status.InProgress);
                if (progressTasks == null) return;
                foreach (var task in progressTasks)
                {
                    Console.WriteLine(task.Description);
                }
                break;
            default:
                break;
        }
        break;
    default:
        return;
}

static void ConfigureServices(IServiceCollection services)
{
    // Register services here
    services.AddSingleton<ITaskService, TaskService>();
}