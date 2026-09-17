using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.");

builder.Services.AddDbContext<MvcMovieContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// ルーティングの形式はここで指定されている
// `id?`の?は省略可能ってことらしい。IDは指定しなくてもOK.
// TODO: HomeとIndexは何のために書いてあるの？消しても何も変わらなかった、、、
// `controller=Ichika`とか`action=Index`の右側を変えたら、デフォルトのControllerとメソッドを変更できる。
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HelloWorld}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
