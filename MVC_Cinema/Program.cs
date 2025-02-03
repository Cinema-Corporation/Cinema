using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Data;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

var rootPath = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
var filePath = Path.Combine(rootPath!, "Infrastructure", "Data", "config.json");
var json = File.ReadAllText(filePath);
var config = JsonConvert.DeserializeObject<ConfigStructure>(json) ?? throw new InvalidDataException("Config deserialization failed.");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(config.ConnectionString!, serverVersion));

builder.Services.AddRepository();

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddTmdbRepository(config.ApiKey);

builder.Services.AddAutoMapper();

builder.Services.AddValidators();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
