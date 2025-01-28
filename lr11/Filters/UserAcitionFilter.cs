using Microsoft.AspNetCore.Mvc.Filters;

namespace lr_evelen.Filters
{
    public class UserAcitionFilter: IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string dateNow = DateTime.Now.ToString();
            string result = $"Action Name: {actionName} | Time: {dateNow}\n";

            string path = "someFile.txt";

            if (!string.IsNullOrEmpty(actionName))
            {
                File.AppendAllTextAsync(path, result);
            }
        }
    }
}
