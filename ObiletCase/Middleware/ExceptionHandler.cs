using System.Web.Mvc;

namespace ObiletCase.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandler : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            filterContext.Controller.TempData["ErrorMessage"] = filterContext.Exception.Message;

            filterContext.Result = new RedirectToRouteResult(
            new System.Web.Routing.RouteValueDictionary {
                { "controller", "Home" },
                { "action", "Error" }
            });

            filterContext.ExceptionHandled = true;

        }
    }
}