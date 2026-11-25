using SchoolLibrary.Data.Repositories;
using SchoolLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Включваме контролерите и изгледите (MVC)
builder.Services.AddControllersWithViews();

// 2. Регистрираме нашите класове
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton(sp => new BookService(sp.GetRequiredService<IBookRepository>()));

var app = builder.Build();

// 3. Казваме кои адреси към кой контролер водят
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

// 4. Стартираме сървъра
app.Run();