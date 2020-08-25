using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models
{
    public class SessionAuthorize : ActionFilterAttribute
    {
        //Injecting SessionManager in class
        private readonly SessionManager _sessionManager;
        public SessionAuthorize(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!_sessionManager.IsLoggedIn)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "SignIn" },
                                { "Action", "Index" }
                                });
            }
        }
    }


    public class SessionAuthorizeAdmin : ActionFilterAttribute
    {
        //Injecting SessionManager in class
        private readonly SessionManager _sessionManager;
        public SessionAuthorizeAdmin(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!_sessionManager.IsLoggedIn || _sessionManager.IsLoggedIn)
            {
                //check if user is admin
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "Admin" },
                                { "Action", "Index" }
                                });
            }
        }
    }
}
