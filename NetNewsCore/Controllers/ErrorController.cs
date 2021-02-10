using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetNews.Models.AppModels;

namespace NetNews.Controllers
{
    public class ErrorController : Controller
    {
        private readonly SystemConfiguration _systemConfiguration;
        AppFunctions functions = new AppFunctions();
        public ErrorController(IOptions<SystemConfiguration> systemConfiguration)
        {
            _systemConfiguration = systemConfiguration.Value;
        }
        public IActionResult E404()
        {
            // Set Meta Data 
            ViewData["Title"] = "Page Not Found";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");

            return View();
        }

        public IActionResult E400()
        {
            // Set Meta Data 
            ViewData["Title"] = "Request Error";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");

            return View();
        }
    }
}