using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NetNews.Models.AppModels;
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
            string url = filterContext.HttpContext.Request.Headers["Referer"].ToString(); 

            AppFunctions functions = new AppFunctions();
            string newUrl = functions.Url(filterContext.HttpContext.Request);
            if (newUrl.Contains("CheckExistingPassword") && !url.Contains("CheckExistingPassword"))
            {
                newUrl = url;
            }

            if (!_sessionManager.IsLoggedIn)
            {

                _sessionManager.LastAccessedUrl = newUrl;

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "SignIn" },
                                { "Action", "Index" }
                                });
            }
        }
    }

    public class AccessControlFilter : ActionFilterAttribute
    {
        //Properties in Action Filter
        public string PermissionName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string AccountID = filterContext.HttpContext.Session.GetString("_AccountId");

            if (string.IsNullOrEmpty(AccountID) || string.IsNullOrEmpty(PermissionName))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "Account" },
                                { "Action", "Index" }
                                });
            }

            using(var db = new DBConnection())
            {
                //get permission id from permission name
                int PermissionID = 0;
                if (db.Permissions.Any(s => s.PermissionName == PermissionName))
                {
                    PermissionID = db.Permissions.Where(s => s.PermissionName == PermissionName).FirstOrDefault().PermissionID;
                }

                //check if user has permission
                if (!db.AccountToPermission.Any(s => s.AccountID == AccountID && s.PermissionID == PermissionID))
                {

                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "Admin" },
                                { "Action", "Index" }
                                });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
