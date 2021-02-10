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
using System.Drawing;
using LazZiya.ImageResize;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NetNews.Models.Email;
using NetNews.Models.AppDataModels;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    //Authorize only logged in users
    [TypeFilter(typeof(SessionAuthorize))]
    public class AdminController : Controller
    {
        AppFunctions functions = new AppFunctions();
        AppValidations validations = new AppValidations();

        private readonly DBConnection _context;
        private readonly ILogger<AdminController> _logger;
        private readonly SessionManager _sessionManager;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public static string PublicAccountID;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;

        public AdminController(DBConnection context, ILogger<AdminController> logger, IOptions<SystemConfiguration> systemConfiguration,
            SessionManager sessionManager, IWebHostEnvironment hostingEnvironment, IDetectionService detectionService, IHttpContextAccessor accessor)
        {
            _context = context;
            _logger = logger;
            _sessionManager = sessionManager;
            _systemConfiguration = systemConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
            PublicAccountID = _sessionManager.LoginAccountId;
            _detectionService = detectionService;
            _accessor = accessor;
        }


        // GET: Dashboard
        public IActionResult Index()
        {
            // Set Meta Data 
            ViewData["Title"] = "Dashboard";

            string AccountID = _sessionManager.LoginAccountId;
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[10], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            ViewBag.PostsData = _context.vwPosts.Where(s => s.PostAuthor == AccountID).OrderByDescending(s => s.ID).ToList();
            ViewBag.TotalPosts = _context.vwPosts.Count(s => s.PostAuthor == AccountID);

            //Viewbags for delete and edit actions for approved posts
            ViewBag.EditApprovedPosts = _systemConfiguration.editApprovedPosts;
            ViewBag.DeleteApprovedPosts = _systemConfiguration.deleteApprovedPosts;

            return View();
        }

        //  ██████╗  ██████╗ ███████╗████████╗███████╗
        //  ██╔══██╗██╔═══██╗██╔════╝╚══██╔══╝██╔════╝
        //  ██████╔╝██║   ██║███████╗   ██║   ███████╗
        //  ██╔═══╝ ██║   ██║╚════██║   ██║   ╚════██║
        //  ██║     ╚██████╔╝███████║   ██║   ███████║
        //  ╚═╝      ╚═════╝ ╚══════╝   ╚═╝   ╚══════╝
        //    


        // GET: StandardNewsPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult StandardNewsPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "Standard News Post";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            return View();
        }


        // POST: Admin/CreatePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> StandardNewsPost(PostsModel postsModel, List<IFormFile> PostImageSelect)
        {
            // Set Meta Data 
            ViewData["Title"] = "Standard News Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            string AccountID = _sessionManager.LoginAccountId;

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            //check if PostImage in model
            if (Request.Form.Files.Count == 0)
            {
                TempData["ErrorMessage"] = "Post image is required.";
                return View(postsModel);
            }

            if (ModelState.IsValid)
            {
                //Image watermark from config file
                string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                //Set post image to empty. Assigned upon upload
                string PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);

                try
                {
                    //Set default post values
                    postsModel.PostID = functions.GetGuid();
 
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostImage = PostImageLink;
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[0];
                    //check if celebrity news type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[4];
                    }
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;
                    postsModel.DateAdded = DateTime.Now;

                    _context.Add(postsModel);
                    await _context.SaveChangesAsync();

                    int PostStatus = functions.Int32Parse(functions.GetSiteLookupData("DefaultPostApproveStatus"));

                    string Message = "Post added successfully. The post may need to be approved by the adminitrator before being available on site. This may take a couple of hours.";

                    //add post to post approvals
                    functions.AddPostApprovalData(postsModel.PostID, postsModel.PostType, PostStatus);

                    TempData["SuccessMessage"] = Message;

                    //send email to editor
                    string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                    functions.EditorPostNotification(postsModel.PostEditor, PostLink, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany,
                            _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);


                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewPost", LogAction);
                    }

                    return RedirectToAction("ManagePosts", "Admin");
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



        // GET: Manage Posts
        public IActionResult ManagePosts()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Posts";

            string AccountID = _sessionManager.LoginAccountId;
            var PostsData = _context.vwPosts.Where(s => s.PostAuthor == AccountID).OrderByDescending(s => s.ID).ToList();

            //Viewbags for delete and edit actions for approved posts
            ViewBag.EditApprovedPosts = _systemConfiguration.editApprovedPosts;
            ViewBag.DeleteApprovedPosts = _systemConfiguration.deleteApprovedPosts;

            return View(PostsData);
        }


        // GET: Admin/EditStandardNewsPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditStandardNewsPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Post Details";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }



        // POST: Admin/EditStandardNewsPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult EditStandardNewsPost(PostsModel postsModel, List<IFormFile> PostImageSelect)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Post Details";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postsModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ApprovalState;
            }

            if (!_systemConfiguration.deleteApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            string AccountID = _sessionManager.LoginAccountId;

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";


            if (ModelState.IsValid)
            {
                //reset post id
                postsModel.ID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ID;

                //Set post image to current image. Would be updated below if changed
                string PostImageLink = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostImage;

                //Image watermark from config file
                string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                //check if PostImage in model
                if (PostImageSelect.Count > 0)
                {
                    //Set post image to empty. Assigned upon upload
                    PostImageLink = "";

                    //Set post image to empty. Assigned upon upload
                    PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);
                }


                try
                {
                    //Set default post values
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[0];
                    //check if celebrity news type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[4];
                    }
                    postsModel.PostEditor = _context.Posts.Where(s=> s.PostID == postsModel.PostID).FirstOrDefault().PostEditor;
                    postsModel.PostImage = PostImageLink;
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;

                    functions.UpdatePostdata(postsModel.PostID, postsModel.PostType, postsModel.PostPermalink, postsModel.PostAuthor, postsModel.PostCategory, postsModel.PostSubCategory, postsModel.PostTitle, postsModel.PostExtract,
                        postsModel.PostImage, postsModel.ImageCaption, postsModel.IsBreakingNews, postsModel.PostContent, postsModel.PostVideoType, postsModel.PostVideoLink, postsModel.PostAudioType, postsModel.PostAudioLink,
                        postsModel.PostTags, postsModel.PostEditor, postsModel.UpdatedBy);

                    var PostType = _systemConfiguration.postTypes.Split(",")[0];
                    var PostStatus = _context.PostApprovals.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ApprovalState;

                    //update post to post approvals
                    functions.UpdatePostApprovalData(postsModel.PostID, PostType, PostStatus);

                    //if post has author comment to reviewer, add comment
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["ReviewComment"]) && HttpContext.Request.Form["ReviewComment"].ToString().Length > 1)
                    {
                        string ReviewComment = HttpContext.Request.Form["ReviewComment"];
                        functions.AddPostComments(postsModel.PostID, AccountID, ReviewComment);


                        //notify reviewers of action and comment
                        var DBQuery = _context.PostReviews.Where(s => s.PostID == postsModel.PostID);
                        string Notified = "";
                        foreach(var item in DBQuery)
                        {
                            string ReviewerID = item.ReviewerID;
                            string PostAuthorID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostAuthor;
                            string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                            if (!Notified.Contains(ReviewerID))
                            {
                                functions.NotifyReviewersOfComment(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);
                            }
                            Notified += ReviewerID +" ";
                        }
                    }

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditPost", LogAction);
                    }

                    TempData["SuccessMessage"] = "Post updated successfully.";
                    return RedirectToAction("ManagePosts", "Admin");
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




        // GET: EntertainmentNewsPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult EntertainmentNewsPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "Entertainment News Post";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            return View();
        }




        // GET: Admin/EditEntertainmentNewsPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditEntertainmentNewsPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Entertainment Post Details";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }




        // GET: NewsVideoPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult NewsVideoPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "News Video Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsVideoPost(PostsModel postsModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            // Set Meta Data 
            ViewData["Title"] = "News Video Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            if (ModelState.IsValid)
            {
                string[] ValidationInputs = { postsModel.PostVideoType, postsModel.PostVideoLink };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return View(postsModel);
                }

                try
                {
                    //Set default post values
                    postsModel.PostID = functions.GetGuid();
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[1];
                    //check if entertainment video type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[5];
                    }
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;
                    postsModel.DateAdded = DateTime.Now;

                    _context.Add(postsModel);
                    await _context.SaveChangesAsync();

                    int PostStatus = functions.Int32Parse(functions.GetSiteLookupData("DefaultPostApproveStatus"));

                    string Message = "Post added successfully. The post may need to be approved by the adminitrator before being available on site. This may take a couple of hours.";

                    //add post to post approvals
                    functions.AddPostApprovalData(postsModel.PostID, postsModel.PostType, PostStatus);

                    TempData["SuccessMessage"] = Message;

                    //send email to editor
                    string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                    functions.EditorPostNotification(postsModel.PostEditor, PostLink, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany,
                            _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewPost", LogAction);
                    }

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Create Video Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }


        // GET: Admin/EditNewsVideoPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditNewsVideoPost(string id, [FromQuery(Name = "comment")] string comment) 
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Video Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }


        // POST: Admin/EditVideoPost/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNewsVideoPost(PostsModel postsModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            if (ModelState.IsValid)
            {
                //reset post id
                postsModel.ID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ID;

                string[] ValidationInputs = { postsModel.PostVideoType, postsModel.PostVideoLink };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return View(postsModel);
                }

                try
                {
                    //Set default post values
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[1];
                    //check if entertainment video type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[5];
                    }
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostEditor = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostEditor;
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;

                    functions.UpdatePostdata(postsModel.PostID, postsModel.PostType, postsModel.PostPermalink, postsModel.PostAuthor, postsModel.PostCategory, postsModel.PostSubCategory, postsModel.PostTitle, postsModel.PostExtract,
                        postsModel.PostImage, postsModel.ImageCaption, postsModel.IsBreakingNews, postsModel.PostContent, postsModel.PostVideoType, postsModel.PostVideoLink, postsModel.PostAudioType, postsModel.PostAudioLink,
                        postsModel.PostTags, postsModel.PostEditor, postsModel.UpdatedBy);

                    var PostType = _systemConfiguration.postTypes.Split(",")[1];
                    var PostStatus = _context.PostApprovals.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ApprovalState;

                    //update post to post approvals
                    functions.UpdatePostApprovalData(postsModel.PostID, PostType, PostStatus);

                    //if post has author comment to reviewer, add comment
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["ReviewComment"]) && HttpContext.Request.Form["ReviewComment"].ToString().Length > 1)
                    {
                        string ReviewComment = HttpContext.Request.Form["ReviewComment"];
                        functions.AddPostComments(postsModel.PostID, AccountID, ReviewComment);


                        //notify reviewers of action and comment
                        var DBQuery = _context.PostReviews.Where(s => s.PostID == postsModel.PostID);
                        string Notified = "";
                        foreach (var item in DBQuery)
                        {
                            string ReviewerID = item.ReviewerID;
                            string PostAuthorID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostAuthor;
                            string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                            if (!Notified.Contains(ReviewerID))
                            {
                                functions.NotifyReviewersOfComment(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);
                            }
                            Notified += ReviewerID + " ";
                        }
                    }

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditPost", LogAction);
                    }

                    TempData["SuccessMessage"] = "Post updated successfully.";

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Edit Video Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }




        // GET: EntertainmentVideoPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult EntertainmentVideoPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "Entertainment Video Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            return View();
        }



        // GET: Admin/EditEntertainmentVideoPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditEntertainmentVideoPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Entertainment Video Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }




        // GET: NewsAudioPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult NewsAudioPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "News Audio Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            return View();
        }


        // POST: Admin/NewsAudioPost/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsAudioPost(PostsModel postsModel, List<IFormFile> PostImageSelect, List<IFormFile> PostAudioLink)
        {
            string AccountID = _sessionManager.LoginAccountId;

            // Set Meta Data 
            ViewData["Title"] = "News Audio Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            if (ModelState.IsValid)
            {

                if (PostAudioLink == null)
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return View(postsModel);
                }

                try
                {
                    //upload audio file
                    string MediaFileName = null;
                    if (PostAudioLink.Count > 0)
                    {
                        MediaFileName = functions.UploadMediaFile(PostAudioLink);
                    }

                    //Set post image to current image. Would be updated below if changed
                    string PostImageLink = null;

                    //Image watermark from config file
                    string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                    string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                    int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                    int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                    //check if PostImage in model
                    if (PostImageSelect.Count > 0)
                    {
                        //Set post image to empty. Assigned upon upload
                        PostImageLink = "";

                        //Set post image to empty. Assigned upon upload
                        PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);
                    }


                    //Set default post values
                    postsModel.PostID = functions.GetGuid();
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostImage = PostImageLink;
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[3];
                    //check if entertainment audio type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[6];
                    }
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostTags = PostTags;
                    postsModel.PostAudioType = "mp3";
                    postsModel.PostAudioLink = MediaFileName;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;
                    postsModel.DateAdded = DateTime.Now;

                    _context.Add(postsModel);
                    await _context.SaveChangesAsync();


                    int PostStatus = functions.Int32Parse(functions.GetSiteLookupData("DefaultPostApproveStatus"));

                    string Message = "Post added successfully. The post may need to be approved by the adminitrator before being available on site. This may take a couple of hours.";

                    //add post to post approvals
                    functions.AddPostApprovalData(postsModel.PostID, postsModel.PostType, PostStatus);

                    TempData["SuccessMessage"] = Message;

                    //send email to editor
                    string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                    functions.EditorPostNotification(postsModel.PostEditor, PostLink, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany,
                            _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewPost", LogAction);
                    }

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Create Audio Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }




        // GET: Admin/EditNewsAudioPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditNewsAudioPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Post Details";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "0";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }



        // POST: Admin/EditNewsAudioPost/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNewsAudioPost(PostsModel postsModel, List<IFormFile> PostImageSelect, List<IFormFile> PostAudioLink)
        {
            string AccountID = _sessionManager.LoginAccountId;

            //set post type for entertainment news
            ViewBag.EntertainmentPost = HttpContext.Request.Form["EntertainmentPost"];

            if (ModelState.IsValid)
            {
                //reset post id
                postsModel.ID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ID;

                try
                {
                    //upload audio file if any
                    string MediaFileName = null;
                    if (PostAudioLink.Count > 0)
                    {
                        MediaFileName = functions.UploadMediaFile(PostAudioLink);
                    }


                    //Set post image to current image. Would be updated below if changed
                    string PostImageLink = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostImage;

                    //Image watermark from config file
                    string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                    string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                    int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                    int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                    //check if PostImage in model
                    if (PostImageSelect.Count > 0)
                    {
                        //Set post image to empty. Assigned upon upload
                        PostImageLink = "";

                        //Set post image to empty. Assigned upon upload
                        PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);
                    }


                    //Set default post values
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostImage = PostImageLink;
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[3];
                    //check if entertainment audio type
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["EntertainmentPost"]) && HttpContext.Request.Form["EntertainmentPost"] == "1")
                    {
                        postsModel.PostType = _systemConfiguration.postTypes.Split(",")[6];
                    }
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostEditor = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostEditor;
                    postsModel.PostTags = PostTags;
                    postsModel.PostAudioType = "mp3";
                    postsModel.PostAudioLink = MediaFileName;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;

                    functions.UpdatePostdata(postsModel.PostID, postsModel.PostType, postsModel.PostPermalink, postsModel.PostAuthor, postsModel.PostCategory, postsModel.PostSubCategory, postsModel.PostTitle, postsModel.PostExtract,
                        postsModel.PostImage, postsModel.ImageCaption, postsModel.IsBreakingNews, postsModel.PostContent, postsModel.PostVideoType, postsModel.PostVideoLink, postsModel.PostAudioType, postsModel.PostAudioLink,
                        postsModel.PostTags, postsModel.PostEditor, postsModel.UpdatedBy);

                    var PostType = _systemConfiguration.postTypes.Split(",")[2];
                    var PostStatus = _context.PostApprovals.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ApprovalState;

                    //update post to post approvals
                    functions.UpdatePostApprovalData(postsModel.PostID, PostType, PostStatus);

                    //if post has author comment to reviewer, add comment
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["ReviewComment"]) && HttpContext.Request.Form["ReviewComment"].ToString().Length > 1)
                    {
                        string ReviewComment = HttpContext.Request.Form["ReviewComment"];
                        functions.AddPostComments(postsModel.PostID, AccountID, ReviewComment);


                        //notify reviewers of action and comment
                        var DBQuery = _context.PostReviews.Where(s => s.PostID == postsModel.PostID);
                        string Notified = "";
                        foreach (var item in DBQuery)
                        {
                            string ReviewerID = item.ReviewerID;
                            string PostAuthorID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostAuthor;
                            string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                            if (!Notified.Contains(ReviewerID))
                            {
                                functions.NotifyReviewersOfComment(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);
                            }
                            Notified += ReviewerID + " ";
                        }
                    }

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditPost", LogAction);
                    }

                    TempData["SuccessMessage"] = "Post updated successfully.";

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Edit Audio Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }



        // GET: EntertainmentAudioPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult EntertainmentAudioPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "Entertainment Audio Post";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            return View();
        }



        // GET: Admin/EditEntertainmentAudioPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditEntertainmentAudioPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Entertainment Post Details";

            //set post type for entertainment news
            ViewBag.EntertainmentPost = "1";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            return View(postModel);
        }


        // GET: NewsGalleryPost
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult NewsGalleryPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "News Gallery Post";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.EditorList = functions.GetEditorList(AccountID, "Editor Permissions");

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            //get max upload gallery limit
            ViewBag.MaxGalleryImages = functions.Int32Parse(functions.GetSiteLookupData("MaxGalleryImages"), 12);

            return View();
        }



        // POST: Admin/NewsGalleryPost/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsGalleryPost(PostsModel postsModel, List<IFormFile> PostImageSelect, List<IFormFile> PostImages)
        {
            string AccountID = _sessionManager.LoginAccountId;

            // Set Meta Data 
            ViewData["Title"] = "News Audio Post";

            //get max upload gallery limit
            ViewBag.MaxGalleryImages = functions.Int32Parse(functions.GetSiteLookupData("MaxGalleryImages"), 12);

            if (ModelState.IsValid)
            {

                if (PostImages == null)
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return View(postsModel);
                }

                //Image watermark from config file
                string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                //Set post image to empty. Assigned upon upload
                string PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);

                try
                {
                    //Set default post values
                    postsModel.PostID = functions.GetGuid();
                    //Set post image to empty. Assigned upon upload. Returns first Post Image
                    if (PostImages.Count > 0)
                    {
                        string Captions = HttpContext.Request.Form["ImgCaption"];
                        functions.UploadGalleryImages(AccountID, postsModel.PostID, PostImages, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth, Captions);
                    }
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostImage = PostImageLink;
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[2];
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;
                    postsModel.DateAdded = DateTime.Now;

                    _context.Add(postsModel);
                    await _context.SaveChangesAsync();


                    int PostStatus = functions.Int32Parse(functions.GetSiteLookupData("DefaultPostApproveStatus"));

                    string Message = "Post added successfully. The post may need to be approved by the adminitrator before being available on site. This may take a couple of hours.";

                    //add post to post approvals
                    functions.AddPostApprovalData(postsModel.PostID, postsModel.PostType, PostStatus);

                    TempData["SuccessMessage"] = Message;

                    //send email to editor
                    string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                    functions.EditorPostNotification(postsModel.PostEditor, PostLink, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany,
                            _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewPost", LogAction);
                    }

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Create Gallery Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }




        // GET: Admin/EditNewsGalleryPost/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditNewsGalleryPost(string id, [FromQuery(Name = "comment")] string comment)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Gallery Post Details";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            //check if Author Permissions allowed
            int ApprovalState = 0;
            if (_context.vwPosts.Any(s => s.PostID == postModel.PostID))
            {
                ApprovalState = _context.vwPosts.Where(s => s.PostID == postModel.PostID).FirstOrDefault().ApprovalState;
            }
            if (!_systemConfiguration.editApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Edit action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();
            ViewBag.TagsList = functions.GetTagsList();
            ViewBag.IsBreakingNews = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().IsBreakingNews == 1) ? "checked" : "";
            ViewBag.FeaturedPost = (_context.Posts.Where(s => s.PostID == id).FirstOrDefault().FeaturedPost == 1) ? "checked" : "";

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManagePosts";

            ViewBag.Comment = comment; //checks if edit is for commented/reviewed post. Add comment box in edit form
            if (!string.IsNullOrEmpty(comment))
            {
                ViewBag.CancelRoute = "PostReviews";
            }

            //get max upload gallery limit
            ViewBag.MaxGalleryImages = functions.Int32Parse(functions.GetSiteLookupData("MaxGalleryImages"), 12);

            return View(postModel);
        }



        // POST: Admin/EditNewsGalleryPost/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNewsGalleryPost(PostsModel postsModel, List<IFormFile> PostImageSelect, List<IFormFile> PostImages)
        {
            string AccountID = _sessionManager.LoginAccountId;

            //get max upload gallery limit
            ViewBag.MaxGalleryImages = functions.Int32Parse(functions.GetSiteLookupData("MaxGalleryImages"), 12);

            if (ModelState.IsValid)
            {
                //reset post id
                postsModel.ID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ID;

                //Set post image to current image. Would be updated below if changed
                string PostImageLink = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostImage;

                //Image watermark from config file
                string TextWaterMark = functions.GetSiteLookupData("TextWaterMark");
                string ImageWaterMark = functions.GetSiteLookupData("ImageWaterMark");
                int ImageHeight = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultHeight"), 820);
                int ImageWidth = functions.Int32Parse(functions.GetSiteLookupData("UploadImageDefaultWidth"), 1450);;

                //check if PostImage in model
                if (PostImageSelect.Count > 0)
                {
                    //Set post image to empty. Assigned upon upload
                    PostImageLink = "";

                    //Set post image to empty. Assigned upon upload
                    PostImageLink = functions.UploadImage(PostImageSelect, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth);
                }

                //Set post image to empty. Assigned upon upload. Returns first Post Image
                if (PostImages.Count > 0)
                {
                    //delete current images
                    functions.DeletePostImageGalleries(postsModel.PostID);
                    functions.DeleteTableData("GalleryImages", "PostID", postsModel.PostID, _systemConfiguration.connectionString); //delete from gallery images

                    string Captions = HttpContext.Request.Form["ImgCaption"];
                    functions.UploadGalleryImages(AccountID, postsModel.PostID, PostImages, TextWaterMark, ImageWaterMark, ImageHeight, ImageWidth, Captions);
                }


                try
                {

                    //Set default post values
                    postsModel.PostPermalink = functions.GeneratePostPermalink(postsModel.PostTitle);
                    postsModel.PostImage = PostImageLink;
                    postsModel.PostType = _systemConfiguration.postTypes.Split(",")[3];
                    postsModel.IsBreakingNews = (string.IsNullOrEmpty(HttpContext.Request.Form["IsBreakingNews"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["IsBreakingNews"]);
                    postsModel.FeaturedPost = (string.IsNullOrEmpty(HttpContext.Request.Form["FeaturedPost"])) ? 0 : functions.Int32Parse(HttpContext.Request.Form["FeaturedPost"]);
                    string PostTags = HttpContext.Request.Form["PostTags"] + (!string.IsNullOrEmpty(HttpContext.Request.Form["MorePostTags"]) ? ","+ HttpContext.Request.Form["MorePostTags"] : "");
                    postsModel.PostEditor = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostEditor;
                    postsModel.PostTags = PostTags;
                    postsModel.MetaTitle = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaTitle"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaTitle"].ToString();
                    postsModel.MetaDescription = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaDescription"])) ? postsModel.PostTitle : HttpContext.Request.Form["MetaDescription"].ToString();
                    postsModel.MetaKeywords = (string.IsNullOrEmpty(HttpContext.Request.Form["MetaKeywords"])) ? postsModel.PostTags : HttpContext.Request.Form["MetaKeywords"].ToString();
                    postsModel.PostAuthor = _sessionManager.LoginAccountId;
                    postsModel.UpdatedBy = _sessionManager.LoginAccountId;
                    postsModel.UpdateDate = DateTime.Now;

                    functions.UpdatePostdata(postsModel.PostID, postsModel.PostType, postsModel.PostPermalink, postsModel.PostAuthor, postsModel.PostCategory, postsModel.PostSubCategory, postsModel.PostTitle, postsModel.PostExtract,
                        postsModel.PostImage, postsModel.ImageCaption, postsModel.IsBreakingNews, postsModel.PostContent, postsModel.PostVideoType, postsModel.PostVideoLink, postsModel.PostAudioType, postsModel.PostAudioLink,
                        postsModel.PostTags, postsModel.PostEditor, postsModel.UpdatedBy);

                    var PostType = _systemConfiguration.postTypes.Split(",")[2];
                    var PostStatus = _context.PostApprovals.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().ApprovalState;

                    //update post to post approvals
                    functions.UpdatePostApprovalData(postsModel.PostID, PostType, PostStatus);

                    //if post has author comment to reviewer, add comment
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["ReviewComment"]) && HttpContext.Request.Form["ReviewComment"].ToString().Length > 1)
                    {
                        string ReviewComment = HttpContext.Request.Form["ReviewComment"];
                        functions.AddPostComments(postsModel.PostID, AccountID, ReviewComment);


                        //notify reviewers of action and comment
                        var DBQuery = _context.PostReviews.Where(s => s.PostID == postsModel.PostID);
                        string Notified = "";
                        foreach (var item in DBQuery)
                        {
                            string ReviewerID = item.ReviewerID;
                            string PostAuthorID = _context.Posts.Where(s => s.PostID == postsModel.PostID).FirstOrDefault().PostAuthor;
                            string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + postsModel.PostID;
                            if (!Notified.Contains(ReviewerID))
                            {
                                functions.NotifyReviewersOfComment(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);
                            }
                            Notified += ReviewerID + " ";
                        }
                    }

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited a post with the title '{postsModel.PostTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditPost", LogAction);
                    }

                    TempData["SuccessMessage"] = "Post updated successfully.";

                    return RedirectToAction("ManagePosts", "Admin");
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Edit Gallery Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                };
            }
            return View(postsModel);
        }



        // GET: Admin/PostDetails/id
        public async Task<IActionResult> PostDetails(string id)
        {
            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.vwPosts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            // Set Meta Data 
            ViewData["Title"] = postModel.PostTitle;

            return View(postModel);
        }


        // GET: Admin/PostReviews
        public IActionResult PostReviews()
        {
            // Set Meta Data 
            ViewData["Title"] = "Post Reviews";

            string AccountID = _sessionManager.LoginAccountId;
            var PostsReviewsData = _context.vwPostReviews.Where(s => s.PostAuthor == AccountID && s.ApprovalState == 0).OrderByDescending(s => s.ID).ToList();
            return View(PostsReviewsData);
        }


        // GET: Admin/PostReviews
        //check if user has access
        [AccessControlFilter(PermissionName = "Editor Permissions")]
        public IActionResult PendingReviews()
        {
            // Set Meta Data 
            ViewData["Title"] = "Pending Reviews";


            string AccountID = _sessionManager.LoginAccountId;
            var PostsReviewsData = _context.vwPosts.Where(s => s.ApprovalState == 0 && s.PostEditor == AccountID).OrderByDescending(s => s.ID).ToList();
            return View(PostsReviewsData);
        }



        // POST: Admin/ApprovePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Editor Permissions")]
        public IActionResult ApprovePost()
        {
            string AccountID = _sessionManager.LoginAccountId;
            string PostID = HttpContext.Request.Form["ModalApprovePostID"];
            string[] ValidationInputs = { PostID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("PendingReviews", "Admin");
            }

            try
            {
                //approve post
                functions.UpdatePostState(PostID, AccountID, 1);

                //notify author of approval
                string PostAuthorID = _context.Posts.Where(s => s.PostID == PostID).FirstOrDefault().PostAuthor;
                string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + PostID;
                functions.NotifyAutorOfApproval(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' approved post with the title '{_context.Posts.Where(s=> s.PostID == PostID).FirstOrDefault().PostTitle}'";
                    functions.LogActivity(PostAuthorID, AccountID, "ApprovePost", LogAction);
                }

                TempData["SuccessMessage"] = "Post approved.";
                return RedirectToAction("PendingReviews", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create Post Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("PendingReviews", "Admin");
            }
        }


        // POST: Admin/CommentOnPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Editor Permissions")]
        public IActionResult CommentOnPost()
        {
            // Set Meta Data 
            ViewData["Title"] = "Comment on Post";

            string AccountID = _sessionManager.LoginAccountId;
            string PostID = HttpContext.Request.Form["ModalCommentPostID"];
            string PostComment = HttpContext.Request.Form["ReviewComment"];
            string[] ValidationInputs = { PostID, PostComment };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("PendingReviews", "Admin");
            }

            try
            {
                //comment on post
                functions.AddPostComments(PostID, AccountID, PostComment);

                //notify author of approval
                string PostAuthorID = _context.Posts.Where(s => s.PostID == PostID).FirstOrDefault().PostAuthor;
                string PostLink = functions.GetSiteLookupData("AppDomain") + "/Admin/PostDetails/" + PostID;
                functions.NotifyAutorOfComment(PostAuthorID, PostLink, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' commented on post with the title '{_context.Posts.Where(s => s.PostID == PostID).FirstOrDefault().PostTitle}'";
                    functions.LogActivity(PostAuthorID, AccountID, "PostComment", LogAction);
                }

                TempData["SuccessMessage"] = "Comment added to post.";
                return RedirectToAction("PendingReviews", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create Post Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("PendingReviews", "Admin");
            }
        }


        // POST: Admin/DeletePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult DeletePost()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemovePostID = HttpContext.Request.Form["ModalDeletePostID"];

            //check if delete approved posts allowed
            int ApprovalState = 0;
            if(_context.vwPosts.Any(s=> s.PostID == RemovePostID))
            {
                ApprovalState = _context.vwPosts.Where(s=> s.PostID == RemovePostID).FirstOrDefault().ApprovalState;
            }
                
            if (!_systemConfiguration.deleteApprovedPosts && ApprovalState == 1)
            {
                TempData["ErrorMessage"] = "Delete action not allowed for post.";
                return RedirectToAction("ManagePosts", "Admin");
            }

            string[] ValidationInputs = { RemovePostID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManagePosts", "Admin");
            }

            try
            {
                string PostTitle = _context.Posts.Where(s => s.PostID == RemovePostID).FirstOrDefault().PostTitle;

                //remove account
                functions.RemovePost(RemovePostID, _systemConfiguration.connectionString);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"Post with title '{PostTitle}' has been removed by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(AccountID, AccountID, "RemovePost", LogAction);
                }


                TempData["SuccessMessage"] = "Post removed.";
                return RedirectToAction("ManagePosts", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Remove Post Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManagePosts", "Admin");
            }
        }


        // GET: Admin/ReviewHistory
        [AccessControlFilter(PermissionName = "Editor Permissions")]
        public IActionResult ReviewHistory()
        {
            // Set Meta Data 
            ViewData["Title"] = "Review History";

            string AccountID = _sessionManager.LoginAccountId;
            var ReviewHistoryData = _context.vwPosts.Where(s => s.ApprovalState == 1 && s.PostEditor == AccountID).OrderByDescending(s => s.ID).ToList();

            return View(ReviewHistoryData);
        }



        // GET: PostViews
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult PostViews([FromQuery(Name = "yr")] string year)
        {
            // Set Meta Data 
            ViewData["Title"] = "Post Views";

            string AccountID = _sessionManager.LoginAccountId;
            ViewBag.ConnectionString = _systemConfiguration.connectionString;
            ViewBag.Year = year;

            var PoostCountryViews = _context.PostViews.Where(s => s.PostAuthor == AccountID)
                        .GroupBy(x => x.Country)
                        .Select(gr => new { PostAuthor = gr.Max(g => g.PostAuthor), Country = gr.Key })
                        .ToList();

            var CountryViewsModel = PoostCountryViews
                        .Select(x => new PostViewsModel { PostAuthor = x.PostAuthor, Country = x.Country })
                        .ToList();

            ViewBag.PostViewsByCountry = CountryViewsModel;


            var PostsData = _context.vwPostsApproved.Where(s => s.PostAuthor == AccountID).OrderByDescending(s => s.ID).ToList();

            return View(PostsData);
        }

        // GET: PostCountryViewDetails
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult PostCountryViewDetails(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Post by View Country";

            string AccountID = _sessionManager.LoginAccountId;
            
            if (id == null)
            {
                return RedirectToAction("PostsViews", "Admin");
            }
            var DBQuery = _context.Countries.Where(s => s.ISO == id);
            if (!DBQuery.Any())
            {
                TempData["ErrorMessage"] = "No country code of "+ id + " found.";
                return RedirectToAction("PostsViews", "Admin");
            }

            string Country = DBQuery.FirstOrDefault().NiceName;

            ViewBag.CountryCode = id;
            ViewBag.Country = Country;


            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            var PostsData = _context.vwPostsApproved.Where(s => s.PostAuthor == AccountID).OrderByDescending(s => s.ID).ToList();

            return View(PostsData);
        }



        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //                                                                                                                                                                                                                                                                                                                   

        //  ███████╗████████╗ █████╗ ████████╗██╗███████╗████████╗██╗ ██████╗███████╗
        //  ██╔════╝╚══██╔══╝██╔══██╗╚══██╔══╝██║██╔════╝╚══██╔══╝██║██╔════╝██╔════╝
        //  ███████╗   ██║   ███████║   ██║   ██║███████╗   ██║   ██║██║     ███████╗
        //  ╚════██║   ██║   ██╔══██║   ██║   ██║╚════██║   ██║   ██║██║     ╚════██║
        //  ███████║   ██║   ██║  ██║   ██║   ██║███████║   ██║   ██║╚██████╗███████║
        //  ╚══════╝   ╚═╝   ╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝   ╚═╝   ╚═╝ ╚═════╝╚══════╝
        //                                                                           
        // GET: Admin/PostStatistics
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult PostStatistics([FromQuery(Name = "yr")] string year)
        {
            // Set Meta Data 
            ViewData["Title"] = "Post Statistics";

            string AccountID = _sessionManager.LoginAccountId;
            ViewBag.ConnectionString = _systemConfiguration.connectionString;
            ViewBag.Year = year;

            var PoostCountryViews = _context.PostViews.Where(s => s.PostID != null)
                        .GroupBy(x => x.Country)
                        .Select(gr => new { Country = gr.Key })
                        .ToList();

            var CountryViewsModel = PoostCountryViews
                        .Select(x => new PostViewsModel { Country = x.Country }).OrderByDescending(s=> s.VisitDate).Take(1000)
                        .ToList();

            ViewBag.PostViewsByCountry = CountryViewsModel;


            var PostsData = _context.vwPostsApproved.OrderByDescending(s => s.ID).Take(1000).ToList();

            return View(PostsData);
        }





        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //                                                                                                                                                                                                                                                                                                                   


        //   █████╗  ██████╗ ██████╗ ██████╗ ██╗   ██╗███╗   ██╗████████╗███████╗
        //  ██╔══██╗██╔════╝██╔════╝██╔═══██╗██║   ██║████╗  ██║╚══██╔══╝██╔════╝
        //  ███████║██║     ██║     ██║   ██║██║   ██║██╔██╗ ██║   ██║   ███████╗
        //  ██╔══██║██║     ██║     ██║   ██║██║   ██║██║╚██╗██║   ██║   ╚════██║
        //  ██║  ██║╚██████╗╚██████╗╚██████╔╝╚██████╔╝██║ ╚████║   ██║   ███████║
        //  ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        //                                                                       

        // GET: Admin/AccountRegistrations
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult AccountRegistrations()
        {
            // Set Meta Data 
            ViewData["Title"] = "Account Registrations";

            string AccountID = _sessionManager.LoginAccountId;
            var PendingAccountsData = _context.Accounts.Where(s => s.Active == 0).OrderByDescending(s => s.ID).ToList();
            return View(PendingAccountsData);
        }

        // POST: Admin/ApproveAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ApproveAccount()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string ApproveAccountID = HttpContext.Request.Form["ModalApproveAccountID"];
            string[] ValidationInputs = { ApproveAccountID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("AccountRegistrations", "Admin");
            }

            try
            {
                //update account status. 1 == approved
                functions.UpdateAccountStatus(ApproveAccountID, AccountID, 1);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"'{functions.GetAccountData(ApproveAccountID, "FullName")}' account has been approved by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(ApproveAccountID, AccountID, "ApproveAccount", LogAction);
                }

                //get approve account email 
                string ActionedAccountEmail = _context.Accounts.Where(s => s.AccountID == ApproveAccountID.ToString()).FirstOrDefault().Email;

                //notify account of approval
                string ToName = functions.GetAccountData(ApproveAccountID, "FullName");
                functions.NotifyAccountOfApproval(ActionedAccountEmail, ToName, _systemConfiguration.smtpEmail, functions.GetSiteLookupData("AppDomain")+"/SignIn",_systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = "Account registration approved.";
                return RedirectToAction("AccountRegistrations", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Approve Account Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("AccountRegistrations", "Admin");
            }
        }

        // POST: Admin/RejectAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult RejectAccount()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RejectAccountID = HttpContext.Request.Form["ModalRejectAccountID"];
            string[] ValidationInputs = { RejectAccountID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("AccountRegistrations", "Admin");
            }

            try
            {
                //update account status. 2 == rejected
                functions.UpdateAccountStatus(RejectAccountID, AccountID, 2);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"'{functions.GetAccountData(RejectAccountID, "FullName")}' account has been rejected by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(RejectAccountID, AccountID, "RejectAccount", LogAction);
                }

                //get rejected account email 
                string ActionedAccountEmail = _context.Accounts.Where(s => s.AccountID == RejectAccountID).FirstOrDefault().Email;

                //notify account of rejection
                string ToName = functions.GetAccountData(RejectAccountID, "FullName");
                functions.NotifyAccountOfRejection(ActionedAccountEmail, ToName, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = "Account registration rejected.";
                return RedirectToAction("AccountRegistrations", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Reject Account Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("AccountRegistrations", "Admin");
            }
        }


        // GET: Admin/ViewAccountDetails/id
        public async Task<IActionResult> ViewAccountDetails(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "View Account Details";

            string AccountID = _sessionManager.LoginAccountId;
            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            if (id == null)
            {
                return NotFound();
            }

            var accountsModel = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (accountsModel == null)
            {
                return NotFound();
            }

            ViewBag.AccountID = id;

            return View(accountsModel);
        }


        // GET: Admin/ManageAccounts
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageAccounts()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Accounts";

            string AccountID = _sessionManager.LoginAccountId;
            var AccountsData = _context.Accounts.OrderByDescending(s => s.ID).ToList();
            return View(AccountsData);
        }


        // POST: Admin/SuspendAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult SuspendAccount()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string SuspendAccountID = HttpContext.Request.Form["ModalSuspendAccountID"];
            string[] ValidationInputs = { SuspendAccountID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageAccounts", "Admin");
            }

            try
            {
                //update account status. 3 == suspended
                functions.UpdateAccountStatus(SuspendAccountID, AccountID, 3);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"'{functions.GetAccountData(SuspendAccountID, "FullName")}' account has been suspended by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(SuspendAccountID, AccountID, "SuspendAccount", LogAction);
                }

                //get to suspended account email 
                string ActionedAccountEmail = _context.Accounts.Where(s => s.AccountID == SuspendAccountID).FirstOrDefault().Email.ToString();

                //notify account of status update
                string ToName = functions.GetAccountData(SuspendAccountID, "FullName");
                functions.NotifyAccountOfSuspension(ActionedAccountEmail, ToName, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = "Account suspended.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Suspend Account Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
        }


        // POST: Admin/ActivateAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ActivateAccount()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string ActivateAccountID = HttpContext.Request.Form["ModalActivateAccountID"];
            string[] ValidationInputs = { ActivateAccountID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageAccounts", "Admin");
            }

            try
            {
                //update account status. 1 == active
                functions.UpdateAccountStatus(ActivateAccountID, AccountID, 1);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"'{functions.GetAccountData(ActivateAccountID, "FullName")}' account has been activated by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(ActivateAccountID, AccountID, "ActivateAccount", LogAction);
                }

                //get to suspended account email 
                string ActionedAccountEmail = _context.Accounts.Where(s => s.AccountID == ActivateAccountID).FirstOrDefault().Email.ToString();

                //notify account of status update
                string ToName = functions.GetAccountData(ActivateAccountID, "FullName");
                functions.NotifyAccountOfActivation(ActionedAccountEmail, ToName, _systemConfiguration.smtpEmail, functions.GetSiteLookupData("AppDomain") ,_systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = "Account activated.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Activate Account Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
        }

        // POST: Admin/RemoveAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult RemoveAccount()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveAccountID = HttpContext.Request.Form["ModalRemoveAccountID"];

            string[] ValidationInputs = { RemoveAccountID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageAccounts", "Admin");
            }

            try
            {
                //get to delete account email 
                string ActionedAccountEmail = _context.Accounts.Where(s => s.AccountID == RemoveAccountID).FirstOrDefault().Email;
                string ToName = functions.GetAccountData(RemoveAccountID, "FullName");

                //remove account
                functions.RemoveAccount(RemoveAccountID, _systemConfiguration.connectionString);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"'{ToName}' account has been removed by {functions.GetAccountData(AccountID, "FullName")}";
                    functions.LogActivity(RemoveAccountID, AccountID, "RemoveAccount", LogAction);
                }

                //notify account of removal
                functions.NotifyAccountOfRemoval(ActionedAccountEmail, ToName, _systemConfiguration.smtpEmail, _systemConfiguration.emailClosure, _systemConfiguration.emailCompany, _systemConfiguration.emailUnsubscribeLink, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);


                TempData["SuccessMessage"] = "Account removed.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Remove Account Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageAccounts", "Admin");
            }
        }


        // GET: Admin/ViewAccountPermissions/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> ViewAccountPermissions(string id, [FromQuery(Name = "edit")] string edit)
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Accounts";

            ViewBag.PermissionInputs = "disabled";
            ViewBag.EditPermissions = "false";
            ViewBag.ActionedAccountID = id;
            TempData["ActionedAccountID"] = id;

            if (!string.IsNullOrEmpty(edit) && edit == "true")
            {
                ViewBag.PermissionInputs = "";
                ViewBag.EditPermissions = "true";
            }

            string AccountID = _sessionManager.LoginAccountId;
            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            if (id == null)
            {
                return NotFound();
            }

            var accountsModel = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (accountsModel == null)
            {
                return NotFound();
            }

            return View(accountsModel);
        }



        // POST: Admin/UpdateAccountPermissions
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult UpdateAccountPermissions()
        {
            string AccountID = _sessionManager.LoginAccountId;

            //validate actioned id
            string ActionedAccountID = HttpContext.Request.Form["ActionedAccountID"];
            string ActionedAccountIDTempData = (TempData["ActionedAccountID"] != null) ? TempData["ActionedAccountID"].ToString() : "";

            if (ActionedAccountID != ActionedAccountIDTempData)
            {
                TempData["ErrorMessage"] = "Invalid account id.";
                return RedirectToAction("ViewAccountPermissions", "Admin", new { id = ActionedAccountID });
            }

            //get permissions
            //admin permissions
            string AuthorPermissions = HttpContext.Request.Form["AuthorPermissions"];

            //editor permissions
            string EditorPermissions = HttpContext.Request.Form["EditorPermissions"];

            //author permissions
            string AdminPermissions = HttpContext.Request.Form["AdminPermissions"];

            try
            {
                //update permissions
                //admin permissions
                functions.UpdatePermission("Author Permissions", AuthorPermissions, ActionedAccountID, AccountID);

                //editor permissions
                functions.UpdatePermission("Editor Permissions", EditorPermissions, ActionedAccountID, AccountID);

                //author permissions
                functions.UpdatePermission("Admin Permissions", AdminPermissions, ActionedAccountID, AccountID);

                TempData["SuccessMessage"] = "Account permissions updated.";

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' updated account permissions for user {functions.GetAccountData(ActionedAccountID, "FullName")}";
                    functions.LogActivity(ActionedAccountID, AccountID, "PermissionsUpdate", LogAction);
                }

                return RedirectToAction("ViewAccountPermissions", "Admin", new { id = ActionedAccountID });
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Update Account Permissions Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ViewAccountPermissions", "Admin", new { id = ActionedAccountID });
            }
        }






        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  



        //  ██████╗ ██████╗  ██████╗ ███████╗██╗██╗     ███████╗
        //  ██╔══██╗██╔══██╗██╔═══██╗██╔════╝██║██║     ██╔════╝
        //  ██████╔╝██████╔╝██║   ██║█████╗  ██║██║     █████╗  
        //  ██╔═══╝ ██╔══██╗██║   ██║██╔══╝  ██║██║     ██╔══╝  
        //  ██║     ██║  ██║╚██████╔╝██║     ██║███████╗███████╗
        //  ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝╚══════╝
        //    

        // GET: Admin/Profile
        public async Task<IActionResult> Profile()
        {
            // Set Meta Data 
            ViewData["Title"] = "My Profile";

            string AccountID = _sessionManager.LoginAccountId;

            var accountModel = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountID == AccountID);
            if (accountModel == null)
            {
                return NotFound();
            }

            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            return View(accountModel);
        }








        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  



        //  ███████╗███████╗████████╗████████╗██╗███╗   ██╗ ██████╗ ███████╗
        //  ██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗  ██║██╔════╝ ██╔════╝
        //  ███████╗█████╗     ██║      ██║   ██║██╔██╗ ██║██║  ███╗███████╗
        //  ╚════██║██╔══╝     ██║      ██║   ██║██║╚██╗██║██║   ██║╚════██║
        //  ███████║███████╗   ██║      ██║   ██║██║ ╚████║╚██████╔╝███████║
        //  ╚══════╝╚══════╝   ╚═╝      ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝
        //   

        // GET: Admin/Settings
        public IActionResult Settings([FromQuery(Name = "edit")] string edit)
        {
            // Set Meta Data 
            ViewData["Title"] = "Settings";

            string AccountID = _sessionManager.LoginAccountId;

            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            //checks if edit. Enable/Disable inputs and Update button
            ViewBag.EditProfile = edit;
            ViewBag.ProfileInputs = "disabled";

            if (!string.IsNullOrEmpty(edit) && edit == "true")
            {
                ViewBag.ProfileInputs = "";
            }

            //Get countries list
            ViewBag.CountriesList = functions.GetCountryList();


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Admin/UpdateProfile
        public IActionResult UpdateProfile()
        {
            string AccountID = _sessionManager.LoginAccountId;

            //Get countries list
            ViewBag.CountriesList = functions.GetCountryList();

            try
            {
                //get input values
                string FirstName = HttpContext.Request.Form["FirstName"];
                TempData["FirstName"] = FirstName;

                string LastName = HttpContext.Request.Form["LastName"];
                TempData["LastName"] = LastName;

                string CountryCode = HttpContext.Request.Form["CountryCode"];
                TempData["CountryCode"] = CountryCode;

                string PhoneNumber = HttpContext.Request.Form["PhoneNumber"];
                TempData["PhoneNumber"] = PhoneNumber;

                string DateOfBirth = HttpContext.Request.Form["DateOfBirth"];
                TempData["DateOfBirth"] = DateOfBirth;

                string Gender = HttpContext.Request.Form["Gender"];
                TempData["Gender"] = Gender;

                string Country = HttpContext.Request.Form["Country"];
                TempData["Country"] = Gender;

                string Biography = HttpContext.Request.Form["Biography"];
                TempData["Biography"] = Biography;

                string[] ValidationInputs = { FirstName, LastName, PhoneNumber, DateOfBirth, Gender, Biography };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";
                    return RedirectToAction("Settings", new { edit = "true" });
                }

                //Set post image to empty. Assigned upon upload
                string ProfileImage = "";
                //Upload profile image if uploaded
                //Image dimention
                int ImageHeight = 150;
                int ImageWidth = 150;

                //check if PostImage in model
                if (Request.Form.Files.Count > 0)
                {
                    //Saving file with resize, text and image watermark
                    var DirectoryName = _sessionManager.LoginDirectoryName;
                    var SavePath = @"wwwroot\\files\\images\\accounts\\" + DirectoryName + "\\";

                    //create directory if not exist
                    Directory.CreateDirectory(SavePath);

                    foreach (var file in Request.Form.Files)
                    {
                        if (file.Length > 0)
                        {
                            using (var stream = file.OpenReadStream())
                            {
                                using (var img = Image.FromStream(stream))
                                {
                                    string NewFileName = functions.RandomString(8) + "-" + file.FileName;

                                    img.ScaleAndCrop(ImageWidth, ImageHeight)
                                        .SaveAs(SavePath + "\\" + NewFileName);

                                    //Set profile image
                                    ProfileImage = NewFileName;
                                }
                            }
                        }
                    }
                }

                //Update values
                functions.UpdateTableData("Accounts", "AccountID", AccountID, "FirstName", FirstName, _systemConfiguration.connectionString);
                functions.UpdateTableData("Accounts", "AccountID", AccountID, "LastName", LastName, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "CountryCode", CountryCode, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "PhoneNumber", PhoneNumber, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "DateOfBirth", DateOfBirth, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "Gender", Gender, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "Country", Country, _systemConfiguration.connectionString);
                functions.UpdateTableData("AccountDetails", "AccountID", AccountID, "Biography", Biography, _systemConfiguration.connectionString);
                if (!string.IsNullOrEmpty(ProfileImage))
                {
                    functions.UpdateTableData("Accounts", "AccountID", AccountID, "ProfilePicture", ProfileImage, _systemConfiguration.connectionString);
                }

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' updated account profile details.";
                    functions.LogActivity(AccountID, AccountID, "AccountUpdate", LogAction);
                }

                TempData["SuccessMessage"] = "Account details updated.";
                return RedirectToAction("Settings", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Update Account Details Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("Settings", new { edit = "true" });
            }
        }



        //Checks if user password already exists
        public ActionResult CheckExistingPassword(string key)
        {
            string AccountID = _sessionManager.LoginAccountId;

            // get password
            var query = _context.Accounts.Where(s => s.AccountID == AccountID);
            string hashedPassword = (query.Any()) ? query.FirstOrDefault().Password : "";

            //check in db and return "exists" if record exists
            if (!string.IsNullOrEmpty(key))
            {
                if (BCrypt.Net.BCrypt.Verify(key, hashedPassword))
                {
                    return Json("exists");
                }
            }
            return Json("");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Admin/UpdatePassword
        public IActionResult UpdatePassword()
        {
            string AccountID = _sessionManager.LoginAccountId;

            try
            {
                //get input values
                string CurrentPassword = HttpContext.Request.Form["CurrentPassword"];
                string NewPassword = HttpContext.Request.Form["Password"];
                string ConfirmPassword = HttpContext.Request.Form["ConfirmPassword"];

                string[] ValidationInputs = { CurrentPassword, NewPassword, ConfirmPassword };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";
                    return RedirectToAction("Settings", new { tab = "PTab" });
                }


                //verify password match
                if (!functions.PasswordsMatch(NewPassword, ConfirmPassword))
                {
                    TempData["ErrorMessage"] = "Passwords do not match";
                    return RedirectToAction("Settings", new { tab = "PTab" });
                }

                // get password
                var query = _context.Accounts.Where(s => s.AccountID == AccountID);
                string hashedPassword = (query.Any()) ? query.FirstOrDefault().Password : "";

                //check in db and return "exists" if record exists
                if (BCrypt.Net.BCrypt.Verify(NewPassword, hashedPassword))
                {
                    TempData["ErrorMessage"] = "Please choose a different password from the current one";
                    return RedirectToAction("Settings", new { tab = "PTab" });
                }

                //Update values
                NewPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                functions.UpdateTableData("Accounts", "AccountID", AccountID, "Password", NewPassword, _systemConfiguration.connectionString);

                TempData["SuccessMessage"] = "Account password updated.";

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' updated account password.";
                    functions.LogActivity(AccountID, AccountID, "PasswordUpdate", LogAction);
                }

                return RedirectToAction("Settings", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Update Account Password Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("Settings", new { tab = "PTab" });
            }
        }







        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  



        //   ██████╗ █████╗ ████████╗███████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗
        //  ██╔════╝██╔══██╗╚══██╔══╝██╔════╝██╔════╝ ██╔═══██╗██╔══██╗╚██╗ ██╔╝
        //  ██║     ███████║   ██║   █████╗  ██║  ███╗██║   ██║██████╔╝ ╚████╔╝ 
        //  ██║     ██╔══██║   ██║   ██╔══╝  ██║   ██║██║   ██║██╔══██╗  ╚██╔╝  
        //  ╚██████╗██║  ██║   ██║   ███████╗╚██████╔╝╚██████╔╝██║  ██║   ██║   
        //   ╚═════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═╝   ╚═╝   
        //                                                                      

        // GET: Manage Categories
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageCategories()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Categories";

            string AccountID = _sessionManager.LoginAccountId;
            var CategoriesData = _context.Categories.ToList();
            return View(CategoriesData);
        }

        // GET: Admin/ViewCategoryDetails/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> ViewCategoryDetails(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "View Category Details";

            string AccountID = _sessionManager.LoginAccountId;
            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            if (id == null)
            {
                return NotFound();
            }

            var categoriesModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (categoriesModel == null)
            {
                return NotFound();
            }

            return View(categoriesModel);
        }

        // GET: Create Category
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateCategory()
        {
            // Set Meta Data 
            ViewData["Title"] = "Create Category";

            string AccountID = _sessionManager.LoginAccountId;

            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageCategories";

            return View();
        }

        // POST: Admin/CreatePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateCategory(CategoriesModel categoriesModel)
        {
            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();

            try
            {
                string AccountID = _sessionManager.LoginAccountId;

                if (ModelState.IsValid)
                {
                    if (_context.Categories.Any(s => s.CategoryName == categoriesModel.CategoryName))
                    {
                        TempData["ErrorMessage"] = "Category with the same name already exist.";
                        return View(categoriesModel);
                    }

                    //get input values
                    categoriesModel.CategoryID = functions.GetGuid();
                    categoriesModel.CategoryName = HttpContext.Request.Form["CategoryName"];
                    categoriesModel.CategoryDescription = HttpContext.Request.Form["CategoryDescription"];
                    categoriesModel.CategoryParent = HttpContext.Request.Form["CategoryParent"];
                    categoriesModel.CategoryIcon = HttpContext.Request.Form["CategoryIcon"];
                    categoriesModel.CategoryOrder = functions.Int32Parse(HttpContext.Request.Form["CategoryOrder"]);
                    categoriesModel.ShortCategoryName = functions.ConvertCase(categoriesModel.CategoryName, "TitleTrim");

                    categoriesModel.IsPublished = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["IsPublished"]))
                    {
                        categoriesModel.IsPublished = functions.Int32Parse(HttpContext.Request.Form["IsPublished"]);
                    }

                    categoriesModel.IsHeader = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["IsHeader"]))
                    {
                        categoriesModel.IsHeader = functions.Int32Parse(HttpContext.Request.Form["IsHeader"]);
                    }

                    categoriesModel.UpdatedBy = AccountID;
                    categoriesModel.UpdateDate = DateTime.Now;

                    _context.Add(categoriesModel);
                    _context.SaveChanges();

                    //update other categories order
                    functions.UpdateOtherCategoriesOrder(categoriesModel.CategoryID, categoriesModel.CategoryOrder);

                    TempData["SuccessMessage"] = "Category added successfully.";

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created category '{categoriesModel.CategoryName}'";
                        functions.LogActivity(AccountID, AccountID, "NewCategory", LogAction);
                    }

                    return RedirectToAction("ManageCategories", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create Category Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(categoriesModel);
        }


        // GET: Admin/EditCategory/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditCategory(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Category";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageCategories";

            return View(categoryModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditCategory(CategoriesModel categoriesModel)
        {
            string AccountID = _sessionManager.LoginAccountId;
            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();

            try
            {
                if (ModelState.IsValid)
                {
                    //reset post id
                    categoriesModel.ID = _context.Categories.Where(s => s.CategoryID == categoriesModel.CategoryID).FirstOrDefault().ID;

                    if (_context.Categories.Any(s => s.CategoryName == categoriesModel.CategoryName && s.CategoryID != categoriesModel.CategoryID))
                    {
                        TempData["ErrorMessage"] = "Another category with the same name already exist.";
                        return View(categoriesModel);
                    }

                    //get input values
                    categoriesModel.CategoryName = HttpContext.Request.Form["CategoryName"];
                    categoriesModel.CategoryDescription = HttpContext.Request.Form["CategoryDescription"];
                    categoriesModel.CategoryParent = HttpContext.Request.Form["CategoryParent"];
                    categoriesModel.CategoryIcon = HttpContext.Request.Form["CategoryIcon"];
                    categoriesModel.CategoryOrder = functions.Int32Parse(HttpContext.Request.Form["CategoryOrder"]);
                    categoriesModel.ShortCategoryName = functions.ConvertCase(categoriesModel.CategoryName, "TitleTrim");

                    categoriesModel.IsPublished = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["IsPublished"]))
                    {
                        categoriesModel.IsPublished = functions.Int32Parse(HttpContext.Request.Form["IsPublished"]);
                    }

                    categoriesModel.IsHeader = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Request.Form["IsHeader"]))
                    {
                        categoriesModel.IsHeader = functions.Int32Parse(HttpContext.Request.Form["IsHeader"]);
                    }

                    functions.UpdateCategories(categoriesModel.CategoryID, categoriesModel.CategoryName, categoriesModel.ShortCategoryName, categoriesModel.CategoryParent, categoriesModel.CategoryDescription,
                        categoriesModel.CategoryIcon, categoriesModel.CategoryOrder, categoriesModel.IsPublished, categoriesModel.IsHeader, AccountID);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited category '{categoriesModel.CategoryName}'";
                        functions.LogActivity(AccountID, AccountID, "EditCategory", LogAction);
                    }

                    TempData["SuccessMessage"] = "Category updated successfully.";
                    return RedirectToAction("ManageCategories", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Edit Category Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(categoriesModel);
        }


        // POST: Admin/DeleteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult DeleteCategory()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveCategoryID = HttpContext.Request.Form["ModalDeleteCategoryID"];

            string[] ValidationInputs = { RemoveCategoryID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageCategories", "Admin");
            }

            try
            {
                string CategoryName = _context.Categories.Where(s => s.CategoryID == RemoveCategoryID).FirstOrDefault().CategoryName;

                //remove category
                functions.DeleteCategory(RemoveCategoryID);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"{functions.GetAccountData(AccountID, "FullName")} has removed category {CategoryName}";
                    functions.LogActivity(AccountID, AccountID, "RemoveCategory", LogAction);
                }

                TempData["SuccessMessage"] = "Category deleted.";
                return RedirectToAction("ManageCategories", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Category Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageCategories", "Admin");
            }
        }


        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  


        //  ████████╗ █████╗  ██████╗ ███████╗
        //  ╚══██╔══╝██╔══██╗██╔════╝ ██╔════╝
        //     ██║   ███████║██║  ███╗███████╗
        //     ██║   ██╔══██║██║   ██║╚════██║
        //     ██║   ██║  ██║╚██████╔╝███████║
        //     ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚══════╝
        //                                    

        // GET: Manage Tags
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageTags()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Tags";

            string AccountID = _sessionManager.LoginAccountId;
            var TagsData = _context.Tags.ToList();
            return View(TagsData);
        }


        // GET: Admin/ViewTagDetails/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> ViewTagDetails(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Tag Details";

            string AccountID = _sessionManager.LoginAccountId;
            //get connection string from app settings
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            if (id == null)
            {
                return NotFound();
            }

            var tagsModel = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagID == id);
            if (tagsModel == null)
            {
                return NotFound();
            }

            return View(tagsModel);
        }


        // GET: Create Tag
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateTag()
        {
            // Set Meta Data 
            ViewData["Title"] = "Create Tag";

            string AccountID = _sessionManager.LoginAccountId;

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageTags";

            return View();
        }



        // POST: Admin/CreateTag
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateTag(TagsModel tagsModel)
        {
            try
            {
                string AccountID = _sessionManager.LoginAccountId;

                if (ModelState.IsValid)
                {
                    if (_context.Tags.Any(s => s.TagName == tagsModel.TagName))
                    {
                        TempData["ErrorMessage"] = "Tag with the same name already exist.";
                        return View(tagsModel);
                    }

                    //get input values
                    tagsModel.TagID = functions.GetGuid();
                    tagsModel.TagName = HttpContext.Request.Form["TagName"];
                    tagsModel.TagDescription = HttpContext.Request.Form["TagDescription"];
                    tagsModel.ShortTagName = functions.ConvertCase(tagsModel.TagName, "TitleTrim");

                    tagsModel.UpdatedBy = AccountID;
                    tagsModel.UpdateDate = DateTime.Now;

                    _context.Add(tagsModel);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Tag added successfully.";

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created tag '{tagsModel.TagName}'";
                        functions.LogActivity(AccountID, AccountID, "NewTag", LogAction);
                    }

                    return RedirectToAction("ManageTags", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create Tag Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(tagsModel);
        }


        // GET: Admin/EditTag/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditTag(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Tags";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var TagModel = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagID == id);
            if (TagModel == null)
            {
                return NotFound();
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageTags";

            return View(TagModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditTag(TagsModel tagsModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            try
            {
                if (ModelState.IsValid)
                {
                    //reset post id
                    tagsModel.ID = _context.Tags.Where(s => s.TagID == tagsModel.TagID).FirstOrDefault().ID;

                    if (_context.Tags.Any(s => s.TagName == tagsModel.TagName && s.TagID != tagsModel.TagID))
                    {
                        TempData["ErrorMessage"] = "Another tag with the same name already exist.";
                        return View(tagsModel);
                    }

                    //get input values
                    tagsModel.TagName = HttpContext.Request.Form["TagName"];
                    tagsModel.TagDescription = HttpContext.Request.Form["TagDescription"];
                    tagsModel.ShortTagName = functions.ConvertCase(tagsModel.TagName, "TitleTrim");

                    functions.UpdateTags(tagsModel.TagID, tagsModel.TagName, tagsModel.TagDescription, AccountID);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited tag '{tagsModel.TagName}'";
                        functions.LogActivity(AccountID, AccountID, "EditTag", LogAction);
                    }

                    TempData["SuccessMessage"] = "Tag updated successfully.";
                    return RedirectToAction("ManageTags", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Edit Tag Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(tagsModel);
        }


        // POST: Admin/DeleteTag
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult DeleteTag()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveTagID = HttpContext.Request.Form["ModalDeleteTagID"];

            string[] ValidationInputs = { RemoveTagID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageTags", "Admin");
            }

            try
            {
                string TagName = _context.Tags.Where(s => s.TagID == RemoveTagID).FirstOrDefault().TagName;

                //remove tag
                functions.DeleteTag(RemoveTagID);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"{functions.GetAccountData(AccountID, "FullName")} has removed tag {TagName}";
                    functions.LogActivity(AccountID, AccountID, "DeleteTag", LogAction);
                }

                TempData["SuccessMessage"] = "Tag deleted.";
                return RedirectToAction("ManageTags", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Tag Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageTags", "Admin");
            }
        }



        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  

        //   █████╗ ██████╗ ██████╗      ██████╗ ██████╗ ███╗   ██╗███████╗██╗ ██████╗ 
        //  ██╔══██╗██╔══██╗██╔══██╗    ██╔════╝██╔═══██╗████╗  ██║██╔════╝██║██╔════╝ 
        //  ███████║██████╔╝██████╔╝    ██║     ██║   ██║██╔██╗ ██║█████╗  ██║██║  ███╗
        //  ██╔══██║██╔═══╝ ██╔═══╝     ██║     ██║   ██║██║╚██╗██║██╔══╝  ██║██║   ██║
        //  ██║  ██║██║     ██║         ╚██████╗╚██████╔╝██║ ╚████║██║     ██║╚██████╔╝
        //  ╚═╝  ╚═╝╚═╝     ╚═╝          ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝     ╚═╝ ╚═════╝ 
        //                                                                             

        // GET: App Configurations
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageSiteData([FromQuery(Name = "group")] string group)
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Site Data";

            string AccountID = _sessionManager.LoginAccountId;

            var ConfigData = _context.SiteDataLookup.Where(s=> s.UinqueKey != "AboutUsPage").OrderBy(x=> x.UinqueKey).ToList();
            ViewBag.ShowDataGroup = "";
            if (!string.IsNullOrEmpty(group))
            {
                ConfigData = _context.SiteDataLookup.Where(s => s.UinqueKey != "AboutUsPage" && s.DataGroup == group).OrderBy(x => x.UinqueKey).ToList();
                ViewBag.ShowDataGroup = "d-none";
            }
            return View(ConfigData);
        }

        // GET: Admin/EditTag/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditSiteData(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Site Data";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var SiteDataModel = await _context.SiteDataLookup
                .FirstOrDefaultAsync(m => m.UinqueKey == id);
            if (SiteDataModel == null)
            {
                return NotFound();
            }

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageSiteData";

            return View(SiteDataModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditSiteData()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string SiteDataUinqueKey = HttpContext.Request.Form["UinqueKey"];
            string SiteDataValue = HttpContext.Request.Form["Value"];

            try
            {
                //update site data
                SiteDataLookupModel nSiteData = _context.SiteDataLookup.Single(u => u.UinqueKey == SiteDataUinqueKey);
                nSiteData.Value = SiteDataValue;
                _context.SaveChanges();

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' updated site data for '{SiteDataUinqueKey}'";
                    functions.LogActivity(AccountID, AccountID, "EditSiteData", LogAction);
                }

                TempData["SuccessMessage"] = "Site data updated.";
                return RedirectToAction("ManageSiteData", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Update Site Data Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("EditSiteData", "Admin", new { id = SiteDataUinqueKey });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult UpdateSiteData() 
        {
            string AccountID = _sessionManager.LoginAccountId;

            string ModalSiteDataUinqueKey = HttpContext.Request.Form["ModalSiteDataUinqueKey"];
            string ModalSiteDataValue = HttpContext.Request.Form["ModalSiteDataValue"];
            string PostContent = HttpContext.Request.Form["PostContent"];
            if (!string.IsNullOrEmpty(PostContent))
            {
                ModalSiteDataValue = PostContent; //setting ModalSiteDataValue if editing about data
            }

            try
            {
                //update site data
                SiteDataLookupModel nSiteData = _context.SiteDataLookup.Single(u => u.UinqueKey == ModalSiteDataUinqueKey);
                nSiteData.Value = ModalSiteDataValue;
                _context.SaveChanges();

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' updated site data for '{ModalSiteDataUinqueKey}'";
                    functions.LogActivity(AccountID, AccountID, "EditSiteData", LogAction);
                }

                TempData["SuccessMessage"] = "Site data updated.";
                return RedirectToAction("ManageSiteData", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Update Site Data Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageSiteData", "Admin");
            }

        }





        //   ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗  ██╗ ██╗ 
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //  ████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗████████╗
        //  ╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝╚██╔═██╔╝
        //   ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═╝ 
        //  
        //  ████████╗ ██████╗ ██████╗      ██╗ ██████╗ 
        //  ╚══██╔══╝██╔═══██╗██╔══██╗    ███║██╔═████╗
        //     ██║   ██║   ██║██████╔╝    ╚██║██║██╔██║
        //     ██║   ██║   ██║██╔═══╝      ██║████╔╝██║
        //     ██║   ╚██████╔╝██║          ██║╚██████╔╝
        //     ╚═╝    ╚═════╝ ╚═╝          ╚═╝ ╚═════╝ 
        //  
        
        // GET: Manage TopTen Video List 
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult ManageTopTenVideos()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Top Ten Videos";

            string AccountID = _sessionManager.LoginAccountId;

            var TopTenListData = _context.TopTenList.Where(s=> s.ListType == "MusicVideos").OrderBy(s=> s.ListOrder).ToList();
            return View(TopTenListData);
        }



        // GET: Admin/EditTopTenVideo/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditTopTenVideo(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Top Ten Video";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var videoListModel = await _context.TopTenList
                .FirstOrDefaultAsync(m => m.ListID == id);
            if (videoListModel == null)
            {
                return NotFound();
            }

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "Edit Top Ten Video";

            return View(videoListModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditTopTenVideo(TopTenListModel videoListModel)
        {
            string AccountID = _sessionManager.LoginAccountId;
            try
            {
                if (ModelState.IsValid)
                {
                    //reset post id
                    videoListModel.ID = _context.TopTenList.Where(s => s.ListID == videoListModel.ListID).FirstOrDefault().ID;

                    if (_context.TopTenList.Any(s => (s.ListLink == videoListModel.ListLink && s.ListType == videoListModel.ListType) && s.ListID != videoListModel.ListID))
                    {
                        TempData["ErrorMessage"] = "Another video list with the same link already exist.";
                        return View(videoListModel);
                    }


                    TopTenListModel VideoList = _context.TopTenList.Single(u => u.ID == videoListModel.ID);

                    //set input values
                    VideoList.ListID = HttpContext.Request.Form["ListID"];
                    VideoList.ListType = HttpContext.Request.Form["ListType"];
                    VideoList.ListOrder = functions.Int32Parse(HttpContext.Request.Form["ListOrder"]);
                    VideoList.ListLink = HttpContext.Request.Form["ListLink"];
                    VideoList.ListTitle = HttpContext.Request.Form["ListTitle"];
                    VideoList.UpdatedBy = AccountID;
                    VideoList.UpdateDate = DateTime.Now;

                    await _context.SaveChangesAsync();


                    //update other list
                    //functions.UpdateOtherListOrder(videoListModel.ListID, videoListModel.ListType, videoListModel.ListOrder);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited video list '{videoListModel.ListLink}'";
                        functions.LogActivity(AccountID, AccountID, "EditTopTenVideo", LogAction);
                    }

                    TempData["SuccessMessage"] = "Video list updated successfully.";
                    return RedirectToAction("ManageTopTenVideos", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Edit Category Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(videoListModel);
        }



        // GET: Manage Embedded Music
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult ManageEmbeddedMusic()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Embedded Music";

            string AccountID = _sessionManager.LoginAccountId;

            var EmbeddedMusicData = _context.EmbeddedMusic.OrderByDescending(s => s.UpdateDate).ToList();
            return View(EmbeddedMusicData);
        }

        // GET: Create CreateEmbeddedMusic
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult CreateEmbeddedMusic()
        {
            // Set Meta Data 
            ViewData["Title"] = "Create Embedded Music";

            string AccountID = _sessionManager.LoginAccountId;

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageEmbeddedMusic";

            return View();
        }



        // POST: Admin/CreateEmbeddedMusic
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult CreateEmbeddedMusic(EmbeddedMusicModel embeddedMusicModel)
        {
            //get form select drop down lists
            ViewBag.CategoryList = functions.GetCategoryList();

            try
            {
                string AccountID = _sessionManager.LoginAccountId;

                if (ModelState.IsValid)
                {
                    //get input values
                    embeddedMusicModel.EmbedID = functions.GetGuid();
                    embeddedMusicModel.EmbedTitle = HttpContext.Request.Form["EmbedTitle"];
                    embeddedMusicModel.EmbedType = HttpContext.Request.Form["EmbedType"];
                    embeddedMusicModel.EmbedCode = HttpContext.Request.Form["EmbedCode"];

                    embeddedMusicModel.UpdatedBy = AccountID;
                    embeddedMusicModel.UpdateDate = DateTime.Now;

                    _context.Add(embeddedMusicModel);
                    _context.SaveChanges();


                    TempData["SuccessMessage"] = "Embed music code added successfully.";

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created embed music titled '{embeddedMusicModel.EmbedTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewCategory", LogAction);
                    }

                    return RedirectToAction("ManageEmbeddedMusic", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create Embed Music Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(embeddedMusicModel);
        }



        // GET: Admin/EditEmbeddedMusic/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditEmbeddedMusic(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Embedded Music Category";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var embeddedMusicModel = await _context.EmbeddedMusic
                .FirstOrDefaultAsync(m => m.EmbedID == id);
            if (embeddedMusicModel == null)
            {
                return NotFound();
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageEmbeddedMusic";

            return View(embeddedMusicModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public async Task<IActionResult> EditEmbeddedMusic(EmbeddedMusicModel embeddedMusicModel)
        {
            string AccountID = _sessionManager.LoginAccountId;
            try
            {
                if (ModelState.IsValid)
                {
                    //reset post id
                    embeddedMusicModel.ID = _context.EmbeddedMusic.Where(s => s.EmbedID == embeddedMusicModel.EmbedID).FirstOrDefault().ID;

                    EmbeddedMusicModel EmbeddedMusicData = _context.EmbeddedMusic.Single(u => u.ID == embeddedMusicModel.ID);

                    //set input values
                    EmbeddedMusicData.EmbedID = HttpContext.Request.Form["EmbedID"];
                    EmbeddedMusicData.EmbedTitle = HttpContext.Request.Form["EmbedTitle"];
                    EmbeddedMusicData.EmbedType = HttpContext.Request.Form["EmbedType"];
                    EmbeddedMusicData.EmbedCode = HttpContext.Request.Form["EmbedCode"];
                    EmbeddedMusicData.UpdatedBy = AccountID;
                    EmbeddedMusicData.UpdateDate = DateTime.Now;

                    await _context.SaveChangesAsync();

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited embed music titled '{embeddedMusicModel.EmbedTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditEmbeddedMusic", LogAction);
                    }

                    TempData["SuccessMessage"] = "Music embed updated successfully.";
                    return RedirectToAction("ManageEmbeddedMusic", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Edit Embed Music Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(embeddedMusicModel);
        }


        // POST: Admin/DeleteEmbeddedMusic
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Author Permissions")]
        public IActionResult DeleteEmbeddedMusic()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveEmbeddedMusicID = HttpContext.Request.Form["ModalDeleteEmbeddedMusicID"];

            string[] ValidationInputs = { RemoveEmbeddedMusicID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageEmbeddedMusic", "Admin");
            }

            try
            {
                string EmbedTitle = _context.EmbeddedMusic.Where(s => s.EmbedID == RemoveEmbeddedMusicID).FirstOrDefault().EmbedTitle;

                //remove embedded music
                functions.DeleteTableData("EmbeddedMusic", "EmbedID", RemoveEmbeddedMusicID, _systemConfiguration.connectionString);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"{functions.GetAccountData(AccountID, "FullName")} has removed embed music titled {EmbedTitle}";
                    functions.LogActivity(AccountID, AccountID, "RemoveEmbedMusic", LogAction);
                }

                TempData["SuccessMessage"] = "Embedded music deleted.";
                return RedirectToAction("ManageEmbeddedMusic", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Embedded Music Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageEmbeddedMusic", "Admin");
            }
        }

        //   █████╗ ██████╗ ██╗   ██╗███████╗██████╗ ████████╗███████╗
        //  ██╔══██╗██╔══██╗██║   ██║██╔════╝██╔══██╗╚══██╔══╝██╔════╝
        //  ███████║██║  ██║██║   ██║█████╗  ██████╔╝   ██║   ███████╗
        //  ██╔══██║██║  ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗   ██║   ╚════██║
        //  ██║  ██║██████╔╝ ╚████╔╝ ███████╗██║  ██║   ██║   ███████║
        //  ╚═╝  ╚═╝╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝
        //                                                            

        // GET: ManageAdverts
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageAdverts()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Site Data";

            string AccountID = _sessionManager.LoginAccountId;
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            var data = _context.Adverts.OrderByDescending(s=> s.DateAdded).ToList();

            return View(data);
        }

        // GET: CreateAdvert
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateAdvert()
        {
            string AccountID = _sessionManager.LoginAccountId;
            return View();
        }

        // POST: Admin/CreateAdvert
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> CreateAdvert(AdvertsModel advertsModel, List<IFormFile> AdvertImage)
        {
            // Set Meta Data 
            ViewData["Title"] = "Create Advert";

            string AccountID = _sessionManager.LoginAccountId;
            if (ModelState.IsValid)
            {
                try
                {
                    advertsModel.AdvertID = functions.GetGuid();
                    advertsModel.AdvertPermalink = functions.GeneratePostPermalink(advertsModel.AdvertTitle);
                    advertsModel.PostBy = AccountID;
                    advertsModel.DateAdded = DateTime.Now;

                    //upload image
                    if (AdvertImage != null)
                    {
                        string fileName = functions.UploadAdvertMedia(AdvertImage);
                        advertsModel.AdvertImage = fileName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No image selected.";
                        return RedirectToAction("ManageAdverts", "Admin");
                    }

                    //add record
                    _context.Add(advertsModel);
                    await _context.SaveChangesAsync();

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created advert '{advertsModel.AdvertTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewAdvert", LogAction);
                    }

                    TempData["SuccessMessage"] = "Advert added";
                    return RedirectToAction("ManageAdverts", "Admin");
                }
                catch (Exception ex)
                {
                    //log error
                    _logger.LogInformation("New Advert Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                    return RedirectToAction("ManageAdverts", "Admin");
                }
            }
            else
            {
                return View(advertsModel);
            }
        }


        // GET: Admin/EditAdvert
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditAdvert(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Advert";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var advertsModel = _context.Adverts
                .FirstOrDefault(m => m.AdvertID == id);
            if (advertsModel == null)
            {
                return NotFound();
            }

            return View(advertsModel);
        }

        // POST: Admin/EditAdvert
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditAdvert(AdvertsModel advertsModel, List<IFormFile> AdvertImage)
        {
            string AccountID = _sessionManager.LoginAccountId;
            if (ModelState.IsValid)
            {
                try
                {
                    advertsModel.AdvertPermalink = functions.GeneratePostPermalink(advertsModel.AdvertTitle);
                    advertsModel.PostBy = AccountID;

                    //upload image
                    if (AdvertImage != null)
                    {
                        string fileName = functions.UploadAdvertMedia(AdvertImage);
                        advertsModel.AdvertImage = fileName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No image selected.";
                        return RedirectToAction("ManageAdverts", "Admin");
                    }

                    //update record
                    _context.Update(advertsModel);
                    await _context.SaveChangesAsync();

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited advert '{advertsModel.AdvertTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditAdvert", LogAction);
                    }

                    TempData["SuccessMessage"] = "Advert updated";
                    return RedirectToAction("ManageAdverts", "Admin");
                }
                catch (Exception ex)
                {
                    //log error
                    _logger.LogInformation("Update Advert Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                    return RedirectToAction("ManageAdverts", "Admin");
                }
            }
            else
            {
                return View(advertsModel);
            }
        }



        // POST: Admin/DeleteAdvert
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult DeleteAdvert()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveAdvertID = HttpContext.Request.Form["ModalDeleteAdvertID"];

            string[] ValidationInputs = { RemoveAdvertID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageAdverts", "Admin");
            }

            try
            {
                string AdvertTitle = _context.Adverts.Where(s => s.AdvertID == RemoveAdvertID).FirstOrDefault().AdvertTitle;

                //remove advert
                AdvertsModel advert = _context.Adverts.Single(u => u.AdvertID == RemoveAdvertID);
                _context.Adverts.Remove(advert);
                _context.SaveChanges();


                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"{functions.GetAccountData(AccountID, "FullName")} has removed advert {AdvertTitle}";
                    functions.LogActivity(AccountID, AccountID, "DeleteAdvert", LogAction);
                }

                TempData["SuccessMessage"] = "Advert deleted.";
                return RedirectToAction("ManageAdverts", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Advert Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageAdverts", "Admin");
            }
        }



        //       ██╗ ██████╗ ██████╗ ███████╗
        //       ██║██╔═══██╗██╔══██╗██╔════╝
        //       ██║██║   ██║██████╔╝███████╗
        //  ██   ██║██║   ██║██╔══██╗╚════██║
        //  ╚█████╔╝╚██████╔╝██████╔╝███████║
        //   ╚════╝  ╚═════╝ ╚═════╝ ╚══════╝
        //                                   
        // GET: ManageJobs
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult ManageJobs()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Jobs";

            string AccountID = _sessionManager.LoginAccountId;
            ViewBag.ConnectionString = _systemConfiguration.connectionString;

            var data = _context.Jobs.OrderByDescending(s=> s.DateAdded).ToList();

            return View(data);
        }

        // GET: CreateAdvert
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateJob()
        {
            // Set Meta Data 
            ViewData["Title"] = "Create Job";

            string AccountID = _sessionManager.LoginAccountId;
            return View();
        }

        // POST: Admin/CreateJob
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> CreateJob(JobsModel jobsModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            if (ModelState.IsValid)
            {
                try
                {
                    jobsModel.JobID = functions.GetGuid();
                    jobsModel.JobAdvertPermalink = functions.GeneratePostPermalink(jobsModel.JobTitle);
                    jobsModel.PostBy = AccountID;
                    jobsModel.DateAdded = DateTime.Now;


                    //add record
                    _context.Add(jobsModel);
                    await _context.SaveChangesAsync();

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created job '{jobsModel.JobTitle}'";
                        functions.LogActivity(AccountID, AccountID, "NewJob", LogAction);
                    }

                    TempData["SuccessMessage"] = "Job added";
                    return RedirectToAction("ManageJobs", "Admin");
                }
                catch (Exception ex)
                {
                    //log error
                    _logger.LogInformation("New Job Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                    return RedirectToAction("ManageJobs", "Admin");
                }
            }

            return View(jobsModel);
        }


        // GET: Admin/EditJob
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditJob(string id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit Job";

            string AccountID = _sessionManager.LoginAccountId;

            if (id == null)
            {
                return NotFound();
            }

            var jobsModel = _context.Jobs
                .FirstOrDefault(m => m.JobID == id);
            if (jobsModel == null)
            {
                return NotFound();
            }

            return View(jobsModel);
        }

        // POST: Admin/EditJob
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditJob(JobsModel jobsModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            if (ModelState.IsValid)
            {
                try
                {
                    jobsModel.JobAdvertPermalink = functions.GeneratePostPermalink(jobsModel.JobTitle);
                    jobsModel.PostBy = AccountID;


                    //update record
                    _context.Update(jobsModel);
                    await _context.SaveChangesAsync();

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited job '{jobsModel.JobTitle}'";
                        functions.LogActivity(AccountID, AccountID, "EditJob", LogAction);
                    }

                    TempData["SuccessMessage"] = "Job updated";
                    return RedirectToAction("ManageJobs", "Admin");
                }
                catch (Exception ex)
                {
                    //log error
                    _logger.LogInformation("Update Job Post Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                    return RedirectToAction("ManageJobs", "Admin");
                }
            }
            else
            {
                return View(jobsModel);
            }
            
        }



        // POST: Admin/DeleteJob
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult DeleteJob()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveJobID = HttpContext.Request.Form["ModalDeleteJobID"];

            string[] ValidationInputs = { RemoveJobID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageJobs", "Admin");
            }

            try
            {
                //remove job
                JobsModel job = _context.Jobs.Single(u => u.JobID == RemoveJobID);
                _context.Jobs.Remove(job);
                _context.SaveChanges();

                //TODO log activity

                TempData["SuccessMessage"] = "Job deleted.";
                return RedirectToAction("ManageJobs", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Job Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageJobs", "Admin");
            }
        }


        //   ██████╗██╗  ██╗ █████╗ ████████╗███████╗
        //  ██╔════╝██║  ██║██╔══██╗╚══██╔══╝██╔════╝
        //  ██║     ███████║███████║   ██║   ███████╗
        //  ██║     ██╔══██║██╔══██║   ██║   ╚════██║
        //  ╚██████╗██║  ██║██║  ██║   ██║   ███████║
        //   ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝
        //        
        // GET: Chat TODO
        public IActionResult Chat()
        {
            // Set Meta Data 
            ViewData["Title"] = "Chat";

            return RedirectToAction("Index", "Admin");
            //return View();
        }




        //   █████╗ ██████╗ ██████╗     ██╗   ██╗███████╗███████╗██████╗ ███████╗
        //  ██╔══██╗██╔══██╗██╔══██╗    ██║   ██║██╔════╝██╔════╝██╔══██╗██╔════╝
        //  ███████║██████╔╝██████╔╝    ██║   ██║███████╗█████╗  ██████╔╝███████╗
        //  ██╔══██║██╔═══╝ ██╔═══╝     ██║   ██║╚════██║██╔══╝  ██╔══██╗╚════██║
        //  ██║  ██║██║     ██║         ╚██████╔╝███████║███████╗██║  ██║███████║
        //  ╚═╝  ╚═╝╚═╝     ╚═╝          ╚═════╝ ╚══════╝╚══════╝╚═╝  ╚═╝╚══════╝
        //                                                                       
        // GET: AppUsers
        public IActionResult AppUsers()
        {
            string AccountID = _sessionManager.LoginAccountId;
            var AccountsData = _context.Accounts.Where(s=> s.Active == 1).OrderByDescending(s => s.ID).ToList();
            return View(AccountsData);
        }





        //  ███╗   ██╗███████╗██╗    ██╗███████╗     █████╗ ██████╗ ██╗
        //  ████╗  ██║██╔════╝██║    ██║██╔════╝    ██╔══██╗██╔══██╗██║
        //  ██╔██╗ ██║█████╗  ██║ █╗ ██║███████╗    ███████║██████╔╝██║
        //  ██║╚██╗██║██╔══╝  ██║███╗██║╚════██║    ██╔══██║██╔═══╝ ██║
        //  ██║ ╚████║███████╗╚███╔███╔╝███████║    ██║  ██║██║     ██║
        //  ╚═╝  ╚═══╝╚══════╝ ╚══╝╚══╝ ╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝
        //                                                             

        // GET: ManageNewsApi
        public IActionResult ManageNewsApi()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage News Api";

            string AccountID = _sessionManager.LoginAccountId;
            var NewsApiData = _context.NewsApi.OrderByDescending(s=> s.ID).ToList();
            return View(NewsApiData);
        }

        // POST: Admin/DeleteNewsApi
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult DeleteNewsApi()
        {
            string AccountID = _sessionManager.LoginAccountId;

            string RemoveNewsApiID = HttpContext.Request.Form["ModalDeleteNewsApiID"];

            string[] ValidationInputs = { RemoveNewsApiID };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageNewsApi", "Admin");
            }

            try
            {
                string UrlLink = _context.NewsApi.Where(s => s.ID == Int32.Parse(RemoveNewsApiID)).FirstOrDefault().Url;

                //remove news api
                functions.DeleteTableData("NewsApi", "ID", RemoveNewsApiID, _systemConfiguration.connectionString);

                //log activity
                if (_systemConfiguration.logActivity)
                {
                    string LogAction = $@"{functions.GetAccountData(AccountID, "FullName")} has removed news api with url {UrlLink}";
                    functions.LogActivity(AccountID, AccountID, "DeleteNewsApi", LogAction);
                }

                TempData["SuccessMessage"] = "News Api deleted.";
                return RedirectToAction("ManageNewsApi", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete News Api Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageNewsApi", "Admin");
            }
        }


        // GET: Create NewsAPi
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateNewsAPi()
        {
            // Set Meta Data 
            ViewData["Title"] = "Create NewsAPi";

            string AccountID = _sessionManager.LoginAccountId;

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageNewsAPi";

            return View();
        }



        // POST: Admin/CreateNewsAPi
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult CreateNewsAPi(NewsApiModel newsApiModel)
        {
            try
            {
                string AccountID = _sessionManager.LoginAccountId;

                if (ModelState.IsValid)
                {
                    if (_context.NewsApi.Any(s => s.Url == newsApiModel.Url))
                    {
                        TempData["ErrorMessage"] = "Post with the same url already exist.";
                        return View(newsApiModel);
                    }

                    //get input values
                    newsApiModel.Source = HttpContext.Request.Form["Source"];
                    newsApiModel.Category = HttpContext.Request.Form["Category"];
                    newsApiModel.Author = HttpContext.Request.Form["Author"];
                    newsApiModel.Title = HttpContext.Request.Form["Title"];
                    newsApiModel.Description = HttpContext.Request.Form["Description"];
                    newsApiModel.Url = HttpContext.Request.Form["Url"];
                    newsApiModel.UrlToImage = HttpContext.Request.Form["UrlToImage"];
                    newsApiModel.PublishedAt = HttpContext.Request.Form["PublishedAt"];
                    newsApiModel.Content = HttpContext.Request.Form["Content"];


                    _context.Add(newsApiModel);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Post added successfully.";

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' created news api with url '{newsApiModel.Url}'";
                        functions.LogActivity(AccountID, AccountID, "NewNewsApi", LogAction);
                    }

                    return RedirectToAction("ManageNewsApi", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Create News Api Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(newsApiModel);
        }



        // GET: Admin/EditNewsApi/id
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public async Task<IActionResult> EditNewsApi(int id)
        {
            // Set Meta Data 
            ViewData["Title"] = "Edit News Api";

            string AccountID = _sessionManager.LoginAccountId;

            var newsApiModel = await _context.NewsApi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newsApiModel == null)
            {
                return NotFound();
            }

            //Set ViewBags data for form return data
            ViewBag.CategoryList = functions.GetCategoryList();

            //set cancel post route for cancelation modal
            ViewBag.CancelRoute = "ManageNewsApi";

            return View(newsApiModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EditNewsApi(NewsApiModel newsApiModel)
        {
            string AccountID = _sessionManager.LoginAccountId;

            try
            {
                if (ModelState.IsValid)
                {
                    //reset post id
                    newsApiModel.ID = Int32.Parse(HttpContext.Request.Form["ID"]);

                    if (_context.NewsApi.Any(s => s.Url == newsApiModel.Url && s.ID != newsApiModel.ID))
                    {
                        TempData["ErrorMessage"] = "Another post with the same url already exist.";
                        return View(newsApiModel);
                    }

                    //get input values
                    newsApiModel.Source = HttpContext.Request.Form["Source"];
                    newsApiModel.Category = HttpContext.Request.Form["Category"];
                    newsApiModel.Author = HttpContext.Request.Form["Author"];
                    newsApiModel.Title = HttpContext.Request.Form["Title"];
                    newsApiModel.Description = HttpContext.Request.Form["Description"];
                    newsApiModel.Url = HttpContext.Request.Form["Url"];
                    newsApiModel.UrlToImage = HttpContext.Request.Form["UrlToImage"];
                    newsApiModel.PublishedAt = HttpContext.Request.Form["PublishedAt"];
                    newsApiModel.Content = HttpContext.Request.Form["Content"];

                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Source", newsApiModel.Source, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Category", newsApiModel.Category, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Author", newsApiModel.Author, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Title", newsApiModel.Title, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Description", newsApiModel.Description, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Url", newsApiModel.Url, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "UrlToImage", newsApiModel.UrlToImage, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "PublishedAt", newsApiModel.PublishedAt, _systemConfiguration.connectionString);
                    functions.UpdateTableData("NewsApi", "ID", newsApiModel.ID.ToString(), "Content", newsApiModel.Content, _systemConfiguration.connectionString);

                    //log activity
                    if (_systemConfiguration.logActivity)
                    {
                        string LogAction = $@"User '{functions.GetAccountData(AccountID, "FullName")}' edited news api data with url '{newsApiModel.Url}'";
                        functions.LogActivity(AccountID, AccountID, "EditNewsApi", LogAction);
                    }

                    TempData["SuccessMessage"] = "News api updated successfully.";
                    return RedirectToAction("ManageNewsApi", "Admin");
                }
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Edit News Api Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
            }
            return View(newsApiModel);
        }



        //  ███████╗██╗   ██╗██████╗ ███████╗ ██████╗██████╗ ██╗██████╗ ███████╗██████╗ ███████╗
        //  ██╔════╝██║   ██║██╔══██╗██╔════╝██╔════╝██╔══██╗██║██╔══██╗██╔════╝██╔══██╗██╔════╝
        //  ███████╗██║   ██║██████╔╝███████╗██║     ██████╔╝██║██████╔╝█████╗  ██████╔╝███████╗
        //  ╚════██║██║   ██║██╔══██╗╚════██║██║     ██╔══██╗██║██╔══██╗██╔══╝  ██╔══██╗╚════██║
        //  ███████║╚██████╔╝██████╔╝███████║╚██████╗██║  ██║██║██████╔╝███████╗██║  ██║███████║
        //  ╚══════╝ ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝
        //     

        // GET: ManageSubscribers
        public IActionResult ManageSubscribers()
        {
            // Set Meta Data 
            ViewData["Title"] = "Manage Subscribers";

            string AccountID = _sessionManager.LoginAccountId;
            var SubscribersData = _context.Subscribers.ToList();
            return View(SubscribersData);
        }


        // GET: SendEmail
        public IActionResult SendEmail([FromQuery(Name = "email")] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return RedirectToAction("ManageSubscribers", "Admin");
                }

                // Set Meta Data 
                ViewData["Title"] = "Send Email";

                ViewBag.Email = email;

                string AccountID = _sessionManager.LoginAccountId;
                var SubscribersData = _context.Subscribers.ToList();
                return View(SubscribersData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Send Email Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageSubscribers", "Admin");
            }
        }


        // POST: Admin/SendEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult SendEmail() 
        {
            string AccountID = _sessionManager.LoginAccountId;

            string SubscriberEmail = HttpContext.Request.Form["SubscriberEmail"];
            string EmailMessage = HttpContext.Request.Form["EmailMessage"];
            string Subject = HttpContext.Request.Form["Subject"];

            string[] ValidationInputs = { SubscriberEmail, EmailMessage };
            if (!functions.ValidateInputs(ValidationInputs))
            {
                TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                return RedirectToAction("ManageSubscribers", "Admin");
            }

            try
            {
                //send email 
                string ToName = _systemConfiguration.emailCompany;
                string[] MessageParagraphs = { "<strong style='text-align:center'>TOP STORIES FOR YOU</strong> <br/><br/>", EmailMessage };
                string PreHeader = "Recent News from " + functions.GetSiteLookupData("SiteName");
                bool Button = false;
                int ButtonPosition = 0;
                string ButtonLink = null;
                string ButtonLinkText = null;
                string Closure = _systemConfiguration.emailClosure;
                string Company = _systemConfiguration.emailCompany;
                string UnsubscribeLink = null;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = _systemConfiguration.smtpEmail;
                string ToEmail = SubscriberEmail;
                if (string.IsNullOrEmpty(Subject))
                {
                    Subject = "Subscription Email";
                }
                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);

                TempData["SuccessMessage"] = "Email sent.";
                return RedirectToAction("ManageSubscribers", "Admin");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Delete Job Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageJobs", "Admin");
            }
        }



        // GET: EmailSubscribers
        public IActionResult EmailSubscribers()
        {
            try
            {
                // Set Meta Data 
                ViewData["Title"] = "Send Email to Subscribers";

                string AccountID = _sessionManager.LoginAccountId;
                var SubscribersData = _context.Subscribers.ToList();
                return View(SubscribersData);
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Send Email to Subscribers Error: " + ex.ToString());
                TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                return RedirectToAction("ManageSubscribers", "Admin");
            }
        }


        // POST: Admin/SendEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        //check if user has access
        [AccessControlFilter(PermissionName = "Admin Permissions")]
        public IActionResult EmailAllSubscribers() 
        {
            string AccountID = _sessionManager.LoginAccountId;

            var DBQuery = _context.Subscribers.ToList();

            //loop through subscribers
            foreach (var item in DBQuery)
            {
                string EmailMessage = HttpContext.Request.Form["EmailMessage"];
                string Subject = HttpContext.Request.Form["Subject"];

                string[] ValidationInputs = { EmailMessage };
                if (!functions.ValidateInputs(ValidationInputs))
                {
                    TempData["ErrorMessage"] = "Validation error. Missing required field(s).";

                    return RedirectToAction("ManageSubscribers", "Admin");
                }

                try
                {
                    //send email 
                    string ToName = _systemConfiguration.emailCompany;
                    string[] MessageParagraphs = { "<strong style='text-align:center'>TOP STORIES FOR YOU</strong> <br/><br/>", EmailMessage };
                    string PreHeader = "Recent News from " + functions.GetSiteLookupData("SiteName");
                    bool Button = false;
                    int ButtonPosition = 0;
                    string ButtonLink = null;
                    string ButtonLinkText = null;
                    string Closure = _systemConfiguration.emailClosure;
                    string Company = _systemConfiguration.emailCompany;
                    string UnsubscribeLink = null;
                    string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                    string FromEmail = _systemConfiguration.smtpEmail;
                    string ToEmail = item.SubscriberEmail;
                    if (string.IsNullOrEmpty(Subject))
                    {
                        Subject = "Subscription Email";
                    }
                    EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, _systemConfiguration.smtpEmail, _systemConfiguration.smtpPass, _systemConfiguration.emailDisplayName, _systemConfiguration.smtpHost, _systemConfiguration.smtpPort);
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Delete Job Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email to the administrator.";
                    return RedirectToAction("ManageJobs", "Admin");
                }
            }

            TempData["SuccessMessage"] = "Email(s) sent.";
            return RedirectToAction("ManageSubscribers", "Admin");
        }


        // GET: ActivityLogs
        public IActionResult ActivityLogs()
        {
            // Set Meta Data 
            ViewData["Title"] = "Activity Logs";

            string AccountID = _sessionManager.LoginAccountId;
            var Data = _context.ActivityLogs.OrderByDescending(s => s.ActivityDate).ToList();
            return View(Data);
        }



        // GET: Empty
        public IActionResult Empty()
        {
            return View();
        }


        //ovveride NotFound() to E404 error page
        public new IActionResult NotFound()
        {
            return RedirectToAction("E404", "Error");
        }
    }
}
