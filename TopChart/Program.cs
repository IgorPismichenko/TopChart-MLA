using Microsoft.EntityFrameworkCore;
using TopChart_BLL.Infrastructure;
using TopChart_BLL.Interfaces;
using TopChart_BLL.Services;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTopChartMLAContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddTransient<ICommentsService, CommentsService>();
builder.Services.AddTransient<ICommentsVideoService, CommentsVideoService>();
builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<ISingersService, SingersService>();
builder.Services.AddTransient<ITracksService, TracksService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IVideoService, VideoService>();
var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
