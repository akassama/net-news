using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetNews.Models;
using NetNews.Models.AccountsDataModel;
using NetNews.Models.AppModels;

namespace NetNews.Controllers
{
    public class SignUpController : Controller
    {

        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(DBConnection context, ILogger<SignUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: SignUp
        public IActionResult Index()
        {
            return View();
        }

        // POST: SignUp/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AccountsModel accountsModel)
        {
            if (ModelState.IsValid)
            {
                //verify password match
                string ConfirmPassword = Request.Form["ConfirmPassword"];
                if(!functions.PasswordsMatch(accountsModel.Password, ConfirmPassword))
                {
                    TempData["ErrorMessage"] = "Passwords do not match";
                    return View(accountsModel);
                }

                try
                {
                    //set registration default values
                    accountsModel.AccountID = functions.GetUinqueId();
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
                    TempData["SuccessMessage"] = "Thank you for registering. Your registration would be reviewed by our team and you would be notified once approved. This may take up to 24 hours.";
                    return RedirectToAction("Index", "Login");
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


        private bool AccountsModelExists(int id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }
    }
}
