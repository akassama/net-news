using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetNews.Models;
using NetNews.Models.AppModels;
using NetNews.Models.Email;
using Wangkanai.Detection.Services;

namespace NetNews.Controllers
{
    public class HomeController : Controller
    {
        AppFunctions functions = new AppFunctions();

        private readonly DBConnection _context;
        private readonly ILogger<HomeController> _logger;
        private readonly SystemConfiguration _systemConfiguration;
        private readonly IDetectionService _detectionService;
        private static IHttpContextAccessor _accessor;
        private readonly SessionManager _sessionManager;

        public HomeController(DBConnection context, ILogger<HomeController> logger, IOptions<SystemConfiguration> systemConfiguration, IDetectionService detectionService, IHttpContextAccessor accessor, SessionManager sessionManager)
        {
            _context = context;
            _logger = logger;
            _systemConfiguration = systemConfiguration.Value;
            _detectionService = detectionService;
            _accessor = accessor;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            ViewBag.ShowDonate = (_systemConfiguration.showDonateLink) ? "true" : "false";
            ViewBag.TotalVideoGallery = _context.vwPostsApproved.Count(s => s.PostType == "Gallery" || s.PostType == "Video");
            ViewBag.MorePosts = _context.vwPostsApproved.Skip(12).Count();
            ViewBag.WebsiteName = functions.GetSiteLookupData("SiteName");
            ViewBag.WeatherWidgetLink = _systemConfiguration.weatherLocationUrl;
            ViewBag.WeatherWidgetLocation = _systemConfiguration.weatherLocationText;
            ViewBag.ForexWidgetLink = _systemConfiguration.forexWidgetUrl;
            ViewBag.CovidWidgetClassId = _systemConfiguration.covidWidgetClassId;

            ViewData["ContentDescription"] = functions.GetSiteLookupData("MetaDescription");
            ViewData["ContentKeywords"] = functions.GetSiteLookupData("MetaKeywords");
            ViewBag.Title = functions.GetSiteLookupData("MetaTitle");

            ViewBag.IsHome = "True";

            //log visit
            string VisitorIP = functions.FormatVisitorIP(_sessionManager.SessionIP,_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            string OtherInfo = null; //add any other info here
            functions.VisitLog(_systemConfiguration.visitLogTypes.Split(",")[0], null, VisitorIP, _detectionService.Browser.Name.ToString(), _detectionService.Device.Type.ToString(), null, OtherInfo);

            return View();
        }


        //Set Session IP Address
        public ActionResult SetSessionIP(string key)
        {
            try
            {
                //log visit
                _sessionManager.SessionIP = key;
                return Json("success");
            }
            catch (Exception ex)
            {
                //Log Error
                _logger.LogInformation("Set Session Log Error: " + ex.ToString());

                _sessionManager.SessionIP = functions.FormatVisitorIP(_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());

                return Json("");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubscribeUser()
        {
            string SubscriberEmail = HttpContext.Request.Form["SubscriberEmail"];
            if (!string.IsNullOrEmpty(SubscriberEmail))
            {
                try
                {
                    if(_context.Subscribers.Any(s=> s.SubscriberEmail == SubscriberEmail))
                    {
                        string Link =  functions.GetSiteLookupData("AppDomain")+"/Subscriber/";
                        TempData["SuccessMessage"] = $@"Thank you for confirming your subscription. <br/> You can manage your subscriptions preferences <a href="+ Link +" target='_blank'>here</a>";
                        return RedirectToAction("Index", "Home");
                    }

                    //add subscriber
                    functions.AddSubscriber(SubscriberEmail);

                    //set email data
                    string SubscriberID = _context.Subscribers.Where(s => s.SubscriberEmail == SubscriberEmail).FirstOrDefault().SubscriberID;
                    string UnsubscribeLinkData = _systemConfiguration.emailUnsubscribeLink.Replace("#Subscriber#", SubscriberID);
                    UnsubscribeLinkData = _systemConfiguration.emailUnsubscribeLink.Replace("#Email#", SubscriberEmail);

                    //email subscriber
                    string ToName = SubscriberEmail.Split("@")[0];
                    string[] MessageParagraphs = { "Hi there",
                        "First off, I’d like to extend a warm welcome and ‘thank you’ for subscribing to the "+functions.GetSiteLookupData("SiteName")+" newsletter.",
                        "The "+functions.GetSiteLookupData("SiteName")+" news blog endeavors to send you only the best content.",
                        "To get you started on the right path, included in this email is a link to setup your subscription notifications to better suit your interest.",
                        "In the mean time, we would be sending you only the most news information.",
                        "If you have any questions or comments about the content you’re receiving please email us at "+functions.GetSiteLookupData("SupportEmail")+" and we will respond to your inquiry promptly.",
                       // "You can unsubscribe to the newsletter by clicking this <a href="+UnsubscribeLink+">link</a>"
                    };

                    string PreHeader = "New subscription notification.";
                    bool Button = true;
                    int ButtonPosition = 6;
                    string ButtonLink = functions.GetSiteLookupData("AppDomain")+"/Subscriber/?ID="+ SubscriberID;
                    string ButtonLinkText = "Setting up email automation";
                    string Closure = _systemConfiguration.emailClosure;
                    string Company = _systemConfiguration.emailCompany;
                    string UnsubscribeLink = UnsubscribeLinkData;
                    string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                    string FromEmail = _systemConfiguration.smtpEmail;
                    string ToEmail = SubscriberEmail;
                    string Subject = "New Subscriber Email";

                    //Get smtp details
                    string smtpEmail = _systemConfiguration.smtpEmail;
                    string smtpPass = _systemConfiguration.smtpPass;
                    string displayName = _systemConfiguration.emailDisplayName;
                    string smtpHost = _systemConfiguration.smtpHost;
                    int smtpPort = _systemConfiguration.smtpPort;

                    EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtpEmail, smtpPass, displayName, smtpHost, smtpPort);
                    TempData["SuccessMessage"] = @"Thank You For Subscribing! An email notification has been sent to "+ SubscriberEmail + " with futher details on setting up your preferences.";
                }
                catch (Exception ex)
                {
                    //Log Error
                    _logger.LogInformation("Subscription Error: " + ex.ToString());
                    TempData["ErrorMessage"] = "There was an error processing your request. Please try again. If this error persists, please send an email.";
                }
                
            }
            return RedirectToAction("Index", "Home");
        }


        //generate dynamic sitemap
        [Route("/sitemap.xml")]
        [Route("/sitemap_index.xml")]
        public void SitemapXml()
        {

            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            string host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                //write constant sitemaps
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host);
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "1.00");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host+ "/About");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Contact");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Sitemap");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.60");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Celebrities");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Music");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Videos");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/SignIn");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.60");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/SignUp");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.60");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/AllCategories");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.80");
                xml.WriteEndElement();

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/Radio");
                xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                xml.WriteElementString("priority", "0.70");
                xml.WriteEndElement();

                //Get all categories
                var Categories = _context.Categories.OrderBy(s => s.CategoryName).ToList();
                foreach (var item in Categories)
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/Category/"+item.CategoryName);
                    xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                    xml.WriteElementString("priority", "0.80");
                    xml.WriteEndElement();
                }

                //Get all tags
                var Tags = _context.Tags.OrderBy(s => s.TagName).ToList();
                foreach (var item in Tags)
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/Tags/" + item.ShortTagName.Replace(" ", string.Empty));
                    xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                    xml.WriteElementString("priority", "0.80");
                    xml.WriteEndElement();
                }

                //Get recent 50 posts
                var Posts = _context.Posts.OrderByDescending(s => s.DateAdded).Take(50).ToList();
                foreach (var item in Posts)
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/Posts/" + item.PostPermalink);
                    xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                    xml.WriteElementString("priority", "0.90");
                    xml.WriteEndElement();
                }

                //Get all authors
                var Authors = _context.Accounts.OrderBy(s => s.FirstName).ToList();
                foreach (var item in Authors)
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/Authors/" + item.DirectoryName);
                    xml.WriteElementString("lastmod", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")+ "+01:00");
                    xml.WriteElementString("priority", "0.60");
                    xml.WriteEndElement();
                }
            }
        }


        //generate robots.txt
        [Route("/robots.txt")]
        public ContentResult RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Disallow: /SignIn/")
                .AppendLine("Disallow: /SignUp/")
                .AppendLine("Disallow: /Jobs/")
                .AppendLine("Disallow: /Adverts/")
                .AppendLine("Disallow: /LatestNews/")
                .AppendLine("Disallow: /Test/")
                .AppendLine("")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap.xml")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap_index.xml");

            return this.Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
