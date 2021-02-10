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

namespace NetNews.Controllers
{
    public class AllNewsController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<AllNewsController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public AllNewsController(DBConnection context, ILogger<AllNewsController> logger, IOptions<SystemConfiguration> systemConfiguration, IWebHostEnvironment hostingEnvironment, 
            IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        // GET: All News Data
        public IActionResult Index([FromQuery(Name = "p")] string p = "1", [FromQuery(Name = "s")] string s = "10")
        {
            try
            {
                //set default pagination values
                ViewBag.PageNo = 1;
                ViewBag.PageSize = 10;
                if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(s))
                {
                    ViewBag.PageNo = Int32.Parse(p);
                    ViewBag.PageSize = Int32.Parse(s);
                }

                ViewBag.PageSkip = (ViewBag.PageNo - 1) * ViewBag.PageSize;
                int PageSkip = ViewBag.PageSkip;
                int PageSize = ViewBag.PageSize;

                //get records for past 3 months
                DateTime LatestNewsDateRange = DateTime.Now.AddDays(-90);
                ViewBag.TotalRecords = _context.vwPostsApproved.Where(s => s.ApprovalsDateAdded > LatestNewsDateRange).Count();

                var CategoryData = _context.vwPostsApproved.Where(s => s.ApprovalsDateAdded > LatestNewsDateRange).OrderByDescending(s => s.ApprovalsDateAdded).Skip(PageSkip).Take(PageSize).ToList();

                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP, _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here

                if (_systemConfiguration.logSearches)
                {
                    //log search
                    string StatType = "AllNews";
                    string ActionValue = "All News";
                    functions.LogSiteStat(StatType, ActionValue, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), OtherInfo);
                }

                //log visit
                string LogName = "AllNews";
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[2], LogName, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                //Set Meta SEO
                ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
                ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
                ViewBag.Title = "All News, " + functions.GetSiteLookupData("MetaTitle");

                return View(CategoryData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get All News Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }


    }
}