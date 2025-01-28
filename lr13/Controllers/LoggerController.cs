using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace lr_thirteen.Controllers
{
    public class LoggerController: Controller
    {
        private readonly ILogger<LoggerController> _logger;
        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }
        public IActionResult DefaultLogger()
        {
            _logger.LogInformation("\n=== Default Logger \"LogInformation\" ===\n");
            _logger.LogDebug("\n=== Default Logger \"LogDebug\" ===\n");
            _logger.LogWarning("\n=== Default Logger \"LogWarning\" ===\n");
            _logger.LogError("\n=== Default Logger \"LogError\" ===\n");
            _logger.LogCritical("\n=== Default Logger \"LogCritical\" ===\n");
            return RedirectToAction("Index");
        }
        public IActionResult SeriLogLogger()
        {
            Log.Information("\n=== SeriLog Logger \"Log.Information\" ===\n");
            Log.Warning("\n=== SeriLog Logger \"Log.Warning\" ===\n");
            Log.Error("\n=== SeriLog Logger \"Log.Error\" ===\n");
            Log.Debug("\n=== SeriLog Logger \"Log.Debug\" ===\n");
            Log.Fatal("\n=== SeriLog Logger \"Log.Fatal\" ===\n");
            return RedirectToAction("Index");
        }
        public IActionResult SeriLogCustom()
        {
            var user = new { Name = "Sasha", Role = "Guest", IsAdmin = false };
            Log.Information("User {Name} logged in at: {Time}", user.Name, DateTime.Now);
            Log.Warning("User {Name} has the role: {Role}", user.Name, user.Role); ;
            Log.Information("User {Name} is a superuser - {Role}", user.Name, user.IsAdmin);
            Log.Debug("{@User}", user);
            Log.Fatal("Some User {Name} actions caused a fatal error at: {Time}", user.Name, DateTime.Now);
            return RedirectToAction("Index");
        }
        public IActionResult SeriLogException()
        {
            try
            {
                throw new InvalidOperationException("This is a test exception.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Some error!");
                return RedirectToAction("Index");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
