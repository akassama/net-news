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
    public class SearchController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AppValidations validations = new AppValidations();

        private readonly DBConnection _context;
        private readonly ILogger<SearchController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public SearchController(DBConnection context, ILogger<SearchController> logger, IOptions<SystemConfiguration> systemConfiguration,
            SessionManager sessionManager, IWebHostEnvironment hostingEnvironment, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _context = context;
            _logger = logger;
            _sessionManager = sessionManager;
            _systemConfiguration = systemConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
            _detectionService = detectionService;
            _accessor = accessor;
        }


        public IActionResult Index([FromQuery(Name = "p")] string p = "1", [FromQuery(Name = "s")] string s = "10", [FromQuery(Name = "q")] string q = null)
        {
            if (string.IsNullOrEmpty(q) || q == "Index")
            {
                return RedirectToAction("Index", "Home");
            }

            if (q.Length < 2)
            {
                TempData["ErrorMessage"] = "Query too short...";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                ViewBag.SearchValue = q;

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
                ViewBag.TotalRecords = _context.vwPostsApproved.Where(s => s.PostTitle.Contains(q) || s.PostExtract.Contains(q) || s.PostContent.Contains(q) || s.PostTags.Contains(q)).Count();

                var SearchData = _context.vwPostsApproved.Where(s => s.PostTitle.Contains(q) || s.PostExtract.Contains(q) || s.PostContent.Contains(q) || s.PostTags.Contains(q)).OrderByDescending(s => s.ApprovalsDateAdded).Skip(PageSkip).Take(PageSize).ToList();

                if (_systemConfiguration.logSearches)
                {
                    //log search
                    string StatType = "Search";
                    string ActionValue = q;
                    string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                    string OtherInfo = null; //add any other info here
                    functions.LogSiteStat(StatType, ActionValue, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), OtherInfo);
                }

                //Set Meta SEO
                ViewData["Title"] = "Search results for " + q;
                ViewData["ContentDescription"] = "Search results for " + q;
                ViewData["ContentKeywords"] = q + "," + functions.GetSiteLookupData("MetaKeywords");

                return View(SearchData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Search Details Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }


    }
}