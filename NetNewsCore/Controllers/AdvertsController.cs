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

namespace NetNews.Controllers
{
    public class AdvertsController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<AdvertsController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;
        //, SessionManager sessionManager
        //_sessionManager = sessionManager;

        public AdvertsController(DBConnection context, ILogger<AdvertsController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        public IActionResult Index(string id)
        {
            DateTime TodaysDate = DateTime.Now;

            try
            {
                var data = _context.Adverts.Where(s => s.ExpiryDate >= TodaysDate).OrderByDescending(x => Guid.NewGuid()).Take(1);//get random advert  
                if (!string.IsNullOrEmpty(id))
                {
                    data = _context.Adverts.Where(s => s.AdvertPermalink == id);
                }

                //log visit
                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[4], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                return View(data);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Adverts Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}