# task-tracker
[roadmap.sh](https://roadmap.sh/) の [task-tracker]([https://roadmap.sh/projects/github-user-activity](https://roadmap.sh/projects/task-tracker))のサンプルプロジェクトです。

# 実行方法
- リポジトリをクローンし、次のコマンドを実行します。
```
https://github.com/Toshiki2968/task-tracker.git
cd task-tracker
```

- 次のコマンドを実行します
```
dotnet run
```

- 以下のようなコマンドを入力し、タスクを追加・変更・削除・表示を行います。
- 新しいタスクを追加
  - add "Buy groceries"

- タスクを更新・削除
  - update 1 "Buy groceries and cook dinner"
  - delete 1

- タスクのステータスを更新
  - mark-in-progress 1
  - mark-done 1

- すべてのタスクを表示
  - list

- ステータスごとのタスクを表示
  - list done
  - list todo
  - list in-progress
