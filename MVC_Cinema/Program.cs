using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<ConfigStructure>(builder.Configuration);

builder.Services.AddControllersWithViews();

var config = builder.Configuration.Get<ConfigStructure>();
var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(config?.ConnectionString, serverVersion));

builder.Services.AddRazorPages();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();

builder.Services.AddRepository();

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<AdminService>();

builder.Services.AddTmdbRepository(config?.ApiKey!);

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
app.MapRazorPages();

app.Run();