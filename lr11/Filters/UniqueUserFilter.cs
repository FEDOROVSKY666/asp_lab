using Microsoft.AspNetCore.Mvc.Filters;

namespace lr_evelen.Filters
{
    public class UniqueUserFilter : IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("UserSession"))){
                string sessionId = Guid.NewGuid().ToString();
                string date = DateTime.Now.ToString();
                string result = $"User id: {sessionId} | Time: {date}\n";

                string path = "uniqueUsers";

                context.HttpContext.Session.SetString("UserSession", sessionId);
                File.AppendAllTextAsync(path, result);
            }
        }
    }
}
