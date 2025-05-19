using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObiletCase.Middleware
{
    public class ExceptionHandler : FilterAttribute, IExceptionFilter
    {
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