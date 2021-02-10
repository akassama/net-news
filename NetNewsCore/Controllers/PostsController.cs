using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class PostsController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<PostsController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public PostsController(DBConnection context, ILogger<PostsController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "Index")
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var postModel = _context.vwPostsApproved
                .FirstOrDefault(m => m.PostPermalink == id);
                if (postModel == null)
                {
                    //check if PostPermalink contained in another PostPermalink
                    if (_context.vwPostsApproved.Any(s => s.PostPermalink.Contains(id)))
                    {
                        string PostPermalink = _context.vwPostsApproved.Where(s => s.PostPermalink.Contains(id)).OrderByDescending(s => s.ApprovalsDateAdded).FirstOrDefault().PostPermalink;
                        return RedirectToAction("Index", "Posts", new { id = PostPermalink });
                    }

                    //check if PostPermalink trimmed contained in another PostPermalink
                    id = id.Substring(0, id.Length - 10); //remove last 10 characters
                    if (_context.vwPostsApproved.Any(s => s.PostPermalink.Contains(id)))
                    {
                        string PostPermalink = _context.vwPostsApproved.Where(s => s.PostPermalink.Contains(id)).OrderByDescending(s => s.ApprovalsDateAdded).FirstOrDefault().PostPermalink;
                        return RedirectToAction("Index", "Posts", new { id = PostPermalink });
                    }
                    return NotFound();
                }

                if (Convert.ToBoolean(functions.GetSiteLookupData("EnableFaceBookComments")))
                {
                    ViewData["FacebookCommentId"] = functions.GetSiteLookupData("FacebookCommentAppId");
                }

                ViewBag.FaceBookComments = Convert.ToBoolean(functions.GetSiteLookupData("EnableFaceBookComments"));

                string PostID = _context.vwPostsApproved.Where(s => s.PostPermalink == id).FirstOrDefault().PostID;
                ViewBag.PostID = PostID;
                string PostTitle = _context.vwPostsApproved.Where(s => s.PostPermalink == id).FirstOrDefault().PostTitle;
                string PostAuthor = _context.vwPostsApproved.Where(s => s.PostPermalink == id).FirstOrDefault().PostAuthor;
                string PostType = _context.vwPostsApproved.Where(s => s.PostPermalink == id).FirstOrDefault().PostType;
                string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                string OtherInfo = null; //add any other info here

                //log post view
                functions.LogPostView(PostID, PostAuthor, PostType, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), OtherInfo);

                //log visit
                functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[1], PostTitle, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

                //get ShareThis url
                ViewBag.ShareThisUrl = functions.GetSiteLookupData("ShareThisUrl");

                ViewBag.ConnectionString = _systemConfiguration.connectionString;

                ViewData["Title"] = PostTitle;
                ViewData["ContentKeywords"] = postModel.MetaKeywords;
                ViewData["ContentDescription"] = PostTitle;
                ViewData["PostAuthor"] = PostAuthor;

                //Set properties
                ViewData["PropertyDescription"] = "By " + functions.GetAccountData(PostAuthor, "FullName")+", "+ functions.FormatLongText(PostTitle, 120);
                ViewData["PropertySection"] = _context.Categories.Where(s=> s.CategoryID == postModel.PostCategory).FirstOrDefault().CategoryName;
                ViewData["PropertyUpdatedTime"] = postModel.UpdateDate;

                return View(postModel);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Get Post Details Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ViewPost([FromQuery(Name = "id")] string id) {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            var DBQuery = _context.NewsApi.Where(s => s.PostID == id);
            if (!DBQuery.Any())
            {
                return RedirectToAction("Index", "Home");
            }



            //get ShareThis url
            ViewBag.ShareThisUrl = functions.GetSiteLookupData("ShareThisUrl");

            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            ViewData["Title"] = DBQuery.FirstOrDefault().Title;
            ViewData["ContentKeywords"] = DBQuery.FirstOrDefault().Category;
            ViewData["ContentDescription"] = DBQuery.FirstOrDefault().Title;
            ViewData["PostAuthor"] = DBQuery.FirstOrDefault().Author;

            //Set properties
            ViewData["PropertyDescription"] = "By " + DBQuery.FirstOrDefault().Author + ", " + functions.FormatLongText(DBQuery.FirstOrDefault().Title, 120);
            ViewData["PropertySection"] = DBQuery.FirstOrDefault().Category;
            ViewData["PropertyUpdatedTime"] = DBQuery.FirstOrDefault().PublishedAt;

            ViewBag.PostUrl = DBQuery.FirstOrDefault().Url;
            return View();
        }

        //ovveride NotFound() to E404 error page
        public new IActionResult NotFound()
        {
            return RedirectToAction("E404", "Error");
        }

    }
}