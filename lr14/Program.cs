using lr_fourteen.Services;
using Microsoft.AspNetCore.Http;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341") 
    .CreateLogger();

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService("LR14")
                .AddAttributes(new Dictionary<string, object>
                {
                    ["environment.name"] = "Microsoft Visual Sutdio",
                    ["team.name"] = "ASP.NET Laboratory",
                    ["laboratory.number"] = "14",
                    ["some.example"] = "EXAMPLE ATTR"
                }))
        .AddConsoleExporter();  
});


builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService("LR14"))
      .WithTracing(tracing => tracing
          .SetSampler(new TraceIdRatioBasedSampler(0.25))
          .AddAspNetCoreInstrumentation(options =>
          {
              options.Filter = httpContext =>
              {
                  var filterOne = httpContext.Request.Method.ToString() == "GET";
                  var filterTwo = httpContext.Request.Path.Value.Contains("example");
                  var filterThree = httpContext.Response.StatusCode == 200;
                  return filterOne && filterTwo && filterThree;
              };
          })
          .AddHttpClientInstrumentation()
          .AddProcessor(new BatchActivityExportProcessor(new SeqTraceExporter()))
          .AddOtlpExporter(options =>
          {
              options.Endpoint = new Uri("http://localhost:4317");
              options.TimeoutMilliseconds = 30000;
          })
          .AddConsoleExporter())
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddReader(new PeriodicExportingMetricReader(new SeqMetricExporter()))
          .AddOtlpExporter(options =>
          {
              options.Endpoint = new Uri("http://localhost:4317");
              options.TimeoutMilliseconds = 30000;
          }));

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var MyActivitySource = new ActivitySource("MyActivitySource");

app.MapGet("/example/{id?}", async (string? id, HttpContext context) =>
{
    var activity = Activity.Current;

    if (activity != null)
    {
        activity.SetTag("custom.tag", "SOME CUSTOM TAG");
        activity.SetTag("rolf.tag", "SOME ROFL TAG");
        activity.SetTag("laboratory.number.tag", "14");
        activity.SetTag("year.tag", "2024");

        if (!string.IsNullOrEmpty(id))
        {
            activity.SetTag("path.param", $"{id}");
        }

    }

    await context.Response.WriteAsync("Example");
});

app.Run();
