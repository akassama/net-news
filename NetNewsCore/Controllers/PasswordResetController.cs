using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;

namespace NetNews.Controllers
{
    public class PasswordResetController : Controller
    {
        AppFunctions functions = new AppFunctions();
        private readonly DBConnection _context;
        private readonly ILogger<PasswordResetController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        public PasswordResetController(DBConnection context, ILogger<PasswordResetController> logger, IOptions<SystemConfiguration> systemConfiguration)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
        }

        //GET: PasswordReset/Index/id
        public IActionResult Index([FromQuery(Name = "id")] string id)
        {
            ViewBag.ShowForm = "false";
            ViewData["Title"] = "Reset Password";
            try
            {
                //if query string is empty
                if (string.IsNullOrEmpty(id))
                {
                    TempData["ErrorMessage"] = "Bad url parameter";
                    return View();
                }

                //if no id available in reset table
                if (_context.PasswordForgot.Any(s => s.ResetID == id))
                {
                    DateTime CurrentDate = DateTime.Now;
                    DateTime ResetDate = Convert.ToDateTime(_context.PasswordForgot.Where(s => s.ResetID == id).FirstOrDefault().ResetDate);
                    double Difference = (CurrentDate - ResetDate).TotalDays;

                    //return reset password view
                    if(Difference < 1)
                    {
                        ViewBag.ShowForm = "true";
                        ViewBag.ResetID = id;
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Reset link has expired";
                        return View();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid url id";
                    return View();
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Password Reset Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to " + functions.GetSiteLookupData("SupportEmail");
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: PasswordReset/ResetPassword
        public IActionResult ResetPassword()
        {
            //get input values
            string ResetID = HttpContext.Request.Form["ResetID"];
            string NewPassword = HttpContext.Request.Form["Password"];
            string ConfirmPassword = HttpContext.Request.Form["ConfirmPassword"];

            try
            {
                string[] ValidationInputs = { ResetID, NewPassword, ConfirmPassword };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";
                    return RedirectToAction("Index", new { id = ResetID });
                }


                //verify password match
                if (!functions.PasswordsMatch(NewPassword, ConfirmPassword))
                {
                    TempData["ErrorMessage"] = "Passwords do not match";
                    return RedirectToAction("Index", new { id = ResetID });
                }

                string AccountID = _context.PasswordForgot.Where(s => s.ResetID == ResetID).FirstOrDefault().AccountID;

                // get password
                var query = _context.Accounts.Where(s => s.AccountID == AccountID);
                string hashedPassword = (query.Any()) ? query.FirstOrDefault().Password : "";

                //Update values
                NewPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                functions.UpdateTableData("Accounts", "AccountID", AccountID, "Password", NewPassword, _systemConfiguration.connectionString);

                TempData["SuccessMessage"] = "Account password has been reset successfully.";

                return RedirectToAction("Index", "SignIn");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Reset Account Password Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("Index", new { id = ResetID });
            }
        }



    }
}