using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using NetNews.Models.Email;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class CelebritiesController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<CelebritiesController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public CelebritiesController(DBConnection context, ILogger<CelebritiesController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            ViewBag.ShowDonate = (_systemConfiguration.showDonateLink) ? "true" : "false";
            ViewBag.TotalVideoGallery = _context.vwPostsApproved.Count(s => s.PostType == "Gallery" || s.PostType == "Video");
            ViewBag.MorePosts = _context.vwPostsApproved.Skip(12).Count();
            ViewBag.WebsiteName = functions.GetSiteLookupData("SiteName");
            ViewBag.WeatherWidgetLink = _systemConfiguration.weatherLocationUrl;
            ViewBag.WeatherWidgetLocation = _systemConfiguration.weatherLocationText;
            ViewBag.ForexWidgetLink = _systemConfiguration.forexWidgetUrl;
            ViewBag.CovidWidgetClassId = _systemConfiguration.covidWidgetClassId;

            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewBag.Title = functions.GetSiteLookupData("MetaTitle");

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[0], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
