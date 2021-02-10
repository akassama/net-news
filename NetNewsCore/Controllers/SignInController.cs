using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppDataModels;
using NetNews.Models.AppModels;
using NetNews.Models.Email;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class SignInController : Controller
    {
        AppFunctions functions = new AppFunctions();
    
        private readonly DBConnection _context;
        private readonly ILogger<SignInController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public SignInController(DBConnection context, ILogger<SignInController> logger, SessionManager sessionManager, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _sessionManager = sessionManager;
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
        }


        public IActionResult Index()
        {
            //if already logged in, redirect home
            if (_sessionManager.IsLoggedIn)
            {
                return RedirectToAction("Index", "Admin");
            }

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[8], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            // Set Meta Data 
            ViewData["Title"] = "Sign In";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["PostAuthor"] = "";

            return View();
        }

        // POST: SignIn/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginModel)
        {
            // Set Meta Data 
            ViewData["Title"] = "Sign In";
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["PostAuthor"] = "";

            if (ModelState.IsValid)
            {
                try
                {
                    // check password
                    var query = _context.Accounts.Where(s => s.Email == loginModel.Email);
                    string hashedPassword = (query.Any()) ? query.FirstOrDefault().Password : "";
                    if (!string.IsNullOrEmpty(hashedPassword))
                    {
                        //if login is valid
                        if (BCrypt.Net.BCrypt.Verify(loginModel.Password, hashedPassword))
                        {
                            //If account not activated
                            if (query.FirstOrDefault().Active == 0)
                            {
                                TempData["ErrorMessage"] = "Account not activated.";
                                return View(loginModel);
                            }

                            //active account
                            else if (query.FirstOrDefault().Active == 1)
                            {
                                //Set sessions
                                _sessionManager.ID = query.FirstOrDefault().ID;
                                _sessionManager.LoginAccountId = query.FirstOrDefault().AccountID;
                                _sessionManager.LoginUsername = query.FirstOrDefault().Email.Split('@')[0];
                                _sessionManager.LoginEmail = query.FirstOrDefault().Email;
                                _sessionManager.LoginFirstName = (query.FirstOrDefault().FirstName != null) ? query.FirstOrDefault().FirstName : "";
                                _sessionManager.LoginLastName = (query.FirstOrDefault().LastName != null) ? query.FirstOrDefault().LastName : "";
                                _sessionManager.LoginDirectoryName = query.FirstOrDefault().DirectoryName;
                                if (query.FirstOrDefault().Oauth == 0 && query.FirstOrDefault().Oauth != null)
                                {
                                    //set profile pic to default if null
                                    _sessionManager.LoginProfilePicture = (query.FirstOrDefault().FirstName != null) ? "/files/images/accounts/" + _sessionManager.LoginDirectoryName + "/" + query.FirstOrDefault().ProfilePicture : "/files/images/defaults/default-profile.jpg";
                                }
                                else
                                {
                                    _sessionManager.LoginProfilePicture = "/files/images/defaults/default-profile.jpg";
                                }

                                //log activity
                                if (_systemConfiguration.logActivity && _systemConfiguration.logSignIn)
                                {
                                    string LogAction = $@"User '{_sessionManager.LoginFirstName} {_sessionManager.LoginLastName}' logged in.";
                                    functions.LogActivity(_sessionManager.LoginAccountId, _sessionManager.LoginAccountId, "UserLogin", LogAction);
                                }

                                return RedirectToAction("Index", "Admin");
                            }
                            //Undefined status
                            else
                            {
                                TempData["ErrorMessage"] = "Account status unknown. Please contact admin.";
                                return View(loginModel);
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Login failed. You have entered an invalid email or password";
                            return View(loginModel);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Login failed. You have entered an invalid email or password";
                        return View(loginModel);
                    }
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Login Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to " + functions.GetSiteLookupData("SupportEmail");
                    return View(loginModel);
                }
            }
            TempData["ErrorMessage"] = "Login failed";
            return View(loginModel);
        }




        // POST: SignIn/ResetPossword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPossword()
        {

            string ForgotEmail = HttpContext.Request.Form["ForgotPasswordEmail"];
            string[] ValidationInputs = { ForgotEmail };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Email required.";

                return RedirectToAction("Index", "SignIn");
            }

            if (!_context.Accounts.Any(s => s.Email == ForgotEmail))
            {
                TempData["ErrorMessage"] = "The email provided does not exist in our records.";

                return RedirectToAction("Index", "SignIn");
            }

            string AccountID = _context.Accounts.Where(s => s.Email == ForgotEmail).FirstOrDefault().AccountID;

            try
            {
                //remove other user reset data if exists
                functions.DeleteTableData("PasswordForgot", "AccountID", AccountID, _systemConfiguration.connectionString);

                //add reset data
                string ResetID = functions.RandomString(120);
                functions.AddForgotPassword(ResetID, AccountID);

                //send user email
                //set email data
                string ToName = functions.GetAccountData(AccountID, "FullName");
                string[] MessageParagraphs = { "Hello, ", "We've received a request to reset the password for your account. No changes have been made to your account yet. You can reset your password by clicking the link below: ", "If you did not request a new password, please let us know immediately by replying to this email." };
                string PreHeader = "Account password reset link notification.";
                bool Button = true;
                int ButtonPosition = 2;
                string ButtonLink = functions.GetSiteLookupData("AppDomain") + "/PasswordReset/?id="+ ResetID;
                string ButtonLinkText = "Reset Password";
                string Closure = _systemConfiguration.emailClosure;
                string Company = _systemConfiguration.emailCompany;
                string UnsubscribeLink = _systemConfiguration.emailUnsubscribeLink;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = _systemConfiguration.smtpEmail;
                string ToEmail = ForgotEmail;
                string Subject = "Password Reset Email";

                //Get smtp details
                string smtpEmail = _systemConfiguration.smtpEmail;
                string smtpPass = _systemConfiguration.smtpPass;
                string displayName = _systemConfiguration.emailDisplayName;
                string smtpHost = _systemConfiguration.smtpHost;
                int smtpPort = _systemConfiguration.smtpPort;

                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtpEmail, smtpPass, displayName, smtpHost, smtpPort);


                TempData["SuccessMessage"] = @"The email with further instructions was sent to the submitted email address. If you don’t receive a message in 5 minutes, " +
                    "check the junk folder. If you are still experiencing any problems, contact support at "+ functions.GetSiteLookupData("SupportEmail");

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{ToName}' did password reset.";
                    functions.LogActivity(ToEmail, ToEmail, "PasswordReset", LogAction);
                }

                return RedirectToAction("Index", "SignIn");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Password Reset Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("Index", "SignIn");
            }
        }

    }
}