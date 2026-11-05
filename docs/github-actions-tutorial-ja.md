# はじめての GitHub Actions：スターターから C# のビルドまで

対象：GitHub にコードを置いたことがあり、GitHub Actions は初めての人。
目安：30〜45分。ブラウザーで操作できます。

この教材では、まずスターターの workflow を動かし、ログを読みます。そのあと、このリポジトリの `MyFirstApp` を自動ビルドします。最後に、わざと失敗させて直す練習をします。

## 1. GitHub Actions で何ができる？

GitHub Actions は、リポジトリで起きた出来事をきっかけに、決めておいた処理を実行する仕組みです。
たとえば「Pull Request（PR）が作られたら、C# のコードをビルドする」という使い方ができます。
変更するたびに自動で確認する使い方を、CI（継続的インテグレーション）と呼びます。

| 用語                     | この教材での意味                                                        |
| ------------------------ | ----------------------------------------------------------------------- |
| workflow（ワークフロー） | 自動処理の全体。YAML ファイルに書く                                     |
| event（イベント）        | 実行のきっかけ。`push` や PR の作成など                                 |
| job（ジョブ）            | 同じ runner 上で行う処理のまとまり                                      |
| runner（ランナー）       | 処理を実行するコンピューター                                            |
| step（ステップ）         | job の中の一つの処理。基本的に上から順に実行する                        |
| action（アクション）     | 再利用できる処理の部品。`uses` で呼び出す                               |
| run（実行履歴）          | workflow を1回動かした記録。YAML の `run:` はコマンド実行を指定するキー |

```text
push などのイベント
  → workflow を開始
    → job を runner で実行
      → step 1 → step 2 → step 3
```

workflow と action は別の用語です。workflow の中のステップで、必要な action を使います。

## 2. 準備する

- 自分がファイルを編集できる GitHub リポジトリを使います。C# の演習まで進める場合は、このリポジトリ、またはその内容を持つ自分のリポジトリを使ってください。
- この教材では既定のブランチを `main` とします。違う場合は、コード中の `main` を実際の名前に変更します。
  - `practice/github-actions`でやる
- ローカルへの追加ソフトのインストールは、ブラウザーだけで進める場合は不要です。

実行場所は自分の PC ではなく、GitHub が用意する runner です。PC の電源を切っても、開始済みの処理は続きます。

公開リポジトリで標準の GitHub ホステッド runner を使う場合は無料です。非公開リポジトリにはプランに応じた利用枠があるので、アカウントの課金設定で使用量を確認してください。詳しくは[公式の課金説明](https://docs.github.com/ja/billing/concepts/product-billing/github-actions)を参照してください。

> 公開リポジトリで使用しよう★

## 3. 「Simple workflow」のスターターを選ぶ

スターターは、workflow のひな形です。`actions/starter-workflows` リポジトリを丸ごとコピーしたり、fork したりする必要はありません。

1. **自分のリポジトリ**を GitHub で開きます。
2. 上部の **Actions** タブをクリックします。
3. 実行履歴が表示されたら **New workflow** をクリックします。初回はテンプレートの選択画面が直接開くことがあります。
4. **Simple workflow** を探し、**Configure**（構成）をクリックします。
5. エディターに `name: CI`、`jobs:`、`echo` などがあることを確認します。まだ保存しません。

表示名や配置は変わる場合があります。見つからないときは、[公式の基本スターター](https://github.com/actions/starter-workflows/blob/main/ci/blank.yml)を開いて内容を確認し、**set up a workflow yourself** から同じひな形を貼り付けても進められます。

テンプレートの選択操作は[公式手順](https://docs.github.com/ja/actions/how-tos/write-workflows/use-workflow-templates)にも掲載されています。

## 4. 最初の workflow を保存する

選んだスターターをもとに、名前と表示メッセージを学習用に変えます。
ファイルの保存先を `.github/workflows/first-actions.yml` にして、内容を次のようにします。
エディターがすでに `.github/workflows/` を表示している場合は、ファイル名の欄には `first-actions.yml` だけを入力します。

```yaml
name: First GitHub Actions

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]
  workflow_dispatch:

permissions:
  contents: read

jobs:
  hello:
    runs-on: ubuntu-latest
    steps:
      - name: Say hello
        run: echo "Hello, GitHub Actions!"

      - name: Show runner information
        run: |
          echo "This job runs on Linux."
          pwd
```

ここでは文字を出すだけなので、スターターにある `actions/checkout` は省いています。コードを扱う演習で追加します。

1. **Commit changes** をクリックします。
2. コミットメッセージに `Add first GitHub Actions workflow` と入力します。
3. 自分の練習用リポジトリで直接コミットできる場合は、`main` にコミットします。
4. PR が必要な場合は、新しいブランチを作る選択肢で保存し、PR を作成して `main` にマージします。

`main` に直接コミットすると `push` が発生し、自動実行が始まります。PR の場合は、`main` 向けの PR を作成すると実行され、マージ後にも `push` によって実行されます。

YAML は字下げで親子関係を表します。タブではなくスペースを使い、まずはコードブロック全体をそのまま貼り付けてください。

## 5. 実行結果とログを読む

1. リポジトリの **Actions** タブを開きます。
2. 左側で **First GitHub Actions** を選びます。
3. 実行履歴のうち、今回のコミットまたは PR に対応するものを開きます。
4. **hello** ジョブをクリックします。
5. **Say hello** ステップを開きます。

ログに次の文字があれば、コマンドが動いています。

```text
Hello, GitHub Actions!
```

**Show runner information** では、メッセージと作業ディレクトリが表示されます。`pwd` は現在のディレクトリを表示する Linux のコマンドです。

緑のチェックは成功、赤い × は失敗です。待機中や実行中の場合は、完了まで待ちます。自動で追加される準備・後片付けのステップが見えても正常です。

### 手動でも動かしてみる

workflow が `main` に入ったら、Actions → **First GitHub Actions** → **Run workflow** をクリックします。ブランチに `main` を選び、実行します。

手動実行には `workflow_dispatch:` が必要です。また、その workflow ファイルが既定のブランチに存在する必要があります。[公式の手動実行手順](https://docs.github.com/ja/actions/how-tos/manage-workflow-runs/manually-run-a-workflow)も確認できます。

**ここまでの達成条件：自動実行と手動実行の両方で、ログのメッセージを確認できた。**

## 6. YAML を読み解く

| 記述                            | 役割                                                                 |
| ------------------------------- | -------------------------------------------------------------------- |
| `name`                          | Actions 画面での workflow の表示名                                   |
| `on`                            | いつ動くかを指定する                                                 |
| `push.branches: [main]`         | `main` にコミットが push されたときに動く                            |
| `pull_request.branches: [main]` | マージ先が `main` の PR の作成・更新などで動く                       |
| `workflow_dispatch`             | 手動実行を受け付ける                                                 |
| `permissions.contents: read`    | 自動発行される `GITHUB_TOKEN` に、リポジトリの内容を読む権限を与える |
| `jobs.hello`                    | `hello` という ID の job を定義する                                  |
| `runs-on: ubuntu-latest`        | GitHub が用意する Ubuntu Linux の runner を使う                      |
| `steps`                         | 実行する処理を並べる                                                 |
| `run`                           | runner でコマンドを実行する                                          |
| `run: \|`                       | 複数行のコマンドを書く                                               |

`pull_request` の `branches` は、作業ブランチ名ではなく**マージ先のブランチ名**を指定します。
また、`name` を変えても起動条件は変わりません。起動条件を決めるのは `on` です。

## 7. `.NET` スターターで C# をビルドする

次は「文字を表示する」から「自分のコードを確認する」に進みます。

1. Actions → **New workflow** を開きます。
2. `.NET` を検索し、ビルドとテスト用のテンプレートで **Configure** をクリックします。
3. [公式の .NET スターター](https://github.com/actions/starter-workflows/blob/main/ci/dotnet.yml)と見比べ、`Setup .NET`、`Restore dependencies`、`Build`、`Test` の流れを確認します。
4. 保存先を `.github/workflows/dotnet.yml` にし、内容を次の学習用コードに置き換えます。

このリポジトリでは `MyFirstApp/MyFirstApp.csproj` が `net10.0` を対象にしています。また、複数のプロジェクトがあるため、処理対象の `.csproj` を明示します。

```yaml
name: CSharp Build

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]
  workflow_dispatch:

permissions:
  contents: read

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: "10.0.x"

      - name: Restore dependencies
        run: dotnet restore MyFirstApp/MyFirstApp.csproj

      - name: Build
        run: dotnet build MyFirstApp/MyFirstApp.csproj --configuration Release --no-restore
```

この例は、参照した `.NET` スターターの action の指定を使っています。テンプレートの内容やバージョンは更新されるため、画面の初期値とは違う場合があります。

### スターターから変更した理由

- **SDK を `10.0.x` にする：** `net10.0` のコードをビルドするためです。`x` は 10.0 系のパッチバージョンを選ぶ指定です。
- **`.csproj` を指定する：** リポジトリのルートで対象が決まらず失敗することを避けます。
- **最初は `Test` を省く：** 今回対象にする `MyFirstApp` はコンソールアプリです。まずビルドの成功を確認します。

保存は前と同じようにコミット、必要なら PR とマージで行います。Actions の **CSharp Build** → **build** → **Build** を開いて、成功したことを確認してください。C# の警告が表示されても、エラーがなければビルドが成功する場合があります。

### それぞれのステップは何をする？

`actions/checkout` は、リポジトリのコードを runner に取得します。GitHub 上にコードがあるだけでは、job の作業ディレクトリにはまだ配置されていません。

`actions/setup-dotnet` は、指定した .NET SDK を使える状態にします。自分の PC に入っている SDK は runner には引き継がれません。設定項目は[公式 README](https://github.com/actions/setup-dotnet)で確認できます。

`dotnet restore` は必要なパッケージを取得します。`dotnet build` はソースコードをコンパイルします。直前に restore したので、`--no-restore` で重複する復元処理を省きます。

`uses` は既存の action を呼び出し、`run` はコマンドを実行します。`with` は action に渡す設定です。`@v4` は action のバージョン指定であり、.NET SDK のバージョンではありません。

**ビルド成功は、アプリのすべての動作が正しいことを意味しません。** テスト用のプロジェクトを用意したら `dotnet test` を追加し、期待する動作も確認できます。アプリの公開（デプロイ）も別の処理です。

WPF や Windows Forms など、別のプロジェクトを対象にするときは OS などの条件も見直します。まずはこの教材の `MyFirstApp` 一つで成功させてください。

## 8. わざと失敗させて、直してみる

まず、文字表示用の `.github/workflows/first-actions.yml` を開きます。最後のステップとして、次を追加してコミットします。PR 運用の場合は作業ブランチで変更し、PR を開いて確認します。

```yaml
- name: Practice failure
  run: exit 1
```

1. **First GitHub Actions** の新しい実行履歴を開きます。
2. **Practice failure** が赤くなっていることを確認します。
3. ログで終了コード `1` に関するエラーを探します。
4. 追加した2行を削除して、もう一度コミットします。
5. **新しいコミットの実行履歴**が緑になることを確認します。

終了コードはコマンドの結果を表す数値です。`0` は通常成功、`1` などの 0 以外は失敗を表します。

**Re-run jobs** は元の実行と同じコミットを使います。ファイルを修正したあとは、新しいコミットで始まった実行を確認しましょう。以前の赤い履歴は残っていて問題ありません。

## 9. つまずいたとき

| 症状                             | 確認すること                                                                                                       |
| -------------------------------- | ------------------------------------------------------------------------------------------------------------------ |
| workflow が実行されない          | `.github/workflows/` に `.yml` または `.yaml` ファイルがあるか。GitHub にコミット済みか。`on` の条件に合っているか |
| 作業ブランチへの push で動かない | この教材では push は `main` 限定。`main` 向けの PR を作ると PR のイベントで動く                                    |
| Run workflow がない              | `workflow_dispatch:` があるか。ファイルが既定のブランチに入っているか                                              |
| Invalid workflow file            | エラーの行番号を確認。字下げ、コロン、タブの混入を見直す                                                           |
| プロジェクトが見つからない       | `.csproj` のパスと大文字・小文字を確認。Linux では大文字・小文字を区別する                                         |
| .NET 10 を対象にできない         | `dotnet-version: '10.0.x'` になっているか                                                                          |
| C# のビルドで失敗する            | 赤いステップのログから最初の `error` を探し、ファイル名と行番号を確認する                                          |
| Actions が利用できない           | リポジトリの Settings → Actions → General の設定や組織の制限を確認する                                             |

調べるときは「workflow 名」「失敗した step 名」「ログのエラー文」の3つをセットで記録すると、問題の場所を絞りやすくなります。

GitHub 上で編集した内容を PC に取り込むときは、ローカルの未コミット変更を確認してから、`main` で `git pull` します。同じ workflow をローカルでも別に作る必要はありません。

## 10. 理解度チェック

- [ ] workflow ファイルの保存場所を説明できる。
- [ ] `push` と `pull_request` の違いを説明できる。
- [ ] runner が自分の PC とは別だとわかる。
- [ ] `uses` と `run` の違いを説明できる。
- [ ] ログから成功したステップと失敗したステップを見つけられる。
- [ ] `MyFirstApp` のビルドに成功した。
- [ ] 失敗する変更を直し、新しい実行が成功した。

次の小さな練習として、別のコンソールプロジェクトを一つ選び、`.csproj` のパスを変更してビルドしてみましょう。

文字表示の練習が終わって自動実行を止めたい場合は、Actions で **First GitHub Actions** を選び、メニューから **Disable workflow** を選べます。CSharp Build は引き続き学習に使えます。

## 参考資料

2026年11月5日に公式資料とテンプレートを確認して作成しました。この教材の workflow は GitHub 上では未実行です。実際の動作確認は上記の演習で行ってください。

- [スターター一覧](https://github.com/actions/starter-workflows)：自分の用途に合うひな形を探す。
- [サンプル ワークフローの作成](https://docs.github.com/ja/actions/tutorials/create-an-example-workflow)：workflow の基本構造とログの確認方法を復習する。
- [ワークフロー テンプレートの使用](https://docs.github.com/ja/actions/how-tos/write-workflows/use-workflow-templates)：GitHub の画面でテンプレートを選ぶ手順を確認する。
