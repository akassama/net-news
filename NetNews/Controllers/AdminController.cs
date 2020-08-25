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
using NetNews.Models.PostsDataModel;

namespace NetNews.Controllers
{
    //Authorize only logged in users
    //[TypeFilter(typeof(SessionAuthorize))]
    public class AdminController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(DBConnection context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return View();
        }


        //  ██████╗  ██████╗ ███████╗████████╗███████╗
        //  ██╔══██╗██╔═══██╗██╔════╝╚══██╔══╝██╔════╝
        //  ██████╔╝██║   ██║███████╗   ██║   ███████╗
        //  ██╔═══╝ ██║   ██║╚════██║   ██║   ╚════██║
        //  ██║     ╚██████╔╝███████║   ██║   ███████║
        //  ╚═╝      ╚═════╝ ╚══════╝   ╚═╝   ╚══════╝
        //    

        // GET: Posts
        public IActionResult Posts()
        {
            return View();
        }


        // GET: Create Post
        public IActionResult CreatePost()
        {
            return View();
        }


        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(PostsModel postsModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(postsModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Create Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }









        //   ██████╗  █████╗ ██╗     ██╗     ███████╗██████╗ ██╗███████╗███████╗
        //  ██╔════╝ ██╔══██╗██║     ██║     ██╔════╝██╔══██╗██║██╔════╝██╔════╝
        //  ██║  ███╗███████║██║     ██║     █████╗  ██████╔╝██║█████╗  ███████╗
        //  ██║   ██║██╔══██║██║     ██║     ██╔══╝  ██╔══██╗██║██╔══╝  ╚════██║
        //  ╚██████╔╝██║  ██║███████╗███████╗███████╗██║  ██║██║███████╗███████║
        //   ╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝
        //                                                                      















        //   █████╗  ██████╗ ██████╗ ██████╗ ██╗   ██╗███╗   ██╗████████╗███████╗
        //  ██╔══██╗██╔════╝██╔════╝██╔═══██╗██║   ██║████╗  ██║╚══██╔══╝██╔════╝
        //  ███████║██║     ██║     ██║   ██║██║   ██║██╔██╗ ██║   ██║   ███████╗
        //  ██╔══██║██║     ██║     ██║   ██║██║   ██║██║╚██╗██║   ██║   ╚════██║
        //  ██║  ██║╚██████╗╚██████╗╚██████╔╝╚██████╔╝██║ ╚████║   ██║   ███████║
        //  ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        //                                                                       















        //  ██╗   ██╗██╗██████╗ ███████╗ ██████╗ ███████╗
        //  ██║   ██║██║██╔══██╗██╔════╝██╔═══██╗██╔════╝
        //  ██║   ██║██║██║  ██║█████╗  ██║   ██║███████╗
        //  ╚██╗ ██╔╝██║██║  ██║██╔══╝  ██║   ██║╚════██║
        //   ╚████╔╝ ██║██████╔╝███████╗╚██████╔╝███████║
        //    ╚═══╝  ╚═╝╚═════╝ ╚══════╝ ╚═════╝ ╚══════╝
        //                                               















        //  ██████╗ ███████╗██╗   ██╗██╗███████╗██╗    ██╗███████╗
        //  ██╔══██╗██╔════╝██║   ██║██║██╔════╝██║    ██║██╔════╝
        //  ██████╔╝█████╗  ██║   ██║██║█████╗  ██║ █╗ ██║███████╗
        //  ██╔══██╗██╔══╝  ╚██╗ ██╔╝██║██╔══╝  ██║███╗██║╚════██║
        //  ██║  ██║███████╗ ╚████╔╝ ██║███████╗╚███╔███╔╝███████║
        //  ╚═╝  ╚═╝╚══════╝  ╚═══╝  ╚═╝╚══════╝ ╚══╝╚══╝ ╚══════╝
        //                                                        















        //  ██████╗ ██████╗  ██████╗ ███████╗██╗██╗     ███████╗
        //  ██╔══██╗██╔══██╗██╔═══██╗██╔════╝██║██║     ██╔════╝
        //  ██████╔╝██████╔╝██║   ██║█████╗  ██║██║     █████╗  
        //  ██╔═══╝ ██╔══██╗██║   ██║██╔══╝  ██║██║     ██╔══╝  
        //  ██║     ██║  ██║╚██████╔╝██║     ██║███████╗███████╗
        //  ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝╚══════╝
        //    















        //  ███████╗███████╗████████╗████████╗██╗███╗   ██╗ ██████╗ ███████╗
        //  ██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗  ██║██╔════╝ ██╔════╝
        //  ███████╗█████╗     ██║      ██║   ██║██╔██╗ ██║██║  ███╗███████╗
        //  ╚════██║██╔══╝     ██║      ██║   ██║██║╚██╗██║██║   ██║╚════██║
        //  ███████║███████╗   ██║      ██║   ██║██║ ╚████║╚██████╔╝███████║
        //  ╚══════╝╚══════╝   ╚═╝      ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝
        //   
        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountsModel = await _context.Accounts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountsModel == null)
            {
                return NotFound();
            }

            return View(accountsModel);
        }

        // GET: Accounts/Create
        //Authorize only admin users
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,FirstName,LastName,Email,Password,ProfilePicture,Active,Oauth,EmailVerification,DirectoryName,RememberToken,UpdatedBy,UpdateDate,DateAdded")] AccountsModel accountsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountsModel);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountsModel = await _context.Accounts.FindAsync(id);
            if (accountsModel == null)
            {
                return NotFound();
            }
            return View(accountsModel);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountID,FirstName,LastName,Email,Password,ProfilePicture,Active,Oauth,EmailVerification,DirectoryName,RememberToken,UpdatedBy,UpdateDate,DateAdded")] AccountsModel accountsModel)
        {
            if (id != accountsModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountsModelExists(accountsModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountsModel);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountsModel = await _context.Accounts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountsModel == null)
            {
                return NotFound();
            }

            return View(accountsModel);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountsModel = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accountsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Posts
        public IActionResult Empty()
        {
            return View();
        }


        private bool AccountsModelExists(int id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }
    }
}
