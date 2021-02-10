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
    public class SitemapController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<SitemapController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public SitemapController(DBConnection context, ILogger<SitemapController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
        }


        public IActionResult Index()
        {
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewBag.CategoriesData = _context.Categories.Where(s=> s.IsPublished == 1).OrderBy(s => s.CategoryName);

            return View();
        }
    }
}