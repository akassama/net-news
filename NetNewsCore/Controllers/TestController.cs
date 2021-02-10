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
using NetNews.Models.AppDataModels;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class TestController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<TestController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public TestController(DBConnection context, ILogger<TestController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
        }

        // GET: Test
        public IActionResult Index()
        {
            //return View(await _context.SiteDataLookup.ToListAsync());
            return RedirectToAction("Index", "Home");
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteDataLookupModel = await _context.SiteDataLookup
                .FirstOrDefaultAsync(m => m.ID == id);
            if (siteDataLookupModel == null)
            {
                return NotFound();
            }

            return View(siteDataLookupModel);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UinqueKey,Value,DateAdded")] SiteDataLookupModel siteDataLookupModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteDataLookupModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteDataLookupModel);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteDataLookupModel = await _context.SiteDataLookup.FindAsync(id);
            if (siteDataLookupModel == null)
            {
                return NotFound();
            }
            return View(siteDataLookupModel);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UinqueKey,Value,DateAdded")] SiteDataLookupModel siteDataLookupModel)
        {
            if (id != siteDataLookupModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteDataLookupModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteDataLookupModelExists(siteDataLookupModel.ID))
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
            return View(siteDataLookupModel);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteDataLookupModel = await _context.SiteDataLookup
                .FirstOrDefaultAsync(m => m.ID == id);
            if (siteDataLookupModel == null)
            {
                return NotFound();
            }

            return View(siteDataLookupModel);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteDataLookupModel = await _context.SiteDataLookup.FindAsync(id);
            _context.SiteDataLookup.Remove(siteDataLookupModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteDataLookupModelExists(int id)
        {
            return _context.SiteDataLookup.Any(e => e.ID == id);
        }
    }
}
