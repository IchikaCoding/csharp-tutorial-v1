---
date: 2026-08-22
Links:
  - "[[programming]]"
  - "[[2026-08-22]]"
  - "[[ミニチュートリアル 1：Razor Page を表示する]]"
Categories:
  - Notes
tags:
  - CSharp
  - ASP.NET-Core
  - Razor-Pages
updated: 2026-08-22
---

# ミニチュートリアル 2：DBなしのフォームと `OnPost()`

今回は、前回作った「ページを表示するだけ」の状態から一歩進めて、入力フォームを作ります。

作るのは「推し作品紹介フォーム」です。

DB、EF Core、`DbContext`、スキャフォールディングは使いません。送信された内容は、そのリクエストを処理している間だけ使います。

このチュートリアルでは、完成コードを最初から貼り付けるのではなく、小さく作って、動作を確認し、少しずつ育てます。

---

## 今回のゴール

ブラウザで次のURLを開きます。

```text
https://localhost:xxxx/Practice/Favorite
```

フォームへ次の内容を入力できるようにします。

- あなたの名前
- 推し作品のタイトル
- カテゴリー
- おすすめ理由
- おすすめ度
- ネタバレを含むか

送信すると、同じページの下に「紹介カード」が表示されます。

```text
いちかさんの推し作品

タイトル: 葬送のフリーレン
カテゴリー: アニメ
おすすめ度: ★★★★★
おすすめ理由: 登場人物と時間の描き方が好きだからです。
```

このページを作りながら、次の流れを説明できるようになることが目標です。

```text
GET /Practice/Favorite
    ↓
空のフォームを表示
    ↓
ユーザーが入力して送信
    ↓
POST /Practice/Favorite
    ↓
モデルバインディングで入力値がC#のプロパティへ入る
    ↓
モデル検証で入力値をチェック
    ↓
OnPost()が実行される
    ↓
同じページに結果を表示
```

---

# はじめに：GETとPOSTの役割

前回は、URLを開いたときに`GET`リクエストが送られ、`OnGet()`が呼ばれました。

今回は`POST`も登場します。

| HTTPメソッド | 主な目的 | Razor Pagesのメソッド |
| --- | --- | --- |
| `GET` | ページやデータを取得する | `OnGet()` |
| `POST` | 入力したデータをサーバーへ送る | `OnPost()` |

`GET`と`POST`は、C#のメソッド名ではありません。ブラウザとサーバーが通信するときに使う、HTTPリクエストの種類です。

`OnGet()`や`OnPost()`は、そのHTTPメソッドに対応するRazor Pagesの**ハンドラーメソッド**です。

```text
HTTPのGET  → Razor PagesがOnGet()を呼ぶ
HTTPのPOST → Razor PagesがOnPost()を呼ぶ
```

普通のC#クラスなら、`OnPost()`という名前を付けただけでは自動実行されません。

`PageModel`を継承したRazor Page用クラスだから、ASP.NET Coreが命名規則を見て呼び出します。

---

# ステップ1：新しいRazor Pageを作る

`Pages/Practice`フォルダーに次の2ファイルを作ります。

```text
RazorPagesMovie
└─ Pages
   └─ Practice
      ├─ Favorite.cshtml
      └─ Favorite.cshtml.cs
```

まず`Favorite.cshtml.cs`を書きます。

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Practice;

public class FavoriteModel : PageModel
{
    public void OnGet()
    {
    }
}
```

次に`Favorite.cshtml`を書きます。

```cshtml
@page
@model RazorPagesMovie.Pages.Practice.FavoriteModel

@{
    ViewData["Title"] = "推し作品紹介";
}

<h1>推し作品紹介フォーム</h1>

<p>あなたの推し作品を教えてください。</p>
```

起動して、`/Practice/Favorite`を開きます。

```powershell
dotnet run --project RazorPagesMovie
```

見出しが表示されれば成功です。

## ここで確認

この時点ではフォームをまだ作っていません。

起きていることは前回と同じです。

```text
GET /Practice/Favorite
    ↓
FavoriteModelのインスタンスが作られる
    ↓
OnGet()が呼ばれる
    ↓
Favorite.cshtmlがHTMLを作る
```

---

# ステップ2：最小のフォームを作る

最初は「名前」だけ送信します。

`Favorite.cshtml`の`<p>`の下に追加します。

```cshtml
<form method="post">
    <div>
        <label for="name">名前</label>
        <input id="name" name="Name" type="text" />
    </div>

    <button type="submit">送信する</button>
</form>
```

ここで一番重要なのは次の部分です。

```html
<form method="post">
```

`method="post"`は、「このフォームを送信するときは`POST`リクエストを使う」というHTMLの指定です。

ボタンを押すと、ブラウザはおおよそ次のようなリクエストを送ります。

```http
POST /Practice/Favorite
Content-Type: application/x-www-form-urlencoded

Name=いちか
```

ページを開いただけのときは`GET`、フォームの送信ボタンを押したときは`POST`です。

## なぜ`name="Name"`が必要なのか

```html
<input id="name" name="Name" type="text" />
```

それぞれの属性には役割があります。

| 属性 | 役割 |
| --- | --- |
| `id="name"` | HTML要素を区別する。`label for="name"`とも対応する |
| `name="Name"` | サーバーへ送るデータのキーになる |
| `type="text"` | 1行の文字入力欄にする |

サーバーへ送られる値にとって、特に重要なのは`name`です。

```text
name="Name" + 入力した「いちか」
        ↓
Name=いちか
```

---

# ステップ3：`OnPost()`を作る

このまま送信すると、`POST`を処理するメソッドがありません。

`Favorite.cshtml.cs`を次のように変更します。

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Practice;

public class FavoriteModel : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public string ResultMessage { get; private set; } = "";

    public void OnGet()
    {
    }

    public void OnPost()
    {
        ResultMessage = $"{Name}さん、送信ありがとうございます！";
    }
}
```

そして、`Favorite.cshtml`の`</form>`より下に追加します。

```cshtml
@if (!string.IsNullOrEmpty(Model.ResultMessage))
{
    <p>@Model.ResultMessage</p>
}
```

名前を入力して送信してください。

```text
いちかさん、送信ありがとうございます！
```

と表示されたら成功です。

---

# ステップ4：`[BindProperty]`とモデルバインディング

今回、フォームの値を自分で取り出すコードは書いていません。

```csharp
[BindProperty]
public string Name { get; set; } = "";
```

それでも、入力値が`Name`プロパティへ入っています。

これは**モデルバインディング**という仕組みが働いたからです。

```text
HTML
<input name="Name">
        ↓ POST
Name=いちか
        ↓ モデルバインディング
C#
public string Name { get; set; }
```

モデルバインディングは、HTTPリクエストに入っている文字列を探し、対応するC#のプロパティや引数へ変換して入れる仕組みです。

`[BindProperty]`は「この`PageModel`の`public`プロパティを、リクエストの値と結び付けてください」という指定です。

## 重要：`OnPost()`より前に値が入る

実行順序は次のようになります。

```text
POSTリクエストが届く
    ↓
FavoriteModelのインスタンスが作られる
    ↓
モデルバインディング
Nameプロパティに入力値が入る
    ↓
OnPost()が呼ばれる
```

だから、`OnPost()`の1行目から`Name`を使えます。

```csharp
public void OnPost()
{
    // この時点でNameにはフォームの値が入っている
    ResultMessage = $"{Name}さん、送信ありがとうございます！";
}
```

## 実験1：`[BindProperty]`を一度消してみる

一度だけ`[BindProperty]`をコメントアウトし、もう一度送信してみてください。

```csharp
// [BindProperty]
public string Name { get; set; } = "";
```

予想してから試します。

`Name`にはフォームの値が入らないため、結果は次のようになります。

```text
さん、送信ありがとうございます！
```

確認後、`[BindProperty]`を元に戻してください。

## 実験2：HTMLの`name`を変えてみる

`name="Name"`を`name="UserName"`へ変えると、送信データのキーとC#のプロパティ名が一致しなくなります。

```html
<input id="name" name="UserName" type="text" />
```

この場合も`Name`には値が入りません。

確認後、`name="Name"`へ戻してください。

---

# ステップ5：`asp-for`でHTMLとC#を結び付ける

手書きの`name="Name"`では、スペルミスをしてもC#コンパイラーが気付きにくいという問題があります。

そこで、Razorの**Input Tag Helper**を使います。

`Favorite.cshtml`の入力部分を変更してください。

```cshtml
<div>
    <label asp-for="Name"></label>
    <input asp-for="Name" />
</div>
```

`asp-for="Name"`は、`FavoriteModel.Name`プロパティを指定しています。

ASP.NET Coreは、ブラウザへ返すときにおおよそ次のHTMLへ変換します。

```html
<label for="Name">Name</label>
<input type="text" id="Name" name="Name" value="" />
```

`asp-for`が`id`、`name`、`type`などをプロパティに合わせて生成します。

`asp-for`はHTMLそのものではありません。ASP.NET CoreがHTMLを作るのを助ける**タグヘルパー**です。

このプロジェクトでは`Pages/_ViewImports.cshtml`に次の設定があるため、タグヘルパーを使えます。

```cshtml
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

# ステップ6：入力専用のModelを作る

ここまでは`FavoriteModel`に`Name`を直接置きました。

項目が6個に増えると、ページの処理と入力データが混ざって読みにくくなります。

そこで入力データを、専用のクラスへ分けます。

`Models`フォルダーに`FavoriteInput.cs`を作ってください。

```text
RazorPagesMovie
├─ Models
│  └─ FavoriteInput.cs
└─ Pages
   └─ Practice
      ├─ Favorite.cshtml
      └─ Favorite.cshtml.cs
```

`FavoriteInput.cs`へ次のコードを書きます。

```csharp
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models;

public class FavoriteInput
{
    [Display(Name = "あなたの名前")]
    public string Name { get; set; } = "";

    [Display(Name = "推し作品のタイトル")]
    public string Title { get; set; } = "";

    [Display(Name = "カテゴリー")]
    public string Category { get; set; } = "";

    [Display(Name = "おすすめ理由")]
    public string Reason { get; set; } = "";

    [Display(Name = "おすすめ度")]
    public int Rating { get; set; } = 3;

    [Display(Name = "ネタバレを含む")]
    public bool ContainsSpoiler { get; set; }
}
```

このクラスはDBのテーブルではありません。

フォームから受け取る値をひとまとめにした、**入力モデル**です。画面専用のデータ構造という意味で、ViewModelやInputModelと呼ばれることもあります。

`[Display(Name = "...")]`は画面上の表示名です。`label asp-for`や検証メッセージで使われます。

---

# ステップ7：PageModelで入力モデルを受け取る

`Favorite.cshtml.cs`を次のように変更します。

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Practice;

public class FavoriteModel : PageModel
{
    [BindProperty]
    public FavoriteInput Input { get; set; } = new();

    public bool Submitted { get; private set; }

    public DateTime SubmittedAt { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        Submitted = true;
        SubmittedAt = DateTime.Now;

        return Page();
    }
}
```

## `Input`プロパティ

```csharp
[BindProperty]
public FavoriteInput Input { get; set; } = new();
```

フォーム全体の値を`Input`の中へ入れます。

```text
Input
├─ Name
├─ Title
├─ Category
├─ Reason
├─ Rating
└─ ContainsSpoiler
```

`= new();`によって、空の`FavoriteInput`インスタンスを最初から用意しています。

## `IActionResult`と`return Page()`

`OnPost()`の戻り値を`void`から`IActionResult`へ変えました。

```csharp
public IActionResult OnPost()
```

`IActionResult`は、「このリクエストに対して次に何を返すか」を表せる型です。

```csharp
return Page();
```

`Page()`は、現在の`Favorite.cshtml`をもう一度HTMLにして返します。

今回はDBへ保存した別ページに移動するのではなく、同じページに入力結果を表示したいため`Page()`を使います。

---

# ステップ8：フォームを完成させる

`Favorite.cshtml`をいったん次の全体コードに置き換えます。

```cshtml
@page
@model RazorPagesMovie.Pages.Practice.FavoriteModel

@{
    ViewData["Title"] = "推し作品紹介";
}

<h1>推し作品紹介フォーム</h1>

<p>あなたの推し作品を教えてください。</p>

<form method="post">
    <div class="mb-3">
        <label asp-for="Input.Name" class="form-label"></label>
        <input asp-for="Input.Name" class="form-control" />
    </div>

    <div class="mb-3">
        <label asp-for="Input.Title" class="form-label"></label>
        <input asp-for="Input.Title" class="form-control" />
    </div>

    <div class="mb-3">
        <label asp-for="Input.Category" class="form-label"></label>
        <select asp-for="Input.Category" class="form-select">
            <option value="">選択してください</option>
            <option value="アニメ">アニメ</option>
            <option value="漫画">漫画</option>
            <option value="ゲーム">ゲーム</option>
            <option value="映画">映画</option>
            <option value="小説">小説</option>
            <option value="その他">その他</option>
        </select>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Reason" class="form-label"></label>
        <textarea asp-for="Input.Reason"
                  class="form-control"
                  rows="4"></textarea>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Rating" class="form-label"></label>
        <input asp-for="Input.Rating"
               class="form-control"
               min="1"
               max="5" />
    </div>

    <div class="form-check mb-3">
        <input asp-for="Input.ContainsSpoiler" class="form-check-input" />
        <label asp-for="Input.ContainsSpoiler" class="form-check-label"></label>
    </div>

    <button type="submit" class="btn btn-primary">紹介カードを作る</button>
</form>

@if (Model.Submitted)
{
    <hr />

    <section>
        <h2>@Model.Input.Name さんの推し作品</h2>

        <dl>
            <dt>タイトル</dt>
            <dd>@Model.Input.Title</dd>

            <dt>カテゴリー</dt>
            <dd>@Model.Input.Category</dd>

            <dt>おすすめ度</dt>
            <dd>@Model.Input.Rating / 5</dd>

            <dt>おすすめ理由</dt>
            <dd>@Model.Input.Reason</dd>

            <dt>ネタバレ</dt>
            <dd>@(Model.Input.ContainsSpoiler ? "含みます" : "含みません")</dd>
        </dl>

        <p>送信時刻: @Model.SubmittedAt.ToString("HH:mm:ss")</p>
    </section>
}
```

ページを開き、全項目を入力して送信します。

この時点では未入力でも送信できます。入力チェックは次のステップで追加します。

## `Input.Name`になる理由

`PageModel`側は次の構造です。

```csharp
public FavoriteInput Input { get; set; }
```

`FavoriteInput`の中に`Name`があります。

そのため、`asp-for`にはプロパティをたどって`Input.Name`と書きます。

```cshtml
<input asp-for="Input.Name" />
```

生成されるHTMLはおおよそ次のようになります。

```html
<input type="text"
       id="Input_Name"
       name="Input.Name"
       value="" />
```

送信された`Input.Name=いちか`という値を、モデルバインディングが`Input.Name`プロパティへ入れます。

---

# 重要：GETとPOSTで同じインスタンスを使い回しているわけではない

ここは、とても大切です。

最初にページを開いたときの`FavoriteModel`と、フォーム送信時の`FavoriteModel`は別のインスタンスです。

```text
1回目のリクエスト
GET /Practice/Favorite
    ↓
FavoriteModel インスタンスAを作る
    ↓
OnGet()
    ↓
レスポンスを返して処理終了

2回目のリクエスト
POST /Practice/Favorite
    ↓
FavoriteModel インスタンスBを新しく作る
    ↓
モデルバインディングでInputへ値を入れる
    ↓
OnPost()
    ↓
レスポンスを返して処理終了
```

`OnGet()`で作られたインスタンスAが、そのまま`OnPost()`まで生き残っているわけではありません。

Webでは、基本的に1回のリクエストごとに処理が区切られます。

今回はPOSTされた入力値がリクエストの中に入っているため、インスタンスBでも値を復元できます。

しかしDBへ保存していないので、別のページへ移動したり、後からもう一度取得したりするための永続的なデータにはなっていません。

---

# ステップ9：入力チェックを追加する

現在は、空欄や`0`でも送信できます。

次は**モデル検証（バリデーション）**を追加します。

`Models/FavoriteInput.cs`を次のように変更してください。

```csharp
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models;

public class FavoriteInput
{
    [Display(Name = "あなたの名前")]
    [Required(ErrorMessage = "名前を入力してください。")]
    [StringLength(
        20,
        MinimumLength = 2,
        ErrorMessage = "名前は2文字以上20文字以内で入力してください。")]
    public string Name { get; set; } = "";

    [Display(Name = "推し作品のタイトル")]
    [Required(ErrorMessage = "タイトルを入力してください。")]
    [StringLength(
        50,
        ErrorMessage = "タイトルは50文字以内で入力してください。")]
    public string Title { get; set; } = "";

    [Display(Name = "カテゴリー")]
    [Required(ErrorMessage = "カテゴリーを選択してください。")]
    public string Category { get; set; } = "";

    [Display(Name = "おすすめ理由")]
    [Required(ErrorMessage = "おすすめ理由を入力してください。")]
    [StringLength(
        200,
        MinimumLength = 10,
        ErrorMessage = "おすすめ理由は10文字以上200文字以内で入力してください。")]
    public string Reason { get; set; } = "";

    [Display(Name = "おすすめ度")]
    [Range(1, 5, ErrorMessage = "おすすめ度は1から5で入力してください。")]
    public int Rating { get; set; } = 3;

    [Display(Name = "ネタバレを含む")]
    public bool ContainsSpoiler { get; set; }
}
```

`[Required]`、`[StringLength]`、`[Range]`などは、プロパティに追加情報を付ける**属性（Attribute）**です。

| 属性 | 今回の意味 |
| --- | --- |
| `[Required]` | 空欄を許可しない |
| `[StringLength]` | 文字数の上限・下限を決める |
| `[Range]` | 数値の範囲を決める |
| `[Display]` | 画面に表示する項目名を決める |

これらは`System.ComponentModel.DataAnnotations`名前空間にあります。

## モデルバインディングとモデル検証は別の仕事

似ていますが、役割が違います。

```text
フォームの文字列
    ↓
モデルバインディング
「どのプロパティに、どの値を入れるか」
    ↓
FavoriteInput
    ↓
モデル検証
「その値はルールを守っているか」
    ↓
ModelState
```

たとえば、おすすめ度へ`abc`を送った場合は`int`へ変換できません。これはモデルバインディング時のエラーです。

おすすめ度へ`9`を送った場合は`int`へ変換できますが、`[Range(1, 5)]`に違反します。これはモデル検証のエラーです。

どちらのエラーも`ModelState`に記録されます。

---

# ステップ10：`ModelState.IsValid`を確認する

検証属性を書いただけでは、`OnPost()`の処理を自動で中止してくれるわけではありません。

`Favorite.cshtml.cs`の`OnPost()`を変更します。

```csharp
public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    Submitted = true;
    SubmittedAt = DateTime.Now;

    return Page();
}
```

`ModelState.IsValid`は、モデルバインディングとモデル検証のエラーがない場合に`true`になります。

```text
ModelState.IsValid == false
    ↓
入力エラーがある
    ↓
return Page()
    ↓
エラー付きで同じフォームを再表示

ModelState.IsValid == true
    ↓
入力に問題がない
    ↓
紹介カードを作る処理へ進む
```

この`if`は、MVCのControllerでもよく登場する重要な形です。

---

# ステップ11：エラーメッセージを画面に表示する

`Favorite.cshtml`の`<form method="post">`直後に追加します。

```cshtml
<div asp-validation-summary="ModelOnly" class="text-danger"></div>
```

さらに、各入力欄の直後に`asp-validation-for`を追加します。

名前の場合は次のようになります。

```cshtml
<div class="mb-3">
    <label asp-for="Input.Name" class="form-label"></label>
    <input asp-for="Input.Name" class="form-control" />
    <span asp-validation-for="Input.Name" class="text-danger"></span>
</div>
```

同じ形で、`Title`、`Category`、`Reason`、`Rating`にも追加します。

```cshtml
<span asp-validation-for="Input.Title" class="text-danger"></span>
<span asp-validation-for="Input.Category" class="text-danger"></span>
<span asp-validation-for="Input.Reason" class="text-danger"></span>
<span asp-validation-for="Input.Rating" class="text-danger"></span>
```

最後に、ファイル末尾へ検証用スクリプトを読み込むセクションを追加します。

```cshtml
@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

空欄のまま送信し、エラーが表示されることを確認します。

## 2種類の検証

この構成では、主に2か所で検証されます。

| 種類 | 実行場所 | 特徴 |
| --- | --- | --- |
| クライアント側検証 | ブラウザ | 通信する前に素早く表示できる |
| サーバー側検証 | ASP.NET Core | 不正なリクエストも含め、必ずサーバーで確認できる |

ブラウザ側の検証だけを信用してはいけません。利用者はJavaScriptを無効にしたり、開発者ツールなどからHTTPリクエストを直接変更したりできるからです。

サーバー側の`ModelState.IsValid`の確認が本体で、クライアント側検証は使いやすさを良くする補助と考えます。

---

# ステップ12：完成版の`Favorite.cshtml`

最終的な画面ファイルは次のようになります。

```cshtml
@page
@model RazorPagesMovie.Pages.Practice.FavoriteModel

@{
    ViewData["Title"] = "推し作品紹介";
}

<h1>推し作品紹介フォーム</h1>

<p>あなたの推し作品を教えてください。</p>

<form method="post">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>

    <div class="mb-3">
        <label asp-for="Input.Name" class="form-label"></label>
        <input asp-for="Input.Name" class="form-control" />
        <span asp-validation-for="Input.Name" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Title" class="form-label"></label>
        <input asp-for="Input.Title" class="form-control" />
        <span asp-validation-for="Input.Title" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Category" class="form-label"></label>
        <select asp-for="Input.Category" class="form-select">
            <option value="">選択してください</option>
            <option value="アニメ">アニメ</option>
            <option value="漫画">漫画</option>
            <option value="ゲーム">ゲーム</option>
            <option value="映画">映画</option>
            <option value="小説">小説</option>
            <option value="その他">その他</option>
        </select>
        <span asp-validation-for="Input.Category" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Reason" class="form-label"></label>
        <textarea asp-for="Input.Reason"
                  class="form-control"
                  rows="4"></textarea>
        <span asp-validation-for="Input.Reason" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Input.Rating" class="form-label"></label>
        <input asp-for="Input.Rating" class="form-control" />
        <span asp-validation-for="Input.Rating" class="text-danger"></span>
    </div>

    <div class="form-check mb-3">
        <input asp-for="Input.ContainsSpoiler" class="form-check-input" />
        <label asp-for="Input.ContainsSpoiler" class="form-check-label"></label>
    </div>

    <button type="submit" class="btn btn-primary">紹介カードを作る</button>
</form>

@if (Model.Submitted)
{
    <hr />

    <section class="card">
        <div class="card-body">
            <h2 class="card-title">@Model.Input.Name さんの推し作品</h2>

            <dl>
                <dt>タイトル</dt>
                <dd>@Model.Input.Title</dd>

                <dt>カテゴリー</dt>
                <dd>@Model.Input.Category</dd>

                <dt>おすすめ度</dt>
                <dd>@Model.Input.Rating / 5</dd>

                <dt>おすすめ理由</dt>
                <dd>@Model.Input.Reason</dd>

                <dt>ネタバレ</dt>
                <dd>@(Model.Input.ContainsSpoiler ? "含みます" : "含みません")</dd>
            </dl>

            <p class="text-muted">
                送信時刻: @Model.SubmittedAt.ToString("HH:mm:ss")
            </p>
        </div>
    </section>
}

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

# ステップ13：完成版の`Favorite.cshtml.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Practice;

public class FavoriteModel : PageModel
{
    [BindProperty]
    public FavoriteInput Input { get; set; } = new();

    public bool Submitted { get; private set; }

    public DateTime SubmittedAt { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Submitted = true;
        SubmittedAt = DateTime.Now;

        return Page();
    }
}
```

---

# 送信時に起きていることを分解する

フォームに入力してボタンを押したときの全体像です。

```text
1. ブラウザ
   <form method="post">を見てPOSTを作る
   Input.Name=いちか などの値を送る

2. ルーティング
   /Practice/FavoriteをFavoriteページに対応させる

3. PageModelの生成
   新しいFavoriteModelインスタンスを作る

4. モデルバインディング
   POSTされた値をInputの各プロパティへ入れる

5. モデル検証
   [Required]、[StringLength]、[Range]を確認する
   エラーをModelStateへ記録する

6. ハンドラーメソッド
   POSTなのでOnPost()を呼ぶ

7. アプリの判断
   ModelState.IsValidを確認する

8. Razorによる表示
   return Page()でFavorite.cshtmlをHTMLへ変換する

9. ブラウザ
   サーバーから届いたHTMLを表示する
```

ポイントは、`4`と`5`が`OnPost()`の前だということです。

---

# MVCの勉強として見るとどうなる？

MVCは、アプリの役割を大きく3つへ分けて考える設計パターンです。

| MVCの役割 | 主な仕事 | 今回のおおよその対応 |
| --- | --- | --- |
| Model | データとルールを表す | `FavoriteInput`と検証属性 |
| View | 画面を表示する | `Favorite.cshtml` |
| Controller | リクエストを受けて処理を選ぶ | `FavoriteModel`の`OnGet()`、`OnPost()` |

ただし、`FavoriteModel`はMVCの`Controller`クラスそのものではありません。

Razor Pagesでは、ページ単位の`PageModel`が、Controllerに近いリクエスト処理の役割を持ちます。画面と処理をページごとに近く置けるのがRazor Pagesの特徴です。

名前に「Model」が2回登場するので注意してください。

| 名前 | 意味 |
| --- | --- |
| `FavoriteModel : PageModel` | Razor Page全体の処理と画面用データを持つクラス |
| `FavoriteInput` | ユーザーが入力するデータの形と検証ルール |
| cshtmlの`Model` | `@model`で指定された`FavoriteModel`のインスタンス |

```cshtml
@Model.Input.Title
```

これは次の順番でプロパティをたどっています。

```text
Model            → FavoriteModelのインスタンス
Model.Input      → FavoriteInputのインスタンス
Model.Input.Title → 入力されたタイトル
```

---

# セキュリティ：知らないうちに追加されるhidden項目

`<form method="post">`をRazor Pageで使うと、Form Tag Helperによって偽造防止用のトークンが自動生成されます。

ブラウザの開発者ツールでHTMLを調べると、おおよそ次のような非表示項目があります。

```html
<input name="__RequestVerificationToken"
       type="hidden"
       value="長い文字列" />
```

これはCSRF（クロスサイト・リクエスト・フォージェリ）という攻撃への対策です。

大まかには、「このアプリ自身が作ったフォームから送られたリクエストか」を確認するための合言葉です。

今回、この値を自分でC#へ書く必要はありません。Razor Pagesとタグヘルパーが処理します。

もう1つ大事な点があります。

```cshtml
<dd>@Model.Input.Reason</dd>
```

Razorの`@`で文字列を出力すると、通常はHTMLエンコードされます。利用者が`<script>`のような文字を入力しても、そのままスクリプトとして実行されにくい形へ変換されます。

入力値へ安易に`Html.Raw`を使わないでください。

---

# わざと壊して理解する実験

## 実験3：`method="post"`を`method="get"`に変える

予想すること：

1. ボタンを押したとき`OnGet()`と`OnPost()`のどちらが呼ばれるか
2. 入力値がURLに表示されるか
3. `[BindProperty]`だけで`Input`へ値が入るか

結果：

- `GET`になるため`OnGet()`が呼ばれます。
- フォーム値はクエリ文字列としてURLへ付きます。
- `[BindProperty]`は既定ではGETをバインドしないため、`Input`へは入りません。

GETでもプロパティへバインドしたい場合は`SupportsGet = true`がありますが、今回はPOSTフォームなので使いません。

確認後、必ず`method="post"`へ戻します。

## 実験4：`return Page()`を消す

`OnPost()`の戻り値が`IActionResult`なら、すべての処理経路で`IActionResult`を返す必要があります。

消すとコンパイルエラーになります。

これはC#の戻り値のルールです。Razor Pagesだけのルールではありません。

## 実験5：`ModelState.IsValid`の`if`を消す

空欄で送信しても`Submitted = true`まで進み、不完全な紹介カードが表示されます。

検証属性がエラーを記録することと、エラー時に処理を止めることは別です。

確認後、`if (!ModelState.IsValid)`を元に戻します。

## 実験6：チェックボックスの送信値を見る

ブラウザの開発者ツールで`Network`を開き、POSTリクエストのフォームデータを見ます。

チェックボックスでは`true`と`false`に関係する値が送られます。Input Tag Helperは、未チェック時も`bool`へ値を渡せるようにhidden項目も生成します。

「画面に書いたcshtml」と「ブラウザへ届いた最終HTML」が同じとは限らない、というタグヘルパーの働きを確認できます。

---

# 自力課題

## 課題1：発売年を追加する

次の仕様で`ReleaseYear`を追加してください。

- 型は`int`
- 表示名は「発売年」
- 1900年から現在の年まで
- フォームへ入力欄を追加する
- 紹介カードにも表示する

ヒント：

```csharp
[Range(1900, 2100, ErrorMessage = "発売年を正しく入力してください。")]
public int ReleaseYear { get; set; }
```

現在年を属性の引数へ直接入れることはできないため、この段階では上限を固定値にして構いません。

## 課題2：おすすめ度を星で表示する

`Rating`が`3`なら`★★★☆☆`と表示してください。

ヒント：

```csharp
new string('★', Model.Input.Rating)
```

空の星は次の個数です。

```csharp
5 - Model.Input.Rating
```

## 課題3：カテゴリーを1つ追加する

自分の好きなカテゴリーを`select`へ追加します。

HTMLの`option`がどのように`Input.Category`へ届くか、送信後のカードで確認してください。

## 課題4：独自のビジネスルールを追加する

「おすすめ理由にタイトルと同じ文字だけを書いた場合はエラー」にします。

`OnPost()`の`ModelState.IsValid`を確認する前に追加します。

```csharp
if (Input.Reason == Input.Title)
{
    ModelState.AddModelError(
        "Input.Reason",
        "タイトルとは別に、おすすめ理由を書いてください。");
}
```

`[Required]`などで表しにくい、そのアプリ独自の決まりを**ビジネスルール**と呼びます。

## 課題5：プレビュー用ハンドラーを追加する（発展）

「登録」ではなく「プレビュー」という別のPOST処理を作れます。

ボタンを追加します。

```cshtml
<button type="submit"
        asp-page-handler="Preview"
        class="btn btn-secondary">
    プレビュー
</button>
```

PageModelへ追加します。

```csharp
public IActionResult OnPostPreview()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    Submitted = true;
    SubmittedAt = DateTime.Now;

    return Page();
}
```

対応関係は次のとおりです。

```text
asp-page-handler="Preview"
        ↓
OnPostPreview()
```

`OnPost()`だけでなく、名前付きハンドラーを使うと、同じページに複数のPOST操作を用意できます。

---

# 確認問題

コードを見ずに、まず自分の言葉で答えてください。

1. ページを開いたとき、なぜ`OnGet()`が呼ばれますか？
2. 送信ボタンを押したとき、なぜ`OnPost()`が呼ばれますか？
3. `[BindProperty]`は何をしていますか？
4. モデルバインディングは`OnPost()`の前と後、どちらで行われますか？
5. `asp-for="Input.Name"`は、ブラウザ用のHTMLへ変換されると何を作りますか？
6. `FavoriteInput`はDBのテーブルですか？
7. `ModelState.IsValid`が`false`になる原因を2種類答えてください。
8. 検証エラー時に`return Page()`する理由は何ですか？
9. GET時とPOST時の`FavoriteModel`は同じインスタンスですか？
10. 今回のコードでMVCのViewに近いものは何ですか？
11. 今回のコードでMVCのControllerの責任に近いものは何ですか？
12. DBを使っていないのに、POST後に入力値を表示できるのはなぜですか？

---

# 確認問題の答え

1. ブラウザが`GET /Practice/Favorite`を送り、Razor PagesがHTTPメソッドに対応するハンドラー`OnGet()`を呼ぶからです。
2. `<form method="post">`によりブラウザがPOSTを送り、Razor Pagesが`OnPost()`を呼ぶからです。
3. HTTPリクエストの値を`PageModel`の`public`プロパティへモデルバインディングする対象にします。
4. `OnPost()`の前です。そのため`OnPost()`の最初から`Input`の値を使えます。
5. プロパティに対応する`id`、`name`、`type`、検証用属性などを持つHTML要素を生成します。
6. 違います。今回はフォーム入力をまとめる入力モデルです。
7. 文字列を`int`へ変換できないなどのモデルバインディングエラーと、`[Required]`や`[Range]`などへの違反です。
8. エラー情報と入力値を持った現在のページを再表示し、利用者に修正してもらうためです。
9. 別のインスタンスです。リクエストごとに新しく作られます。
10. `Favorite.cshtml`です。
11. `FavoriteModel`の`OnGet()`や`OnPost()`です。ただし`PageModel`はControllerクラスそのものではありません。
12. POSTリクエストの中に入力値があり、モデルバインディングで新しい`FavoriteModel.Input`へ入れ直されるからです。永続的に保存されているわけではありません。

---

# 用語集

| 用語 | 意味 |
| --- | --- |
| HTTPリクエスト | ブラウザからサーバーへのお願い |
| HTTPレスポンス | サーバーからブラウザへの返事 |
| GET | 主にページやデータを取得するHTTPメソッド |
| POST | 主に入力データをサーバーへ送るHTTPメソッド |
| フォーム | 利用者が値を入力して送信するHTMLの仕組み |
| ハンドラーメソッド | HTTPリクエストに対応してRazor Pagesが呼ぶ`OnGet()`や`OnPost()` |
| PageModel | Razor Pageの処理と画面用データを持つクラス |
| モデルバインディング | HTTPリクエストの値をC#のプロパティや引数へ入れる仕組み |
| `[BindProperty]` | `PageModel`のプロパティをモデルバインディング対象にする属性 |
| InputModel | フォームから受け取るデータをまとめたクラス |
| ViewModel | 画面の表示や入力に必要なデータをまとめたクラス |
| タグヘルパー | Razor上のHTML風の記述から、必要なHTMLを生成する仕組み |
| `asp-for` | HTML要素をC#のプロパティに対応させるタグヘルパー属性 |
| モデル検証 | 入力値が決められたルールを守っているか確認する仕組み |
| DataAnnotations | `[Required]`や`[Range]`などでデータのルールを表す仕組み |
| `ModelState` | モデルバインディングとモデル検証の結果・エラーを持つ |
| `IActionResult` | ページ表示やリダイレクトなど、リクエストへの結果を表す型 |
| `Page()` | 現在のRazor Pageを表示する結果 |
| MVC | Model、View、Controllerへ責任を分ける設計パターン |
| CSRF | 利用者の意図しないリクエストを別サイトから送らせる攻撃 |

---

# 今回できるようになったこと

- `GET`と`POST`の役割を区別できる
- `<form method="post">`でPOSTリクエストを送れる
- `OnPost()`が自動的に呼ばれる理由を説明できる
- `[BindProperty]`でフォーム値を受け取れる
- モデルバインディングの実行タイミングを説明できる
- `asp-for`でHTMLとC#プロパティを結び付けられる
- 入力専用Modelへ責任を分けられる
- DataAnnotationsで入力ルールを書ける
- `ModelState.IsValid`で処理を分岐できる
- Razor PagesとMVCの役割の対応を説明できる
- GETとPOSTでは`PageModel`が別インスタンスになると理解できる

---

# 今回、あえて使わなかったもの

- EF Core
- SQL Server
- `DbContext`
- DBへの保存
- スキャフォールディング
- `async`と`await`
- ファイルアップロード
- 認証とログイン
- PRG（Post-Redirect-Get）パターン
- `TempData`

今回の紹介カードはDBに保存されません。

次のチュートリアルでは、DBへ進む前に「POST後にリダイレクトする理由」と「`TempData`を使って1回だけメッセージを渡す方法」を学ぶと、Webアプリのリクエストの区切りがさらに分かりやすくなります。

---

# 参考にしたMicrosoft Learn

- [Razor Pagesのアーキテクチャと概念](https://learn.microsoft.com/ja-jp/aspnet/core/razor-pages/?view=aspnetcore-10.0)
- [ASP.NET Coreでのモデル バインド](https://learn.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-10.0)
- [ASP.NET Core MVCおよびRazor Pagesでのモデルの検証](https://learn.microsoft.com/ja-jp/aspnet/core/mvc/models/validation?view=aspnetcore-10.0)
- [ASP.NET Coreのフォームのタグ ヘルパー](https://learn.microsoft.com/ja-jp/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-10.0)
- [ASP.NET Core MVCの概要](https://learn.microsoft.com/ja-jp/aspnet/core/mvc/overview?view=aspnetcore-10.0)
- [ASP.NET CoreでCSRF攻撃を防止する](https://learn.microsoft.com/ja-jp/aspnet/core/security/anti-request-forgery?view=aspnetcore-10.0)
