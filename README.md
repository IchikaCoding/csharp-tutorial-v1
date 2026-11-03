# C# Tutorial

C# と .NET を基礎から学ぶために作成した練習用リポジトリです。
コンソールアプリで文法を練習し、ASP.NET Core（MVC / Razor Pages）と Entity Framework Core を使った Web アプリまで、段階的に作っています。
※個人的に学習するためのリポジトリで、教材ではありません。

## 開発環境

| 項目               | バージョン・ツール             |
| ------------------ | ------------------------------ |
| .NET               | 10.0                           |
| 言語               | C#                             |
| Web フレームワーク | ASP.NET Core MVC / Razor Pages |
| ORM                | Entity Framework Core 10       |
| データベース       | SQL Server LocalDB             |
| エディター         | Visual Studio / VS Code        |

## プロジェクト一覧

### Web アプリ

| プロジェクト                              | 内容                                                                                            |
| ----------------------------------------- | ----------------------------------------------------------------------------------------------- |
| [MvcMovie](MvcMovie/)                     | ASP.NET Core MVC のチュートリアル。映画の CRUD に加えて、住民（Resident）を管理する CRUD を自作 |
| [RazorPagesMovie](RazorPagesMovie/)       | Razor Pages のチュートリアル。映画の CRUD と、フォーム送信・PRG パターンの練習ページ            |
| [RazorPagesStudyLog](RazorPagesStudyLog/) | 学習記録フォーム。データ注釈によるバリデーションの練習                                          |

### デスクトップアプリ

| プロジェクト                        | 内容                             |
| ----------------------------------- | -------------------------------- |
| [FirstWpfApp1](FirstWpfApp1/)       | WPF のはじめてのアプリ           |
| [MyWeatherCSharp](MyWeatherCSharp/) | Windows Forms で作った天気アプリ |

### コンソールアプリ（文法の練習）

| プロジェクト                                                              | 学んだこと                                             |
| ------------------------------------------------------------------------- | ------------------------------------------------------ |
| [MyFirstApp](MyFirstApp/)                                                 | C# の基本文法・クラス                                  |
| [OopTraining](OopTraining/) / [OopTrainingV2](OopTrainingV2/)             | オブジェクト指向（継承・カプセル化・ポリモーフィズム） |
| [EnumTraining](EnumTraining/)                                             | 列挙型（enum）                                         |
| [StructTraining](StructTraining/) / [StructTrainingV2](StructTrainingV2/) | 構造体（struct）とクラスの違い                         |
| [OutAndRefTraining](OutAndRefTraining/)                                   | `out` / `ref` 引数                                     |
| [TwoDArrayTraining](TwoDArrayTraining/)                                   | 2次元配列・ジャグ配列                                  |
| [FileTraining](FileTraining/) / [FileTrainingV2](FileTrainingV2/)         | ファイルの読み書き                                     |
| [AsyncAwaitTraining](AsyncAwaitTraining/)                                 | 非同期処理（`async` / `await`）                        |
| [ResidentSandbox](ResidentSandbox/)                                       | MvcMovie の Resident を試すためのサンドボックス        |

## 実行方法

### コンソールアプリ

```bash
dotnet run --project OopTraining
```

### Web アプリ（MvcMovie の例）

1. EF Core のツールをインストールする（初回のみ）

   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. データベースを作成する

   ```bash
   dotnet ef database update --project MvcMovie
   ```

3. アプリを起動して、表示された URL をブラウザーで開く

   ```bash
   dotnet run --project MvcMovie
   ```

> [!NOTE]
> MvcMovie の接続文字列は、DB ファイルを `D:\Dev\Databases` に作る設定になっています。
> 別の環境で動かすときは、`MvcMovie/appsettings.json` の `AttachDbFilename` を変更してください。

## 学習メモ

- [個人用タスク管理アプリへの学習教材](個人用タスク管理アプリへの学習教材.md)
- [ミニチュートリアル 2：DBなしのフォームと OnPost](ミニチュートリアル%202：DBなしのフォームと%20OnPost.md)

## 開発の進め方

1. Issue を作成する
2. `feature/○○` ブランチで作業する
3. PR を作成し、本文に `Closes #Issue番号` を書く（[PR テンプレート](docs/pull_request_template.md)）
4. `main` にマージする

## 今後の予定

- [ ] MvcMovie を個人用タスク管理アプリに発展させる
