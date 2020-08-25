using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetNews.Models;
using NetNews.Models.AppDataModels;
using NetNews.Models.AppModels;

namespace NetNews.Controllers
{
    public class SignInController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<SignInController> _logger;
        private readonly SessionManager _sessionManager;

        public SignInController(DBConnection context, ILogger<SignInController> logger, SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        // POST: SignIn/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // check a password
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

                                //TODO set last login data

                                return RedirectToAction("Index", "Admin");
                            }
                            //Undefined status
                            else
                            {
                                TempData["ErrorMessage"] = "Account status unknown. Please contact admin.";
                                return View(loginModel);
                            }

                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Login failed. You have entered an invalid email or password";
                    }
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Login Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                    return View(loginModel);
                }
            }
            TempData["ErrorMessage"] = "Login failed";
            return View(loginModel);
        }

    }
}