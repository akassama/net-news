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
using NetNews.Models.Email;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class ContactController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<ContactController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public ContactController(DBConnection context, ILogger<ContactController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        public IActionResult Index([FromQuery(Name = "subject")] string subject)
        {
            ViewBag.Subject = subject;

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[3], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            //Set Meta SEO
            ViewData["Title"] = functions.GetSiteLookupData("SiteName") + "Contact Us";
            ViewData["ContentDescription"] = functions.GetSiteLookupData("SiteName") + "Contact Us Page";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendContact()
        {
            try
            {
                string Subject = HttpContext.Request.Form["Subject"];
                string Message = HttpContext.Request.Form["Message"];
                string Name = HttpContext.Request.Form["Name"];
                string Email = HttpContext.Request.Form["Email"];

                string[] ValidationInputs = { Message, Name, Email };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return RedirectToAction("Index", "Contact");
                }

                //send email 
                string ToName = _systemConfiguration.emailCompany;
                string[] MessageParagraphs = { "Contact message from " + Name + "<br/>", Message };
                string PreHeader = "New contact message";
                bool Button = false;
                int ButtonPosition = 0;
                string ButtonLink = null;
                string ButtonLinkText = null;
                string Closure = _systemConfiguration.emailClosure;
                string Company = _systemConfiguration.emailCompany;
                string UnsubscribeLink = null;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = _systemConfiguration.smtpEmail;
                string ToEmail = functions.GetSiteLookupData("SupportEmail");
                if (string.IsNullOrEmpty(Subject))
                {
                    Subject = "New Contact Message";
                }
                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = $"Thank you for getting in touch! We appreciate you contacting {_systemConfiguration.emailCompany}. <br/> One of our colleagues will get back in touch with you soon! <br/><br/> Have a great day!";
                return RedirectToAction("Index", "Contact");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Send Contact Message Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("Index", "Contact");
            }
        }
    }
}