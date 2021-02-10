using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AccountsDataModel;
using NetNews.Models.AppModels;
using NetNews.Models.Email;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class SignUpController : Controller
    {

        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<SignUpController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public SignUpController(DBConnection context, ILogger<SignUpController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        // GET: SignUp
        public IActionResult Index()
        {
            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[9], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            // Set Meta Data 
            ViewData["Title"] = "Sign Up";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["PostAuthor"] = "";

            return View();
        }

        // POST: SignUp/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AccountsModel accountsModel)
        {
            // Set Meta Data 
            ViewData["Title"] = "Sign Up";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["PostAuthor"] = "";

            if (ModelState.IsValid)
            {
                //verify password match
                string ConfirmPassword = Request.Form["ConfirmPassword"];
                if(!functions.PasswordsMatch(accountsModel.Password, ConfirmPassword))
                {
                    TempData["ErrorMessage"] = "Passwords do not match";
                    return View(accountsModel);
                }

                //verify email does not exist
                if (_context.Accounts.Any(s => s.Email == accountsModel.Email))
                {
                    TempData["ErrorMessage"] = "Email already exists, please choose a different email";
                    return View(accountsModel);
                }

                try
                {
                    //set registration default values
                    accountsModel.AccountID = functions.GetGuid();
                    accountsModel.DirectoryName = functions.GenerateDirectoryName(accountsModel.Email);
                    accountsModel.Active = 0;
                    accountsModel.Oauth = 0;
                    accountsModel.EmailVerification = 0;
                    accountsModel.UpdatedBy = accountsModel.AccountID;
                    accountsModel.UpdateDate = DateTime.Now;
                    accountsModel.DateAdded = DateTime.Now;

                    //hashing password with BCrypt
                    accountsModel.Password = BCrypt.Net.BCrypt.HashPassword(accountsModel.Password);

                    _context.Add(accountsModel);
                    await _context.SaveChangesAsync();

                    //add account id to account details
                    if(!_context.AccountDetails.Any(s=> s.AccountID == accountsModel.AccountID))
                    {
                        functions.AddTableData("AccountDetails", "AccountID", accountsModel.AccountID, _systemConfiguration.connectionString);
                    }

                    //send user email
                    //set email data
                    string ToName = functions.GetAccountData(accountsModel.AccountID, "FullName");
                    string[] MessageParagraphs = { "Hello " + ToName + ", ", "Thank you for registering to "+functions.GetSiteLookupData("SiteName") + ".", "Your registration would be reviewed by our team and you would be notified once approved.", "This may take up to 24 hours." };
                    string PreHeader = "New account registration notification.";
                    bool Button = false;
                    int ButtonPosition = 2;
                    string ButtonLink = null;
                    string ButtonLinkText = null;
                    string Closure = _systemConfiguration.emailClosure;
                    string Company = _systemConfiguration.emailCompany;
                    string UnsubscribeLink = _systemConfiguration.emailUnsubscribeLink;
                    string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                    string FromEmail = _systemConfiguration.smtpEmail;
                    string ToEmail = accountsModel.Email;
                    string Subject = "Account Registration Email";

                    //Get smtp details
                    string smtpEmail = _systemConfiguration.smtpEmail;
                    string smtpPass = _systemConfiguration.smtpPass;
                    string displayName = _systemConfiguration.emailDisplayName;
                    string smtpHost = _systemConfiguration.smtpHost;
                    int smtpPort = _systemConfiguration.smtpPort;

                    EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtpEmail, smtpPass, displayName, smtpHost, smtpPort);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{ToName}' registered.";
                        functions.LogActivity(accountsModel.AccountID, accountsModel.AccountID, "NewRegistration", LogAction);
                    }


                    TempData["SuccessMessage"] = "Thank you for registering. Your registration would be reviewed by our team and you would be notified once approved. This may take up to 24 hours.";
                    return RedirectToAction("Index", "SignIn");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Sign Up Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                }
            }
            return View(accountsModel);
        }


        //Checks if user email already exists
        public ActionResult CheckUniqueEmail(string key)
        {
            //check in db and return "exists" if record exists
            if (_context.Accounts.Where(s => s.Email == key).Any())
            {
                return Json("exists");
            }
            return Json("");
        }

        private bool AccountsModelExists(int id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }

    }
}
