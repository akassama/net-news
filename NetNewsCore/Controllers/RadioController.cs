using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNewsCore.Controllers
{
    public class RadioController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<RadioController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;
        public RadioController(DBConnection context, ILogger<RadioController> logger, IOptions<SystemConfiguration> systemConfiguration, 
            IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
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
            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP, _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            string PageName = "Radio Stations";
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[2], PageName, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            ViewData["Title"] = "Radio Stations";
            ViewData["ContentDescription"] = functions.GetSiteLookupData("SiteName");
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");

            return View();
        }
    }
}