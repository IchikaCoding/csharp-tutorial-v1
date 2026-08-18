using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.");

// スキャフォールディングによって生成された部分。
// DBとのアクセスしたりする実行環境の用意のコード？
builder.Services.AddDbContext<RazorPagesMovieContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// ミドルウェアを有効にする処理たち
app.UseHttpsRedirection();

app.UseRouting();
// 今回のアプリではこれ不要らしい
app.UseAuthorization();
// HTTPとかCSSとかの静的アセットの配信を最適化？
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
