using lr_one;
using lr_one.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Company company = new Company();

company.Name = "UBISOFT";
company.Year = 1986;
company.Countries = [ "Germany", "USA","Ukraine", "French"];
company.Staff = 500000;

app.UseMiddleware<MiddlewareLogic>();
app.Run(async (context) => await context.Response.WriteAsync($"{company}"));
app.Run();
