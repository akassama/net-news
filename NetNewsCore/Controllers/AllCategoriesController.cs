using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNewsCore.Controllers
{
    public class AllCategoriesController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AppValidations validations = new AppValidations();

        private readonly DBConnection _context;
        private readonly ILogger<AllCategoriesController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public AllCategoriesController(DBConnection context, ILogger<AllCategoriesController> logger, IOptions<SystemConfiguration> systemConfiguration,
            SessionManager sessionManager, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _context = context;
            _logger = logger;
            _sessionManager = sessionManager;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            try
            {
                //log visit
                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP, _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here
                string CategoryName = "All Categories";
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[2], CategoryName, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                ViewData["Title"] = CategoryName;
                ViewData["ContentDescription"] = functions.GetSiteLookupData("SiteName") + " " + CategoryName;
                ViewData["ContentKeywords"] = CategoryName;

                var data = _context.Categories.OrderBy(s => s.CategoryOrder);

                return View(data);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get All Categories Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");

        }
    }
}