using lr_fifteen.Currency;
using lr_fifteen.DB;
using lr_fifteen.Services;
using lr_fifteen.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<BackgroundCheckService>();
builder.Services.AddSingleton<EmailScheduler>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<IScheduler>(sp => sp.GetRequiredService<ISchedulerFactory>().GetScheduler().Result);
builder.Services.AddScoped<DBEmailService>();

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/some_logs.log").CreateLogger();

builder.Services.AddHostedService<BackgroundCheckService>();
builder.Services.AddHostedService<CurrencyService>();
builder.Services.AddHostedService<ChatService>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

var email = app.Services.GetRequiredService<EmailScheduler>();
await email.Start();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatUrl");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CheckUrl}/{action=Index}/{id?}");

app.Run();
