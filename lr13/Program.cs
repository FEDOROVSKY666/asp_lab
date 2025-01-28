using Serilog;
using Serilog.Exceptions;
using System.Net;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Log.Logger = new LoggerConfiguration().
    Enrich.WithExceptionDetails().
    MinimumLevel.Debug().
    WriteTo.Console().
    WriteTo.Email(
        from: "lrThirteenSerilog@gmail.com", 
        to: "blinkotukpomerplak@gmail.com",
        subject: "Serilog Log", 
        host: "smtp.gmail.com",
        port: 587,
        connectionSecurity: MailKit.Security.SecureSocketOptions.StartTls,
        credentials: new NetworkCredential("lrThirteenSerilog@gmail.com", "gtqf rtqb cwwc hnwc")).
    WriteTo.Seq("http://localhost:5341").
    WriteTo.File("./Logs/logs1.txt").
    CreateLogger();

//builder.Host.UseSerilog();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Logger}/{action=Index}/{id?}");

app.Run();
Log.CloseAndFlush();
