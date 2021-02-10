using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using NetNews.Models.PostsDataModel;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class CategoryController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AppValidations validations = new AppValidations();

        private readonly DBConnection _context;
        private readonly ILogger<CategoryController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public CategoryController(DBConnection context, ILogger<CategoryController> logger, IOptions<SystemConfiguration> systemConfiguration,
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

        // GET: Category Data
        public async Task<IActionResult> Index(string id, [FromQuery(Name = "p")] string p = "1", [FromQuery(Name = "s")] string s = "10")
        {
            if (string.IsNullOrEmpty(id) || id== "Index")
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                ViewBag.Category = id;
                string ShortCategoryName = functions.ConvertCase(id, "TitleTrim");
                var DBQuery = _context.Categories.Where(s => s.ShortCategoryName == ShortCategoryName);
                //if category not found
                if (!DBQuery.Any())
                {
                    TempData["ErrorMessage"] = "Category not found";
                    return RedirectToAction("Index", "Home");
                }
                string CategoryID = DBQuery.FirstOrDefault().CategoryID;
                ViewBag.CategoryID = CategoryID;

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
                ViewBag.TotalRecords = _context.vwPostsApproved.Where(s => s.PostCategory == CategoryID).Count();

                var CategoryData = _context.vwPostsApproved.Where(s => s.PostCategory == CategoryID).OrderByDescending(s => s.ApprovalsDateAdded).Skip(PageSkip).Take(PageSize).ToListAsync();

                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP, _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here

                if (_systemConfiguration.logSearches)
                {
                    //log category
                    string StatType = "Category";
                    string ActionValue = CategoryID;
                    functions.LogSiteStat(StatType, ActionValue, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), OtherInfo);
                }

                //log visit
                string CategoryName = _context.Categories.Where(s => s.CategoryID == CategoryID).FirstOrDefault().CategoryName;
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[2], CategoryName, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                ViewData["Title"] = functions.ConvertCase(id, "SplitUpper");
                ViewData["ContentDescription"] = functions.GetSiteLookupData("SiteName") + " " + id + " Category";
                ViewData["ContentKeywords"] = id;

                return View(await CategoryData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Category Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
