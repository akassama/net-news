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
    public class AuthorsController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<AuthorsController> _logger;
        private readonly SystemConfiguration _systemConfiguration;

        public AuthorsController(DBConnection context, ILogger<AuthorsController> logger, IOptions<SystemConfiguration> systemConfiguration)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }
            string AccountID = "";
            try
            {
                AccountID = _context.Accounts.Where(s => s.DirectoryName == id).FirstOrDefault().AccountID;
                var accountDetailsModel = _context.AccountDetails
                .FirstOrDefault(m => m.AccountID == AccountID);
                if (accountDetailsModel == null)
                {
                    return NotFound();
                }

                ViewBag.AuthorPosts = _context.vwPostsApproved.Where(s => s.PostAuthor == AccountID).OrderByDescending(s=> s.DateApproved).Take(20);
                ViewBag.ConnectionString = _systemConfiguration.connectionString;


                ViewData["Title"] = "Account Details - " + functions.GetAccountData(AccountID, "FullName");
                ViewBag.FullName = functions.GetAccountData(AccountID, "FullName");

                return View(accountDetailsModel);
            }
            catch (Exception ex)
            {   //Log Error
                _logger.LogInformation("View Author Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult RedirectAuthor(string id) 
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                string new_id = _context.Accounts.Where(s => s.AccountID == id).FirstOrDefault().DirectoryName;
                return RedirectToAction("Index", "Authors", new { id = new_id });
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Redirect Author Error: " + ex.ToString());
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