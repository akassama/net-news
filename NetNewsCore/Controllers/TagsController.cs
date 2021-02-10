using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class TagsController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AppValidations validations = new AppValidations();

        private readonly DBConnection _context;
        private readonly ILogger<TagsController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public TagsController(DBConnection context, ILogger<TagsController> logger, IOptions<SystemConfiguration> systemConfiguration,
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

        public async Task<IActionResult> Index(string id, [FromQuery(Name = "p")] string p = "1", [FromQuery(Name = "s")] string s = "10")
        {
            if (string.IsNullOrEmpty(id) || id == "Index")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                ViewBag.Tag = id;

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
                ViewBag.TotalRecords = _context.vwPostsApproved.Where(s => s.PostTitle.Contains(id) || s.PostExtract.Contains(id) || s.PostTags.Contains(id)).Count();

                var TagsData = _context.vwPostsApproved.Where(s => s.PostTitle.Contains(id) || s.PostExtract.Contains(id) || s.PostTags.Contains(id)).OrderByDescending(s => s.ApprovalsDateAdded).Skip(PageSkip).Take(PageSize).ToListAsync();

                if (_systemConfiguration.logSearches)
                {
                    //log search
                    string StatType = "Tag";
                    string ActionValue = id;
                    string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                    string OtherInfo = null; //add any other info here
                    functions.LogSiteStat(StatType, ActionValue, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), OtherInfo);
                }

                //Set Meta SEO
                ViewData["Title"] = "Results for tag: " + id;
                ViewData["ContentDescription"] = "Showing results for tag: " + id;
                ViewData["ContentKeywords"] = id + "," + functions.GetSiteLookupData("MetaKeywords");

                return View(await TagsData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Tag Details Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}