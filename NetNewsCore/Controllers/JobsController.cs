using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.TagHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class JobsController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<JobsController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public JobsController(DBConnection context, ILogger<JobsController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
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
            DateTime TodaysDate = DateTime.Now;
            var data = _context.Jobs.Where(s => s.ExpiryDate >= TodaysDate);

            //Set Meta SEO
            ViewData["ContentDescription"] = "Find jobs in The Gambia";
            ViewData["ContentKeywords"] = "Jobs, Gambia Jobs, Find jobs in Gambia, Employment, Employment in Gambia";

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[6], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            return View(data);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Jobs");
            }

            try
            {
                var jobsModel = _context.Jobs
               .FirstOrDefault(m => m.JobAdvertPermalink == id);
                if (jobsModel == null)
                {
                    return NotFound();
                }

                //log visit
                string JobTitle = _context.Jobs.Where(s => s.JobAdvertPermalink == id).FirstOrDefault().JobTitle;
                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[7], JobTitle, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                return View(jobsModel);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Job Details Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }



        //ovveride NotFound() to E404 error page
        public new IActionResult NotFound()
        {
            return RedirectToAction("E404", "Error");
        }

    }
}