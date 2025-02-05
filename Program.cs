using System.Text.Json;

Console.WriteLine("コマンドを入力してください");
var command = Console.ReadLine();

if (command == null) return;
var commands = command.Split(" ");

// JSON読み込み
string filePath = "task.json";
List<Task> tasks = LoadJson(filePath);

switch (commands[0])
{
    case "add":
        int newId = GetNextId(tasks);
        var newTask = new Task
            {
                Id = newId,
                Description = commands[1].Replace("\"", ""),
                Status = Status.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt =  DateTime.Now
            };
        tasks.Add(newTask);
        Console.WriteLine("タスクを追加しました");
        break;
    case "update":
        var target = tasks.Find((task) => task.Id.ToString() == commands[1]);
        if (target == null)
        {
            Console.WriteLine("タスクが見つかりませんでした");
            return;
        }
        target.Description = commands[2].Replace("\"","");
        Console.WriteLine("タスクを更新しました");
        break;
    case "delete":
        tasks.RemoveAll((task) => task.Id.ToString() == commands[1]);
        Console.WriteLine("タスクを削除しました");
        break;
    case "mark-in-progress":
        var targetMarkInProgress = tasks.Find((task) => task.Id.ToString() == commands[1]);
        if (targetMarkInProgress == null)
        {
            Console.WriteLine("タスクが見つかりませんでした");
            return;
        }
        targetMarkInProgress.Status = Status.InProgress;
        Console.WriteLine("タスクを進行中へ変更しました");
        break;
    case "mark-done":
        var targetMarkDone = tasks.Find((task) => task.Id.ToString() == commands[1]);
        if (targetMarkDone == null)
        {
            Console.WriteLine("タスクが見つかりませんでした");
            return;
        }
        targetMarkDone.Status = Status.Done;
        Console.WriteLine("タスクを完了へ変更しました");
        break;
    case "list":
        if (commands.Count() == 1)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Description);
            }
            Console.WriteLine("すべてのタスクを表示しました"); 
            return;
        }
        switch (commands[1])
        {
            case "done":
                foreach (var task in tasks)
                {
                    if (!(task.Status == Status.Done)) continue;
                    Console.WriteLine(task.Description);
                }
                Console.WriteLine("完了済みのタスクを表示しました");
                break;
            case "todo":
                foreach (var task in tasks)
                {
                    if (!(task.Status == Status.Todo)) continue;
                    Console.WriteLine(task.Description);
                }
                Console.WriteLine("新しいタスクを表示しました");
                break;
            case "in-progress":
                foreach (var task in tasks)
                {
                    if (!(task.Status == Status.InProgress)) continue;
                    Console.WriteLine(task.Description);
                }
                Console.WriteLine("実行中のタスクを表示しました");
                break;
            default:
                break;
        }
        break;
    default:
        return;
}

SaveJson(filePath, tasks);


List<Task> LoadJson(string filePath)
{
    if (!File.Exists(filePath))
    {
        // jsonファイルを作成
        File.WriteAllText(filePath, "[]");
    }
    string json = File.ReadAllText(filePath);

    // JSONからオブジェクトへデシリアライズ
    return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
}

void SaveJson(string filePath, List<Task> tasks)
{
    // オブジェクトからJSONへシリアライズ
    string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions{WriteIndented = true});
    File.WriteAllText(filePath, json);
}

// 一意のIDを取得
static int GetNextId(List<Task> tasks)
{
    return tasks.Count > 0 ? tasks[^1].Id + 1 : 1; // 最後のID + 1、リストが空の場合は1
}