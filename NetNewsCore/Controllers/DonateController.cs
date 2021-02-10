using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;

namespace NetNews.Controllers
{
    public class DonateController : Controller
    {
        AppFunctions functions = new AppFunctions();
        private readonly DBConnection _context;
        private readonly ILogger<DonateController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        public DonateController(DBConnection context, ILogger<DonateController> logger, IOptions<SystemConfiguration> systemConfiguration)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
        }

        public IActionResult Index()
        {
            if (!_systemConfiguration.showDonateLink)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ShowDonate = (_systemConfiguration.showDonateLink) ? "true" : "false";

            //Set Meta SEO
            ViewData["ContentDescription"] = functions.GetSiteLookupData("SiteName") + "Donate Page";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");

            return View();
        }
    }
}