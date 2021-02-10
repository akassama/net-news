using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetNews.Models;
using NetNews.Models.PostsDataModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace AppHelpers.App_Code
{
    public static class DataHelper
    {
        //Converts string to integer
        /// <summary>
        /// Converts string to integer, returns zero if fails
        /// </summary>
        /// <returns>integer</returns>
        public static int Int32Parse(string string_number)
        {
            try
            {
                return Int32.Parse(string_number);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        //Converts string to integer
        /// <summary>
        /// Converts string to integer, returns default passed if fails
        /// </summary>
        /// <returns>integer</returns>
        public static int Int32Parse(string string_number, int return_default)
        {
            try
            {
                return Int32.Parse(string_number);
            }
            catch (FormatException)
            {
                return return_default;
            }
        }

        //Get total post views by month
        public static int GetTotalViewsPerMonth(string post_author, int month, int year, string connection_string)
        {
            int total_count = 0;
            var DBQuery = @$"SELECT    COUNT(*) 
                            FROM      [PostViews] 
                            WHERE     YEAR(VisitDate) = '{year}' AND MONTH(VisitDate) = '{month}' AND [PostAuthor]  = '{post_author}'
                            GROUP BY  MONTH(VisitDate)";

            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        total_count = Int32Parse(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
            }

            return total_count;
        }


        //Get total post views by month for all
        public static int GetTotalViewsPerMonth(int month, int year, string connection_string)
        {
            int total_count = 0;
            var DBQuery = @$"SELECT    COUNT(*) 
                            FROM      [PostViews] 
                            WHERE     YEAR(VisitDate) = '{year}' AND MONTH(VisitDate) = '{month}'
                            GROUP BY  MONTH(VisitDate)";

            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        total_count = Int32Parse(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
            }

            return total_count;
        }


        //Get total post views by day
        public static int GetTotalViewsPerDay(string post_author, int day, int month, int year, string connection_string)
        {
            int day_number = DateTime.Now.AddDays(day).Day;

            int total_count = 0;
            var DBQuery = @$"SELECT    COUNT(*) 
                            FROM      [PostViews] 
                            WHERE     YEAR(VisitDate) = '{year}' AND MONTH(VisitDate) = '{month}' AND DAY(VisitDate) = '{day_number}' AND [PostAuthor]  = '{post_author}'
                            GROUP BY  MONTH(VisitDate)";

            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        total_count = Int32Parse(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
            }

            return total_count;
        }


        //Get total post views by day for all
        public static int GetTotalViewsPerDay(int day, int month, int year, string connection_string)
        {
            int day_number = DateTime.Now.AddDays(day).Day;

            int total_count = 0;
            var DBQuery = @$"SELECT    COUNT(*) 
                            FROM      [PostViews] 
                            WHERE     YEAR(VisitDate) = '{year}' AND MONTH(VisitDate) = '{month}' AND DAY(VisitDate) = '{day_number}'
                            GROUP BY  MONTH(VisitDate)";

            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        total_count = Int32Parse(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
            }

            return total_count;
        }


        //Get total views for author
        public static int GetTotalAuthorViews(string post_author, string connection_string)
        {
            int total_count = 0;
            var DBQuery = @$"SELECT    COUNT(*) 
                            FROM      [PostViews] 
                            WHERE     [PostAuthor]  = '{post_author}'";

            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        total_count = Int32Parse(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
            }

            return total_count;
        }

        //get post image folder name
        public static string GetAdvertImageDirectory(string advert_id)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.Adverts.Where(s => s.AdvertID == advert_id);
                if (DBQuery.Any())
                {
                    var DateString = DBQuery.FirstOrDefault().DateAdded.ToString();
                    DateTime FormattedDate = DateTime.Parse(DateString);
                    return FormattedDate.ToString("MM-yyyy");
                }
            }
            return null;
        }


        //get gallery image links
        public static HtmlString GetAdverts(int total = 20)
        {
            string result = "";
            using (var db = new DBConnection())
            {
                DateTime TodaysDate = DateTime.Now;
                if (db.Adverts.Any(s => s.ExpiryDate >= TodaysDate))
                {
                    var DBQuery = db.Adverts.Where(s => s.ExpiryDate >= TodaysDate).OrderByDescending(x => Guid.NewGuid()).Take(total);//get random advert 
                    foreach (var item in DBQuery)
                    {
                        result += @$"<div class='col-md-3 col-sm-6 mb-4'>
								<a href='/Adverts/{item.AdvertPermalink}'>
									<img class='img-fluid' src='/files/images/{DataHelper.GetAdvertImageDirectory(item.AdvertID)}/{item.AdvertImage}' width='500' height='300' alt=''>
								</a>
							</div>";
                    }
                }
            }
            return new HtmlString(result);
        }

        //Get img thumbnails from Vimeo
        public static string GetVimeoPreviewImage(string vimeoURL)
        {
            //TODO
            return null;
        }



        //formats header style based on total number
        public static HtmlString GetPostPreview(string post_id)
        {
            using (var db = new DBConnection())
            {
                string result = "<span></span>";

                string PostImage = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage;
                string PostTitle = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTitle;
                string PostType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostType;

                switch (PostType)
                {
                    case "StandardNewsPost":
                    case "NewsGalleryPost":
                    case "EntertainmentNewsPost":
                        // get headline image
                        result = @$"<a href='/Admin/PostDetails/{post_id}'>
                                            <img src='/files/{PostHelper.GetPostImageLink(post_id)}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        break;
                    case "NewsAudioPost":
                        // get audio cover image
                        result = @$"<a href='/Admin/PostDetails/{post_id}'>
                                            <img src='/files/images/defaults/audio-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        break;
                    case "EntertainmentAudioPost":
                        // get audio cover image for entertainment audio
                        string AudioCover = (!string.IsNullOrEmpty(db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage)) ? $@"/files/{PostHelper.GetPostImageLink(post_id)}" : "/files/images/defaults/audio-file.jpg";
                        result = @$"<a href='/Admin/PostDetails/{post_id}'>
                                            <img src='{AudioCover}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        break;
                    case "NewsVideoPost":
                    case "EntertainmentVideoPost":
                        // get video image
                        string VideoType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoType;
                        string VideoLink = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoLink;
                        string VideoID = PostHelper.GetYouTubeVideoID(VideoLink);

                        if (VideoType == "YouTube")
                        {
                            result = @$"<a href='{VideoLink}'>
                                            <img src='https://i1.ytimg.com/vi/{VideoID}/sddefault.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";

                        }
                        else if (VideoType == "Vimeo")
                        {
                            result = @$"<a href='{VideoLink}'>
                                            <img src='/files/images/defaults/video-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        }
                        else
                        {
                            result = @$"<a href='{VideoLink}'>
                                            <img src='/files/images/defaults/video-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        }
                        break;
                    default:
                        // default
                        result = @$"<a href='#'>
                                            <img src='https://via.placeholder.com/300x200/09f.png/fff?text=NEWS' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' width='150' height='75' class='img-fluid' />
                                        </a>";
                        break;
                }
                return new HtmlString(result);
            }
        }

    }

    public static class TextHelper
    {
        //removes html tags from text
        public static string StripHTML(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "<.*?>", "");
            }
            return text;
        }


        //trims text to the nearest desired lenght passed in the parameter
        public static string FormatLongText(string text, int max_length)
        {
            if (text != null && text.Length > max_length)
            {
                int iNextSpace = text.LastIndexOf(" ", max_length, StringComparison.Ordinal);
                text = $"{(text.Substring(0, (iNextSpace > 0) ? iNextSpace : max_length).Trim())}...";
            }
            return text;
        }

        //convert text case
        public static string ConvertCase(string text, string convert_to)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(convert_to))
            {
                return text;
            }
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            switch (convert_to)
            {
                case "Upper":
                    // convert to upper case
                    return textInfo.ToUpper(text);
                case "Lower":
                    // convert to lower case
                    return textInfo.ToLower(text);
                case "Title":
                    // convert to title case
                    return textInfo.ToTitleCase(text);
                case "TitleTrim":
                    // convert to title case and remove space
                    return Regex.Replace(textInfo.ToTitleCase(text), @"\s+", "");
                case "SplitUpper":
                    //split text by capital case
                    return Regex.Replace(text, "([A-Z])", " $1").Trim();
                default:
                    return text;
            }
        }

        //formats alert style based on total number
        public static HtmlString FormatAlert(int total_number)
        {
            string result = "<span></span>";
            if (total_number == 0)
            {
                result = $"<span class='badge badge-pill badge-primary'>{total_number}</span>";
            }
            else if (total_number > 0)
            {
                result = $"<span class='badge badge-pill badge-danger'>{total_number}</span>";
            }
            return new HtmlString(result);
        }

        //formats pusblished style based on total number
        public static HtmlString FormatPublishedState(int published_value)
        {
            string result = "<span></span>";
            if (published_value == 0)
            {
                result = $"<span class='badge badge-pill badge-danger'>Unpublished</span>";
            }
            else if (published_value == 1)
            {
                result = $"<span class='badge badge-pill badge-success'>Published</span>";
            }
            return new HtmlString(result);
        }

        //formats header style based on total number
        public static HtmlString FormatHeaderState(int header_value)
        {
            string result = "<span></span>";
            if (header_value == 0)
            {
                result = $"<span class='badge badge-pill badge-danger'>No</span>";
            }
            else if (header_value == 1)
            {
                result = $"<span class='badge badge-pill badge-success'>Yes</span>";
            }
            return new HtmlString(result);
        }

        /// <summary>
        /// Returns the absolute url.
        /// </summary>
        public static string Url(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }

    }

    public static class PostHelper
    {
        //get category name using category id
        public static string GetCategoryName(string category_id)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.Categories.Where(s => s.CategoryID == category_id);
                if (DBQuery.Any())
                {
                    return DBQuery.FirstOrDefault().CategoryName;
                }
            }
            return null;
        }

        //get category id using category name
        public static string GetCategoryID(string category_name)
        {
            category_name = TextHelper.ConvertCase(category_name, "TitleTrim");
            using (var db = new DBConnection())
            {
                var DBQuery = db.Categories.Where(s => s.ShortCategoryName == category_name);
                if (DBQuery.Any())
                {
                    return DBQuery.FirstOrDefault().CategoryID;
                }
            }
            return null;
        }

        //get approvat status  using approval id
        public static HtmlString GetPostApprovalState(int approval_id)
        {

            if (approval_id == 0)
            {
                return new HtmlString($"<div class='badge badge-warning badge-warning-alt'>Pending</div>");
            }
            else if (approval_id == 1)
            {
                return new HtmlString($"<div class='badge badge-success badge-success-alt'>Approved</div>");
            }
            else if (approval_id == 2)
            {
                return new HtmlString($"<div class='badge badge-danger badge-danger-alt'>Rejected</div>");
            }
            else
            {
                return new HtmlString($"<div class='badge badge-primary badge-primary-alt'>Unknown</div>");
            }
        }

        //get approvat status  using post id
        public static HtmlString GetPostApprovalState(string post_id)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPosts.Where(s => s.PostID == post_id).ToList();
                if (DBQuery.Any())
                {
                    if (DBQuery.FirstOrDefault().ApprovalState == 0) {
                        return new HtmlString($"<div class='badge badge-danger badge-danger-alt'>Pending</div>");
                    }
                    else if (DBQuery.FirstOrDefault().ApprovalState == 1)
                    {
                        return new HtmlString($"<div class='badge badge-success badge-success-alt'>Approved</div>");
                    }
                    else
                    {
                        return new HtmlString($"<div class='badge badge-primary badge-primary-alt'>Unknown</div>");
                    }
                }
            }
            return null;
        }

        //get post image link using post id
        public static string GetPostImageLink(string post_id)
        {
            //get post image folder name
            var DirectoryName = GetPostImageDirectory(post_id);

            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPosts.Where(s => s.PostID == post_id);
                if (DBQuery.Any())
                {
                    var PostImage = DBQuery.FirstOrDefault().PostImage;
                    return "images/" + DirectoryName + "/" + PostImage;
                }
            }
            //return default image place holder
            return "images/defaults/news-placeholder-image.jpg";
        }


        //get total post views
        public static int GetPostViews(string post_id)
        {
            using (var db = new DBConnection())
            {
                return db.PostViews.Count(s => s.PostID == post_id);
            }
        }

        //get total post views by country
        public static int GetPostCountryViews(string post_id, string country_name)
        {
            using (var db = new DBConnection())
            {
                return db.PostViews.Count(s => s.PostID == post_id && s.Country == country_name);
            }
        }


        //get post gallery image link using post id
        public static string GetPostGalleryImageLink(int gallery_id, string post_id)
        {
            //get post image folder name
            var DirectoryName = GetPostImageDirectory(post_id);

            using (var db = new DBConnection())
            {
                var DBQuery = db.GalleryImages.Where(s => s.ID == gallery_id);
                if (DBQuery.Any())
                {
                    var PostImage = DBQuery.FirstOrDefault().ImageLink;
                    return "images/" + DirectoryName + "/" + PostImage;
                }
            }
            //return default image place holder
            return "images/defaults/news-placeholder-image.jpg";
        }


        //get post image folder name
        public static string GetPostImageDirectory(string post_id)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPosts.Where(s => s.PostID == post_id);
                if (DBQuery.Any())
                {
                    var DateString = DBQuery.FirstOrDefault().DateAdded.ToString();
                    DateTime FormattedDate = DateTime.Parse(DateString);
                    return FormattedDate.ToString("MM-yyyy");
                }
            }
            return null;
        }

        //format post tags
        public static HtmlString FormatPostTags(string post_tags)
        {
            string result = "";
            string[] elements = post_tags.Split(',');
            // generate tags list
            foreach (string item in elements)
            {
                result += $"<span class='badge badge-outline-dark mr-2 mb-1'><a class='text-decoration-none text-dark' href='/Tags/{item}'>{TextHelper.ConvertCase(item, "SplitUpper")}</a></span>";
            }
            return new HtmlString(result);
        }

        //format post date, takes date as string
        public static string FormatPostDate(string post_date)
        {
            DateTime FormattedDate = DateTime.Parse(post_date);
            return FormattedDate.ToString("MMMM dd, yyyy");
        }


        //format post date, takes date as date object
        public static string FormatPostDate(DateTime post_date)
        {
            return post_date.ToString("MMMM dd, yyyy");
        }


        //get post comments
        public static HtmlString GetPostComments(string post_id)
        {
            string result = "";

            using (var db = new DBConnection())
            {
                var DBQuery = db.PostReviews.Where(s => s.PostID == post_id).ToList();
                // generate comments data
                foreach (var item in DBQuery)
                {
                    result += $"<p class='font-weight-bold'><img src='{AccountHelper.GetAccountProfilePicture(item.ReviewerID)}' class='rounded-circle m-1' alt='profile-pic' width='30' height='30'>{AccountHelper.GetAccountData(item.ReviewerID, "FullName")}</p><p class='text-danger'>{item.ReviewComment}</p><b/>";
                }
            }
            return new HtmlString(result);
        }


        //get post comments
        public static HtmlString GetCategoryParent(string category_id)
        {
            string result = "<span class='text-info'>None</span>";
            if (!string.IsNullOrEmpty(category_id))
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.CategoryID == category_id);
                    // get category details if existing
                    if (DBQuery.Any())
                    {
                        result = $"<a class='text-info text-decoration-none' href='/Admin/ViewCategoryDetails/{DBQuery.FirstOrDefault().CategoryID}'>{DBQuery.FirstOrDefault().CategoryName}</a>";
                        return new HtmlString(result);
                    }
                }
            }
            return new HtmlString(result);
        }

        //get specific post data. Takes post id and data to return
        public static string GetPostData(string post_id, string return_data)
        {
            try
            {
                if (!string.IsNullOrEmpty(post_id))
                {
                    using (var db = new DBConnection())
                    {
                        switch (return_data)
                        {
                            case "PostType":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostType != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostType : "";
                            case "PostPermalink":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostPermalink != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostPermalink : "";
                            case "PostAuthor":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostAuthor != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAuthor : "";
                            case "PostCategory":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostCategory != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostCategory : "";
                            case "PostTitle":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostTitle != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTitle : "";
                            case "PostExtract":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostExtract != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostExtract : "";
                            case "PostImage":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostImage != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage : "";
                            case "ImageCaption":
                                return (db.Posts.Any(s => s.PostID == post_id && s.ImageCaption != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().ImageCaption : "";
                            case "IsBreakingNews":
                                return (db.Posts.Any(s => s.PostID == post_id && s.IsBreakingNews != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().IsBreakingNews.ToString() : "0";
                            case "PostContent":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostContent != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostContent : "";
                            case "PostVideoType":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostVideoType != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoType : "";
                            case "PostVideoLink":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostVideoLink != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoLink : "";
                            case "PostAudioType":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostAudioType != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAudioType : "";
                            case "PostAudioLink":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostAudioLink != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAudioLink : "";
                            case "PostTags":
                                return (db.Posts.Any(s => s.PostID == post_id && s.PostTags != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTags : "";
                            case "UpdatedBy":
                                return (db.Posts.Any(s => s.PostID == post_id && s.UpdatedBy != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().UpdatedBy : "";
                            case "DateAdded":
                                return (db.Posts.Any(s => s.PostID == post_id && s.DateAdded != null)) ? db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().DateAdded.ToString() : "";
                            case "ApprovalsDateAdded":
                                return (db.vwPostsApproved.Any(s => s.PostID == post_id && s.ApprovalsDateAdded != null)) ? db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().ApprovalsDateAdded.ToString() : "";
                            default:
                                return "NA";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return "NA";
        }


        //get total number of posts for category
        public static int GetTotalCategoryPosts(string category_id)
        {
            using (var db = new DBConnection())
            {
                return db.vwPostsApproved.Count(s => s.PostCategory == category_id);
            }
        }


        //check if post has gallery
        public static bool PostHasGallery(string PostID)
        {
            using (var db = new DBConnection())
            {
                if (db.GalleryImages.Any(s => s.PostID == PostID))
                {
                    return true;
                }
            }
            return false;
        }


        //get post view country flag links
        public static HtmlString GetCountryFlagLinks(string country_name, int width)
        {
            string result = "";
            if (!string.IsNullOrEmpty(country_name))
            {
                using (var db = new DBConnection())
                {
                    string CountryCode = null;
                    var DBQuery = db.Countries.Where(s => s.NiceName == country_name);
                    if (DBQuery.Any())
                    {
                        CountryCode = DBQuery.FirstOrDefault().ISO;
                        result += @$"<img class='mr-2' src='https://www.countryflags.io/{CountryCode}/flat/{width}.png'>";
                    }
                    else if (db.Countries.Where(s => s.NiceName.Contains(country_name)).Any())
                    {
                        CountryCode = db.Countries.Where(s => s.NiceName.Contains(country_name)).FirstOrDefault().ISO;
                    }
                }
            }
            return new HtmlString(result);
        }


        //Get country info
        /// <summary>
        /// Get current visitor country info based on ip address
        /// </summary>
        /// <returns>The the info for the second parameter passed</returns>
        public static string GetIpInfo(string ip_address, string return_data, string access_key)
        {
            string url = "http://api.ipstack.com/" + ip_address + "?access_key=" + access_key;
            var request = WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            using (Stream stream = wrs.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var obj = JObject.Parse(json);
                string continent = (string)obj["continent_name"];
                string city = (string)obj["city"];
                string country = (string)obj["country_name"];
                string country_code = (string)obj["country_code"];
                var country_flag_url = (string)obj["location"]["country_flag"];
                string calling_code = (string)obj["location"]["calling_code"];

                switch (return_data)
                {
                    case "Continent":
                        return continent;
                    case "Country":
                        return country;
                    case "City":
                        return city;
                    case "CountryCode":
                        return country_code;
                    case "Flag":
                        return country_flag_url;
                    case "CallCode":
                        return calling_code;
                    default:
                        return null;
                }
            }
        }


        //checks if posts has a view from a country
        public static bool PostHasCountryView(string post_id, string country_name)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.PostViews.Where(s => s.PostID == post_id && s.Country == country_name);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get post view by country for author
        public static int GetPostViewByCountry(string author_id, string country_name)
        {
            using (var db = new DBConnection())
            {
                return db.PostViews.Count(s => s.PostAuthor == author_id && s.Country == country_name);
            }
        }

        //get country code from country name
        public static string GetCountryCode(string country_name)
        {
            using (var db = new DBConnection())
            {
                var DbQuery = db.Countries.Where(s => s.NiceName == country_name);
                if (DbQuery.Any())
                {
                    return DbQuery.FirstOrDefault().ISO;
                }
                return null;
            }
        }

        //get post edit view
        public static string GetEditRoute(string post_id)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.Posts.Where(s => s.PostID == post_id);
                if (DBQuery.Any())
                {
                    string PostType = DBQuery.FirstOrDefault().PostType;
                    switch (PostType)
                    {
                        case "StandardNewsPost":
                            return "EditStandardNewsPost";
                        case "NewsVideoPost":
                            return "EditNewsVideoPost";
                        case "NewsGalleryPost":
                            return "EditNewsGalleryPost";
                        case "NewsAudioPost":
                            return "EditNewsAudioPost";
                        case "EntertainmentNewsPost":
                            return "EditEntertainmentNewsPost";
                        case "EntertainmentVideoPost":
                            return "EditEntertainmentVideoPost";
                        case "EntertainmentAudioPost":
                            return "EditEntertainmentAudioPost";
                        default:
                            return "EditStandardNewsPost";
                    }
                }
            }
            return null;
        }



        /// <summary>
        /// Get youtube video id
        /// </summary>
        public static string GetYouTubeVideoID(string url)
        {
            if (url.Contains("http") || url.Contains("www") || url.Contains("you"))
            {
                var uri = new Uri(url);

                // you can check host here => uri.Host <= "www.youtube.com"

                var query = HttpUtility.ParseQueryString(uri.Query);

                var videoId = string.Empty;

                if (query.AllKeys.Contains("v"))
                {
                    videoId = query["v"];
                }
                else
                {
                    videoId = uri.Segments.Last();
                }

                return videoId;
            }
            return url;
        }



        /// <summary>
        /// Get vimeo video id
        /// </summary>
        public static string GetVimeoVideoID(string url)
        {
            if (url.Contains("http") || url.Contains("www"))
            {
                var uri = new Uri(url);
                Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match vimeoMatch = VimeoVideoRegex.Match(url);
                string id = string.Empty;
                if (vimeoMatch.Success)
                {
                    id = vimeoMatch.Groups[1].Value;
                    return id;
                }
            }
            return url;
        }

        //Get id from video links //https://gist.github.com/greenygh0st/0ebca53a90548ee15338
        public static string ParseLinks(string str)
        {
            Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
            Regex HyperlinkRegex = new Regex("http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);


            //here we pass through all of the regex
            MatchCollection HyperLinkmatches = HyperlinkRegex.Matches(str);
            foreach (Match match in HyperLinkmatches)
            {

                Match youtubeMatch = YoutubeVideoRegex.Match(match.Value);
                Match vimeoMatch = VimeoVideoRegex.Match(match.Value);

                if (youtubeMatch.Success)
                {
                    var id = youtubeMatch.Groups[1].Value;
                    str = str.Replace(match.Value, "<iframe id=\"ytplayer\" type=\"text/html\" width=\"640\" height=\"390\" src=\"http://www.youtube.com/embed/" + id + "\" frameborder=\"0\"/>");
                } else if (vimeoMatch.Success)
                {
                    var id = vimeoMatch.Groups[1].Value;
                    str = str.Replace(match.Value, "<iframe src=\"//player.vimeo.com/video/" + id + "\" width=\"WIDTH\" height=\"HEIGHT\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>");
                } else
                {
                    str = str.Replace(match.Value, "<a target='_blank' href='" + match.Value + "'>" + match.Value + "</a>");
                }

            }
            return str;
        }


        //get gallery images
        public static HtmlString GetPostGalleryImages(string post_id)
        {
            string result = "";
            using (var db = new DBConnection())
            {
                if (db.GalleryImages.Any(s => s.PostID == post_id))
                {
                    var DBQuery = db.GalleryImages.Where(s => s.PostID == post_id);
                    int count = 0;
                    foreach (var item in DBQuery)
                    {
                        result += @$"<div class='col-12 mt-3'>
					                    <img src='/files/{PostHelper.GetPostGalleryImageLink(item.ID, item.PostID)}' alt='{item.ImageCaption}' class='img-fluid w-100'>   
				                    </div>
                                    <div class='col-sm-12 col-md-10 offset-md-1 bg-light mb-3'>
						                <h5 class='text-dark font-weight-bold'>{item.ImageCaption}</h5>
					                </div>
                                    <hr/>
                                    ";
                        count++;
                    }
                }
            }
            return new HtmlString(result);
        }



        //formats header style based on total number
        public static HtmlString GetPostImagePreview(string post_id)
        {
            using (var db = new DBConnection())
            {
                string result = "<span></span>";

                string PostImage = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage;
                string PostTitle = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTitle;
                string PostType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostType;
                string PostPermalink = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostPermalink;

                switch (PostType)
                {
                    case "StandardNewsPost":
                    case "NewsGalleryPost":
                    case "EntertainmentNewsPost":
                        // get headline image
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/{PostHelper.GetPostImageLink(post_id)}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100' />
                                        </a>";
                        break;
                    case "NewsAudioPost":
                        // get audio cover image
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/images/defaults/audio-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}'  class='img-fluid w-100 h-100' />
                                        </a>";
                        break;
                    case "EntertainmentAudioPost":
                        // get audio cover image for entertainment audio
                        string AudioCover = (!string.IsNullOrEmpty(db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage)) ? $@"/files/{PostHelper.GetPostImageLink(post_id)}" : "/files/images/defaults/audio-file.jpg";
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='{AudioCover}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100' />
                                        </a>";
                        break;
                    case "NewsVideoPost":
                    case "EntertainmentVideoPost":
                        // get video image
                        string VideoType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoType;
                        string VideoLink = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoLink;
                        string VideoID = PostHelper.GetYouTubeVideoID(VideoLink);

                        if (VideoType == "YouTube")
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='https://i1.ytimg.com/vi/{VideoID}/sddefault.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100' />
                                        </a>";

                        }
                        else if (VideoType == "Vimeo")
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/images/defaults/video-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100' />
                                        </a>";
                        }
                        else
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/images/defaults/video-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100' />
                                        </a>";
                        }
                        break;
                    default:
                        // default
                        result = @$"<a href='#'>
                                            <img src='https://via.placeholder.com/300x200/09f.png/fff?text=NEWS' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='w-100 h-100 img-fluid' />
                                        </a>";
                        break;
                }
                return new HtmlString(result);
            }
        }


        //formats header style based on total number
        public static HtmlString GetPostImagePreview(string post_id, string class_name)
        {
            using (var db = new DBConnection())
            {
                string result = "<span></span>";

                string PostImage = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage;
                string PostTitle = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTitle;
                string PostType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostType;
                string PostPermalink = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostPermalink;

                switch (PostType)
                {
                    case "StandardNewsPost":
                    case "NewsGalleryPost":
                    case "EntertainmentNewsPost":
                        // get headline image
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/{PostHelper.GetPostImageLink(post_id)}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";
                        break;
                    case "NewsAudioPost":
                        // get audio cover image
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='/files/images/defaults/audio-file.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}'  class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";
                        break;
                    case "EntertainmentAudioPost":
                        // get audio cover image for entertainment audio
                        string AudioCover = (!string.IsNullOrEmpty(db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage)) ? $@"/files/{PostHelper.GetPostImageLink(post_id)}" : "/files/images/defaults/video-file.jpg";
                        result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='{AudioCover}' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 {class_name}' />
                                        </a>";
                        break;
                    case "NewsVideoPost":
                    case "EntertainmentVideoPost":
                        // get video image
                        string VideoType = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoType;
                        string VideoLink = db.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoLink;
                        string VideoID = PostHelper.GetYouTubeVideoID(VideoLink);

                        if (VideoType == "YouTube")
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='https://i1.ytimg.com/vi/{VideoID}/sddefault.jpg' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";

                        }
                        else if (VideoType == "Vimeo")
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='https://via.placeholder.com/300x200/09f.png/fff?text=Vimeo' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";
                        }
                        else
                        {
                            result = @$"<a href='/Posts/{PostPermalink}'>
                                            <img src='https://via.placeholder.com/300x200/09f.png/fff?text=Video' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";
                        }
                        break;
                    default:
                        // default
                        result = @$"<a href='#'>
                                            <img src='https://via.placeholder.com/300x200/09f.png/fff?text=NEWS' alt='{TextHelper.FormatLongText(PostTitle, 15).ToLower()}' class='img-fluid w-100 h-100 {class_name}' />
                                        </a>";
                        break;
                }
                return new HtmlString(result);
            }
        }

        //Algorithm Get featured home news ID
        public static string GetFeaturedNewsID(string featured_categories)
        {
            string FeaturedID = null;
            using (var db = new DBConnection())
            {
                if (db.vwPostsApproved.Any())
                {
                    //1st check for recent featured within past n hours
                    DateTime YesterdaysDate = DateTime.Now.AddHours(-12);
                    var DBQuery = db.vwPostsApproved.Where(s => (s.FeaturedPost == 1 || s.IsBreakingNews == 1) && s.ApprovalsDateAdded >= YesterdaysDate);
                    if (DBQuery.Any())
                    {
                        FeaturedID = DBQuery.FirstOrDefault().PostID;
                        return FeaturedID;
                    }

                    //TODO Make dynamic from config
                    //2nd check for popular categories within past n hours
                    string FeaturedCategoryOneID = "";
                    string FeaturedCategoryTwoID = "";
                    string FeaturedCategoryThreeID = "";
                    if (!string.IsNullOrEmpty(featured_categories))
                    {
                        //more then one featured category
                        if (featured_categories.Contains(","))
                        {
                            int length = featured_categories.Split(",").Length;
                            switch (length)
                            {
                                case 1:
                                    FeaturedCategoryOneID = PostHelper.GetCategoryID(featured_categories.Split(",")[0].ToString());
                                    break;
                                case 2:
                                    FeaturedCategoryOneID = PostHelper.GetCategoryID(featured_categories.Split(",")[0].ToString());
                                    FeaturedCategoryTwoID = PostHelper.GetCategoryID(featured_categories.Split(",")[1].ToString());
                                    break;
                                case 3:
                                    FeaturedCategoryOneID = PostHelper.GetCategoryID(featured_categories.Split(",")[0].ToString());
                                    FeaturedCategoryTwoID = PostHelper.GetCategoryID(featured_categories.Split(",")[1].ToString());
                                    FeaturedCategoryThreeID = PostHelper.GetCategoryID(featured_categories.Split(",")[2].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //only one featured category
                            FeaturedCategoryOneID = PostHelper.GetCategoryID(featured_categories);
                        }
                    }

                    YesterdaysDate = DateTime.Now.AddHours(-18);
                    DBQuery = db.vwPostsApproved.Where(s => (s.PostCategory == FeaturedCategoryOneID || s.PostCategory == FeaturedCategoryTwoID || s.PostCategory == FeaturedCategoryThreeID) && s.ApprovalsDateAdded >= YesterdaysDate);
                    if (DBQuery.Any())
                    {
                        FeaturedID = DBQuery.FirstOrDefault().PostID;
                        return FeaturedID;
                    }

                    // finally check last posted
                    DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                    if (DBQuery.Any())
                    {
                        FeaturedID = DBQuery.FirstOrDefault().PostID;
                        return FeaturedID;
                    }
                }
            }

            return FeaturedID;
        }



        //get approvat status  using approval id
        public static HtmlString GetLookupInputFormat(string data_type, string data_options, string value)
        {
            string result = @$"<input type='text' class='form-control count-chars' data-chars-max='250' data-msg-color='danger' maxlength='250' name='Value' id='Value' spellcheck='true' value='{value}' required>";
            if (data_type == "text")
            {
                result = @$"<input type='text' class='form-control count-chars' data-chars-max='250' data-msg-color='danger' maxlength='250' name='Value' id='Value' spellcheck='true' value='{value}' required>";
                return new HtmlString(result);
            }
            else if (data_type == "textarea")
            {
                result = @$"<textarea class='form-control' rows='5' name='Value' id='PostContent' required>{value}</textarea>";
                return new HtmlString(result);
            }
            else if (data_type == "integer")
            {
                result = @$"<input type='text' class='form-control integer-plus-only' maxlength='10' name='Value' id='Value' spellcheck='true' value='{value}' required>";
                return new HtmlString(result);
            }
            else if (data_type == "select")
            {
                string[] arrData = data_options.Split(",");
                result = @$"<select class='form-control'  name='Value' id='Value' required>
	                            <option value='{value}' selected>{value}</option>
                           ";
                foreach (string item in arrData)
                {
                    result += $@"<option value='{item}'>{item}</option>";
                }

                result += @$"</select>";

                return new HtmlString(result);
            }
            else
            {
                return new HtmlString(result);
            }
        }

    }

    public static class AccountHelper
    {
        //get total account posts
        public static int GetAccountPostsCount(string account_id)
        {
            using (var db = new DBConnection())
            {
                return db.Posts.Count(s => s.PostAuthor == account_id);
            }
        }

        //get specific account data. Takes account id and data to return
        public static string GetAccountData(string account_id, string return_data)
        {
            try
            {
                if (!string.IsNullOrEmpty(account_id))
                {
                    using (var db = new DBConnection())
                    {
                        switch (return_data)
                        {
                            case "FirstName":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.FirstName != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().FirstName : "";
                            case "LastName":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.LastName != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().LastName : "";
                            case "FullName":
                                return db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().FirstName + " " + db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().LastName;
                            case "Email":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.Email != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Email : "";
                            case "ProfilePicture":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.ProfilePicture != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().ProfilePicture : "";
                            case "Active":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.Active != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Active.ToString() : "";
                            case "Oauth":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.Oauth != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Oauth.ToString() : "";
                            case "EmailVerification":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.EmailVerification != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().EmailVerification.ToString() : "";
                            case "DirectoryName":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.DirectoryName != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().DirectoryName : "";
                            case "Country":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.Country != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Country : "";
                            case "CountryCode":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.CountryCode != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().CountryCode.ToString() : "";
                            case "PhoneNumber":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.PhoneNumber != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumber.ToString() : "";
                            case "PhoneNumberVerification":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.PhoneNumberVerification != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumberVerification.ToString() : "";
                            case "Biography":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.Biography != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Biography : "";
                            case "DateOfBirth":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.DateOfBirth != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().DateOfBirth.ToString() : "";
                            case "Gender":
                                return (db.AccountDetails.Any(s => s.AccountID == account_id && s.Gender != null)) ? db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Gender : "";
                            case "UpdatedBy":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.UpdatedBy != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().UpdatedBy : "";
                            case "UpdateDate":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.UpdateDate != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().UpdateDate.ToString() : "";
                            case "DateAdded":
                                return (db.Accounts.Any(s => s.AccountID == account_id && s.DateAdded != null)) ? db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().DateAdded.ToString() : "";
                            default:
                                return "NA";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return "NA";
        }

        //get account profile picture
        public static string GetAccountProfilePicture(string account_id)
        {
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Accounts.Where(s => s.AccountID == account_id && s.ProfilePicture != null);
                    if (DBQuery.Any())
                    {
                        return "/files/images/accounts/" + DBQuery.FirstOrDefault().DirectoryName + "/" + DBQuery.FirstOrDefault().ProfilePicture;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return "/files/images/defaults/default-profile.jpg";
        }

        //get account social media
        public static HtmlString GetAccountSocialMedia(string account_id, string connection_string)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.SocialMediaDetails.Where(s => s.AccountID == account_id);
                    string Facebook = ModelHelper.GetTableData("SocialMediaDetails", "AccountID", account_id, "FacebookLink", connection_string);
                    string Twitter = ModelHelper.GetTableData("SocialMediaDetails", "AccountID", account_id, "TwitterLink", connection_string);
                    string Instagram = ModelHelper.GetTableData("SocialMediaDetails", "AccountID", account_id, "InstagramLink", connection_string);
                    string LinkedIn = ModelHelper.GetTableData("SocialMediaDetails", "AccountID", account_id, "LinkedInLink", connection_string);

                    //get facebook
                    if (!string.IsNullOrEmpty(Facebook))
                    {
                        Facebook = "<li><a href='" + Facebook + "' target='_blank'><i class='mdi mdi-facebook'></i></a></li>";
                    }

                    //get twitter
                    if (!string.IsNullOrEmpty(Twitter))
                    {
                        Twitter = "<li><a href='" + Twitter + "' target='_blank'><i class='mdi mdi-twitter'></i></a></li>";
                    }

                    //get twitter
                    if (!string.IsNullOrEmpty(Instagram))
                    {
                        Instagram = "<li><a href='" + Instagram + "' target='_blank'><i class='mdi mdi-instagram'></i></a></li>";
                    }

                    result = Facebook + Twitter + Instagram;
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }

            return new HtmlString(result);
        }

        //get total posts for author
        public static int GetTotalPostsCount(string author_id)
        {
            using (var db = new DBConnection())
            {
                return db.vwPosts.Count(s => s.PostAuthor == author_id);
            }
        }

        //get number of pending reviews for author
        public static int GetPostReviewsCount(string author_id)
        {
            using (var db = new DBConnection())
            {
                return db.vwPosts.Count(s => s.PostAuthor == author_id && s.ApprovalState == 0);
            }
        }

        //get number of pending reviews for reviewer
        public static int GetPendingtReviewsCount()
        {
            using (var db = new DBConnection())
            {
                return db.vwPosts.Count(s => s.ApprovalState == 0);
            }
        }

        //get the number of posts that need review
        public static int GetPostReviewsCount()
        {
            using (var db = new DBConnection())
            {
                return db.vwPostReviews.Count(s => s.ApprovalState == 0);
            }
        }


        //get number of pending acoount needing approval
        public static int GetPendingAccountsCount()
        {
            using (var db = new DBConnection())
            {
                return db.Accounts.Count(s => s.Active == 0);
            }
        }

        //get approval status badge using active status
        public static HtmlString GetAccountApprovalState(int? active_status)
        {

            if (active_status == 0)
            {
                return new HtmlString($"<div class='badge badge-warning badge-warning-alt'>Pending</div>");
            }
            else if (active_status == 1)
            {
                return new HtmlString($"<div class='badge badge-success badge-success-alt'>Active</div>");
            }
            else if (active_status == 2)
            {
                return new HtmlString($"<div class='badge badge-danger badge-danger-alt'>Rejected</div>");
            }
            else if (active_status == 3)
            {
                return new HtmlString($"<div class='badge badge-danger badge-danger-alt'>Suspended</div>");
            }
            else
            {
                return new HtmlString($"<div class='badge badge-primary badge-primary-alt'>Unknown</div>");
            }
        }

        //check if user has permission
        public static string CheckUserAccess(string account_id, string permission_name, string connection_string)
        {
            try
            {

                //get permission id
                int PermissionID = 0;
                if (ModelHelper.GetTableData("Permissions", "PermissionName", permission_name, "PermissionID", connection_string) != null)
                {
                    PermissionID = DataHelper.Int32Parse(ModelHelper.GetTableData("Permissions", "PermissionName", permission_name, "PermissionID", connection_string).ToString());
                }
                using (var db = new DBConnection())
                {
                    if (db.AccountToPermission.Any(s => s.AccountID == account_id && s.PermissionID == PermissionID))
                    {
                        return "checked";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "";
        }

        //get verifcation status badge using account id
        public static HtmlString GetVerificationState(string account_id, string verifcation_type)
        {
            using (var db = new DBConnection())
            {
                if (verifcation_type == "Phone")
                {
                    if (db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumberVerification == 0)
                    {
                        return new HtmlString($"<span class='badge badge-pill badge-danger float-right'>Unverified</span>");
                    }
                    return new HtmlString($"<span class='badge badge-pill badge-success float-right'>Verified</span>");
                }
                else if (verifcation_type == "Email")
                {
                    if (db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().EmailVerification == 0)
                    {
                        return new HtmlString($"<span class='badge badge-pill badge-danger float-right'>Unverified</span>");
                    }
                    return new HtmlString($"<span class='badge badge-pill badge-success float-right'>Verified</span>");
                }
            }
            return new HtmlString($"<span class='badge badge-pill badge-danger float-right'>Unverified</span>");
        }


        //get verifcation link if not verified
        public static HtmlString GetVerificationLink(string account_id, string verifcation_type)
        {
            using (var db = new DBConnection())
            {
                if (verifcation_type == "Phone")
                {
                    if (db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumberVerification == 0)
                    {
                        return new HtmlString($"<a href='#' class='text-decoration-none ml-4' data-toggle='modal' data-target='#verifyAccountPhoneNumberModal'>Verify</a>");
                    }
                    return new HtmlString($"<span></span>");
                }
                else if (verifcation_type == "Email")
                {
                    if (db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().EmailVerification == 0)
                    {
                        return new HtmlString($"<a href='#' class='text-decoration-none ml-4' data-toggle='modal' data-target='#verifyAccountEmailModal'>Verify</a>");
                    }
                    return new HtmlString($"<span></span>");
                }
            }
            return new HtmlString($"<span></span>");
        }


        //Checks if a user has permission access
        public static bool CheckUserAccess(string account_id, string permission_name)
        {
            if (string.IsNullOrEmpty(account_id) || string.IsNullOrEmpty(permission_name))
            {
                return false;
            }

            using (var db = new DBConnection())
            {
                //get permission id from permission name
                int PermissionID = 0;
                if (db.Permissions.Any(s => s.PermissionName == permission_name))
                {
                    PermissionID = db.Permissions.Where(s => s.PermissionName == permission_name).FirstOrDefault().PermissionID;
                }

                //check if user has permission
                if (db.AccountToPermission.Any(s => s.AccountID == account_id && s.PermissionID == PermissionID))
                {
                    return true;
                }
            }
            return false;
        }

        //Get total account notifications
        public static int GetAccountTotalNotifications(string account_id)
        {
            int TotalNotifications = 0;
            using (var db = new DBConnection())
            {
                //pending accounts 
                if (CheckUserAccess(account_id, "Admin Permissions"))
                {
                    var DBQuery0 = db.Accounts.Where(s => s.Active == 0);
                    if (DBQuery0.Any())
                    {
                        TotalNotifications += DBQuery0.Count();
                    }
                }

                //post reviews 
                var DBQuery1 = db.vwPostReviews.Where(s => s.PostAuthor == account_id && s.ApprovalState == 0);
                if (DBQuery1.Any())
                {
                    TotalNotifications += DBQuery1.Count();
                }

                //pending reviews 
                var DBQuery2 = db.vwPosts.Where(s => s.ApprovalState == 0 && s.PostEditor == account_id);
                if (DBQuery2.Any())
                {
                    TotalNotifications += DBQuery2.Count();
                }
            }
            return TotalNotifications;
        }

        //get notification action links
        public static HtmlString GetNotificationLinks(string account_id)
        {
            string result = "";
            using (var db = new DBConnection())
            {
                //pending accounts 
                if (CheckUserAccess(account_id, "Admin Permissions"))
                {
                    var DBQuery0 = db.Accounts.Where(s => s.Active == 0);
                    if (DBQuery0.Any())
                    {
                        result += @$"<a class='dropdown-item' href='/Admin/AccountRegistrations' >Account Registrations {GetTotalActionNotification(account_id, "AccountRegistrations")}</a>";
                    }
                }

                //post reviews 
                var DBQuery1 = db.vwPostReviews.Where(s => s.PostAuthor == account_id && s.ApprovalState == 0);
                if (DBQuery1.Any())
                {
                    result += @$"<a class='dropdown-item' href='/Admin/PostReviews' >Post Reviews {GetTotalActionNotification(account_id, "PostReviews")}</a>";
                }

                //pending reviews 
                var DBQuery2 = db.vwPosts.Where(s => s.ApprovalState == 0 && s.PostEditor == account_id);
                if (DBQuery2.Any())
                {
                    result += @$"<a class='dropdown-item' href='/Admin/PendingReviews' >Pending Reviews {GetTotalActionNotification(account_id, "PendingReviews")}</a>";
                }
            }
            return new HtmlString(result);
        }


        //Get total account notifications
        public static string GetTotalActionNotification(string account_id, string action)
        {

            using (var db = new DBConnection())
            {
                switch (action)
                {
                    case "AccountRegistrations":
                        //pending accounts 
                        if (CheckUserAccess(account_id, "Admin Permissions"))
                        {
                            var DBQuery0 = db.Accounts.Where(s => s.Active == 0);
                            if (DBQuery0.Any())
                            {
                                return @$"<span class='badge badge-pill badge-danger'>{DBQuery0.Count().ToString()}</span>";
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    case "PostReviews":
                        //post reviews 
                        var DBQuery1 = db.vwPostReviews.Where(s => s.PostAuthor == account_id && s.ApprovalState == 0);
                        if (DBQuery1.Any())
                        {
                            return @$"<span class='badge badge-pill badge-danger'>{DBQuery1.Count().ToString()}</span>";
                        }
                        else
                        {
                            return null;
                        }
                    case "PendingReviews":
                        //pending reviews 
                        var DBQuery2 = db.vwPosts.Where(s => s.ApprovalState == 0 && s.PostEditor == account_id);
                        if (DBQuery2.Any())
                        {
                            return @$"<span class='badge badge-pill badge-danger'>{DBQuery2.Count().ToString()}</span>";
                        }
                        else
                        {
                            return null;
                        }
                    default:
                        return null;
                }
            }
        }


    }


    public static class ModelHelper
    {
        //gete table column value base on the key passed
        public static string GetTableData(string model_name, string pk_name, string pk_value, string return_data, string connection_string)
        {
            string[] ValidationInputs = { model_name, pk_name, pk_value, return_data, connection_string };
            if (!ValidateInputs(ValidationInputs))
            {
                return "";
            }

            string return_value = "";

            var DBQuery = @"SELECT [" + return_data + "] FROM [" + model_name + "] WHERE [" + pk_name + "]  = @key";
            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(DBQuery, con);
                    cmd.Parameters.AddWithValue("@key", pk_value);
                    if (cmd.ExecuteScalar() != DBNull.Value && cmd.ExecuteScalar() != null)
                    {
                        return_value = cmd.ExecuteScalar().ToString();
                    }
                    else
                    {
                        return_value = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
                return_value = null;
            }

            return return_value;
        }


        //set edit inputs. chcks the first parameter for value, sets to second if empty
        public static string SetEditInput(object view_data_value, string data_value)
        {
            if (view_data_value != null)
            {
                return view_data_value.ToString();
            }
            return data_value;
        }


        //Validate required inputs 
        /// <summary>
        /// Validates array of inputs for not null
        /// </summary>
        /// <returns>boolean</returns>
        public static bool ValidateInputs(string[] inputs)
        {
            // Loop over and check if empty.
            for (int i = 0; i < inputs.Length; i++)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                {
                    return false;
                }
            }
            return true;
        }


        //get specific post data. Takes post id and data to return
        public static string GetSiteLookupData(string unique_key)
        {
            try
            {
                if (!string.IsNullOrEmpty(unique_key))
                {
                    using (var db = new DBConnection())
                    {
                        var DBQuery = db.SiteDataLookup.Where(s => s.UinqueKey == unique_key);
                        if (DBQuery.Any())
                        {
                            return DBQuery.FirstOrDefault().Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return null;
        }

        //get specific post data. Takes post id and data to return
        public static HtmlString GetSiteHtmlLookupData(string unique_key)
        {
            try
            {
                if (!string.IsNullOrEmpty(unique_key))
                {
                    using (var db = new DBConnection())
                    {
                        var DBQuery = db.SiteDataLookup.Where(s => s.UinqueKey == unique_key);
                        if (DBQuery.Any())
                        {
                            return new HtmlString(DBQuery.FirstOrDefault().Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return null;
        }

    }






    //  ██████╗ ██╗  ██╗    ███╗   ██╗███████╗██╗    ██╗███████╗    ██╗      █████╗ ██╗   ██╗ ██████╗ ██╗   ██╗████████╗
    //  ╚════██╗██║  ██║    ████╗  ██║██╔════╝██║    ██║██╔════╝    ██║     ██╔══██╗╚██╗ ██╔╝██╔═══██╗██║   ██║╚══██╔══╝
    //   █████╔╝███████║    ██╔██╗ ██║█████╗  ██║ █╗ ██║███████╗    ██║     ███████║ ╚████╔╝ ██║   ██║██║   ██║   ██║   
    //  ██╔═══╝ ╚════██║    ██║╚██╗██║██╔══╝  ██║███╗██║╚════██║    ██║     ██╔══██║  ╚██╔╝  ██║   ██║██║   ██║   ██║   
    //  ███████╗     ██║    ██║ ╚████║███████╗╚███╔███╔╝███████║    ███████╗██║  ██║   ██║   ╚██████╔╝╚██████╔╝   ██║   
    //  ╚══════╝     ╚═╝    ╚═╝  ╚═══╝╚══════╝ ╚══╝╚══╝ ╚══════╝    ╚══════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝  ╚═════╝    ╚═╝   
    //                                                                                                                  

    //World time template layout helper class : https://freehtml5.co/24-news-free-viral-html5-template-using-bootstrap-for-news-websites/
    public static class LayoutHelper24News
    {
        //check if there is a breaking news
        public static bool HasBreakingNews()
        {
            using(var db = new DBConnection())
            {
                DateTime BreakingTime = DateTime.Now.AddHours(-6);
                var DBQuery = db.vwPostsApproved.Where(s => s.IsBreakingNews == 1 && s.ApprovalsDateAdded >= BreakingTime && (s.PostType != "EntertainmentAudioPost" && s.PostType != "EntertainmentVideoPost")).OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get breaking news
        public static HtmlString GetBreakingNews()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    //get last breaking news
                    DateTime BreakingTime = DateTime.Now.AddHours(-6);
                    var DBQuery = db.vwPostsApproved.Where(s => s.IsBreakingNews == 1 && s.ApprovalsDateAdded >= BreakingTime && (s.PostType != "EntertainmentAudioPost" || s.PostType != "EntertainmentVideoPost")).OrderByDescending(s => s.ApprovalsDateAdded).Take(1);

                    string NewsType = "Breaking";

                    //if no breaking news, take last news
                    if (!DBQuery.Any())
                    {
                        DBQuery = db.vwPostsApproved.Where(s=> s.PostType != "EntertainmentAudioPost" && s.PostType != "EntertainmentVideoPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                        NewsType = "Latest";
                    }
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-12 fh5co_mediya_center'>
                                            <a href='/Posts/{item.PostPermalink}' class='color_fff fh5co_mediya_setting'>
                                                <i class='far fa-clock'></i>&nbsp;&nbsp;{PostHelper.FormatPostDate(DateTime.Now.ToString())}
                                            </a>
                                            <div class='d-inline-block fh5co_trading_posotion_relative'>
                                                <a href='/Posts/{item.PostPermalink}' class='treding_btn'>{NewsType}</a>
                                                <div class='fh5co_treding_position_absolute'></div>
                                            </div>
                                            <a href='/Posts/{item.PostPermalink}' class='color_fff fh5co_mediya_setting'>{TextHelper.FormatLongText(item.PostTitle, 75)}</a>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        //check if there is any news
        public static bool HasAnyNews()
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved;
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }

        //check if there is any news for post type
        public static bool HasAnyNews(string post_type)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.Where(s=> s.PostType == post_type);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //check if there is at least some news for post type
        public static bool HasAnyNews(string post_type, int minimum_number)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.Where(s => s.PostType == post_type);
                if (DBQuery.Any() && DBQuery.Count() >= minimum_number)
                {
                    return true;
                }
            }
            return false;
        }


        //check if there is any trending news
        public static bool HasAnyTrendingNews()
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPopularThisWeek;
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get trending news
        public static HtmlString GetTrendingNews(int total) 
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='item px-2'>
		                                        <div class='fh5co_hover_news_img'>
			                                        <div class='fh5co_news_img'>
                                                        {PostHelper.GetPostImagePreview(item.PostID)}
                                                    </div>
			                                        <div>
				                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='d-block fh5co_small_post_heading'>
                                                        <span class=''>{TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 75)}</span></a>
				                                        <div class='c_g'><i class='fa fa-clock-o'></i> {PostHelper.FormatPostDate(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded"))}</div>
			                                        </div>
		                                        </div>
	                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get trending news home
        public static HtmlString GetTrendingHomeNews(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_latest_trading_img_position_relative'>
		                                        <div class='fh5co_latest_trading_img'>
		                                        {PostHelper.GetPostImagePreview(item.PostID)}
		                                        </div>
		                                        <div class='fh5co_latest_trading_img_position_absolute'></div>
		                                        <div class='fh5co_latest_trading_img_position_absolute_1'>
			                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-white'> 
				                                        {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 75)}
			                                        </a>
			                                        <div class='fh5co_latest_trading_date_and_name_color'> 
                                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-white'> 
				                                            {AccountHelper.GetAccountData(PostHelper.GetPostData(item.PostID, "PostAuthor"), "FullName")} - {PostHelper.FormatPostDate(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded"))}
                                                        </a>			                                        
                                                    </div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }



        //check if there is any video news
        public static bool HasAnyVideoNews()
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "NewsVideoPost" && s.PostVideoType == "YouTube");
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }

        //get video news
        public static HtmlString GetVideoNews(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    //TODO add vimeo type
                    var DBQuery = db.vwPostsApproved.Where(s=> s.PostType == "NewsVideoPost" && s.PostVideoType == "YouTube").OrderByDescending(s => s.ApprovalsDateAdded).Take(total);

                    if (DBQuery.Any())
                    {
                        int count = 1;
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string img_class = (count >= 2) ? "_"+count : "";

                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_hover_news_img'>
		                                        <div class='fh5co_hover_news_img_video_tag_position_relative'>
			                                        <div class='fh5co_news_img'>
				                                        <iframe id='video{img_class}' class='w-100' height='200'
						                                        src='https://www.youtube.com/embed/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}?rel=0&amp;showinfo=0'
						                                        frameborder='0' allowfullscreen></iframe>
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute fh5co_hide{img_class}'>
				                                        <img src='https://i1.ytimg.com/vi/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}/sddefault.jpg' alt='' />
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1 fh5co_hide{img_class}' id='play-video{img_class}'>
				                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button_1'>
					                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button'>
						                                        <span><i class='fa fa-play'></i></span>
					                                        </div>
				                                        </div>
			                                        </div>
		                                        </div>
		                                        <div class='pt-2'>
			                                        <div>
				                                        <a href='/Posts/{item.PostPermalink}' class='d-block fh5co_small_post_heading'>
					                                        <span class=''>{TextHelper.FormatLongText(item.PostTitle, 75)}</span>
				                                        </a>
				                                        <div class='c_g'><i class='far fa-clock'></i> {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                            count++;
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get video news
        public static HtmlString GetVideoNews(string post_type, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    //TODO add vimeo type
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == post_type && s.PostVideoType == "YouTube").OrderByDescending(s => s.ApprovalsDateAdded).Take(total);

                    if (DBQuery.Any())
                    {
                        int count = 1;
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string img_class = (count >= 2) ? "_" + count : "";

                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_hover_news_img'>
		                                        <div class='fh5co_hover_news_img_video_tag_position_relative'>
			                                        <div class='fh5co_news_img'>
				                                        <iframe id='video{img_class}' class='w-100' height='200'
						                                        src='https://www.youtube.com/embed/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}?rel=0&amp;showinfo=0'
						                                        frameborder='0' allowfullscreen></iframe>
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute fh5co_hide{img_class}'>
				                                        <img src='https://i1.ytimg.com/vi/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}/sddefault.jpg' alt='' />
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1 fh5co_hide{img_class}' id='play-video{img_class}'>
				                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button_1'>
					                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button'>
						                                        <span><i class='fa fa-play'></i></span>
					                                        </div>
				                                        </div>
			                                        </div>
		                                        </div>
		                                        <div class='pt-2'>
			                                        <div>
				                                        <a href='/Posts/{item.PostPermalink}' class='d-block fh5co_small_post_heading'>
					                                        <span class=''>{TextHelper.FormatLongText(item.PostTitle, 75)}</span>
				                                        </a>
				                                        <div class='c_g'><i class='far fa-clock'></i> {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                            count++;
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        //check if there is a featured news
        public static bool HasFeaturedNews()
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.Where(s => s.FeaturedPost == 1 && (s.PostType != "EntertainmentAudioPost" || s.PostType == "EntertainmentVideoPost"));
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get featured news home. Horizontal news
        public static HtmlString GetFeaturedNews(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    //exclude most recent post by id
                    string RecentPostID = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).FirstOrDefault().PostID;
                    //var DBQuery = db.vwPostsApproved.Where(s => s.PostID != RecentPostID && s.FeaturedPost == 1).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                    var DBQuery = db.vwPostsApproved.Where(s => s.FeaturedPost == 1 && (s.PostType != "EntertainmentAudioPost" || s.PostType == "EntertainmentVideoPost")).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='item px-2'>
		                                        <div class='fh5co_hover_news_img'>
			                                        <div class='fh5co_news_img'>
                                                        {PostHelper.GetPostImagePreview(item.PostID)}
                                                    </div>
			                                        <div>
				                                        <a href='/Posts/{item.PostPermalink}' class='d-block fh5co_small_post_heading'>
                                                        <span class=''>{TextHelper.FormatLongText(item.PostTitle, 75)}</span></a>
				                                        <div class='c_g'><i class='fa fa-clock-o'></i> {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</div>
			                                        </div>
		                                        </div>
	                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //check if there is any related news
        public static bool HasRelatedNews(string post_id)
        {
            using (var db = new DBConnection())
            {
                string PostCategory = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostCategory;
                string PostTags = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostTags;
                string PostType = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostType;
                var DBQuery = db.vwPostsApproved.Where(s => (s.PostCategory == PostCategory || s.PostTags.Contains(PostTags) || s.PostType == PostType) && s.PostID != post_id);

                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get related news
        public static HtmlString GetRelatedNews(string post_id, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    string PostCategory = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostCategory;
                    string PostTags = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostTags;
                    string PostType = db.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().PostType;

                    //check for spcific types like videos or music, then more related types like category ot tags
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == PostType && s.PostID != post_id).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                    if (!DBQuery.Any())
                    {
                        DBQuery = db.vwPostsApproved.Where(s => (s.PostCategory == PostCategory || s.PostType == PostType) && s.PostID != post_id).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                        if (!DBQuery.Any())
                        {
                            DBQuery = db.vwPostsApproved.Where(s => (s.PostCategory == PostCategory || s.PostTags.Contains(PostTags) || s.PostType == PostType) && s.PostID != post_id).Take(total);
                        }
                    }

                    
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_hover_news_img'>
		                                        <div class='fh5co_news_img'>
                                                    {PostHelper.GetPostImagePreview(item.PostID)}
                                                </div>
		                                        <div>
			                                        <a href='/Posts/{item.PostPermalink}' class='d-block fh5co_small_post_heading'><span class=''>{TextHelper.FormatLongText(item.PostTitle, 75)}</span></a>
			                                        <div class='c_g'><i class='fa fa-clock-o'></i> {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //check if there is any home body news
        public static bool HasAnyHomeBodyNews(int skip, int total)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.Where(s =>  s.PostType != "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }

        //get home body news
        public static HtmlString GetHomeBodyNews(int skip, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType != "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(item.PostContent)) ? TextHelper.FormatLongText(TextHelper.StripHTML(item.PostContent), 150) : "";

                            result += @$"<div class='row pb-4'>
                                            <div class='col-md-5'>
                                                <div class='fh5co_hover_news_img'>
                                                    <div class='fh5co_news_img'>{PostHelper.GetPostImagePreview(item.PostID)}</div>
                                                    <div></div>
                                                </div>
                                            </div>
                                            <div class='col-md-7'>
                                                <a href='/Posts/{item.PostPermalink}' class='fh5co_magna py-2'>
                                                    {TextHelper.FormatLongText(item.PostTitle, 75)}
                                                </a> <a href='/Posts/{item.PostPermalink}' class='fh5co_mini_time py-3'>
                                                    {AccountHelper.GetAccountData(item.PostAuthor, "FullName")} -
                                                    {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}
                                                </a>
                                                <div class='fh5co_consectetur'>
				                                    {PostContent}
                                                </div>
                                            </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //check if there is any more body news
        public static bool HasMoreBodyNews(int skip, int total)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }

        //get more body news
        public static HtmlString GetMoreBodyNews(int skip, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType != "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-sm-12 col-md-6 col-lg-4 mb-2'>
											<div class='item px-2'>
												<div class='fh5co_hover_news_img'>
													<div class='fh5co_news_img'>{PostHelper.GetPostImagePreview(item.PostID)}</div>
													<div>
														<a href='/Posts/{item.PostPermalink}' class='d-block fh5co_small_post_heading'><span class=''>{TextHelper.FormatLongText(item.PostTitle, 75)}</span></a>
														<div class='c_g'><i class='far fa-clock'></i> {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</div>
													</div>
												</div>
											</div>
										</div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //check if there is any news for api category
        public static bool HasApiNews(string category, int total)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.NewsApi.Where(s=> s.Category == category).Take(total);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }


        //get news api news
        public static HtmlString GetNewsApi(string category, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.NewsApi.Where(s => s.Category == category).OrderByDescending(s => s.ID).Take(total);

                    if (DBQuery.Any())
                    {
                        result = $@"<div class='col-12 mb-1'>
                                        <h2 class='text-left'>{category}</h2>
                                    </div>";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-sm-12 col-md-6 col-lg-4 mb-2'>
											<div class='item px-2'>
												<div class='fh5co_hover_news_img'>
													<div class='fh5co_news_img'>
                                                        <a href='{item.Url}' target='_blank'>
														    <img src='{item.UrlToImage}' class='img-fluid w-100 h-100' alt='{item.Title}'>
                                                        </a>
													</div>
													<div>
														<a href='{item.Url}' target='_blank' class='d-block fh5co_small_post_heading'><span class=''>{TextHelper.FormatLongText(item.Title, 75)}</span></a>
														<div class='c_g'><i class='far fa-clock'></i> {PostHelper.FormatPostDate(item.PublishedAt)}</div>
													</div>
												</div>
											</div>
										</div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //format post tags
        public static HtmlString FormatPostTags(string post_tags)
        {
            string result = "";
            string[] elements = post_tags.Split(',');
            // generate tags list
            foreach (string item in elements)
            {
                result += $"<a href='/Tags/{item}' class='fh5co_tagg'>{TextHelper.ConvertCase(item, "SplitUpper")}</a>";
            }
            return new HtmlString(result);
        }


        //format post tags
        public static HtmlString FormatPostTags(string post_tags, int total)
        {
            string result = "";
            string[] elements = post_tags.Split(',');
            // generate tags list
            int count = 1;
            ArrayList TagsList = new ArrayList();

            foreach (string item in elements)
            {
                if(count <= total)
                {
                    //check to avoid duplicate listing
                    if (!TagsList.Contains(TextHelper.ConvertCase(item, "SplitUpper")))
                    {
                        result += $"<a href='/Tags/{item}' class='fh5co_tagg'>{TextHelper.ConvertCase(item, "SplitUpper")}</a>";
                    }
                    TagsList.Add(TextHelper.ConvertCase(item, "SplitUpper"));
                }
                count++;
            }
            return new HtmlString(result);
        }


        //get popular news
        public static HtmlString GetPopularNews(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='row pb-3'>
                                            <div class='col-5 align-self-center zoom-xm'>
                                                {PostHelper.GetPostImagePreview(item.PostID, "fh5co_most_trading")}
                                            </div>
                                            <div class='col-7 paddding'>
                                                <div class='most_fh5co_treding_font'>
						                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-decoration-none text-dark'>
							                        {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 50)}
						                        </a>
						                        </div>
                                                <div class='most_fh5co_treding_font_123'> {PostHelper.FormatPostDate(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded"))}</div>
                                            </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get breaking news
        public static string GetPopularTags(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"{PostHelper.GetPostData(item.PostID, "PostTags")},";
                        }
                    }
                    return result.TrimEnd(','); ;
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return result.TrimEnd(','); ;
        }


        //get tags from popolar news
        public static string GetPopularTags()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(10);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"{PostHelper.GetPostData(item.PostID, "PostTags")},";
                        }
                    }
                    return result.TrimEnd(','); ;
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return result.TrimEnd(','); ;
        }

        //get categories dropdown for navigation
        public static HtmlString GetNavCategoryDropdown(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderBy(s => s.CategoryOrder).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += $"<a class='dropdown-item' href='/Category/{item.ShortCategoryName}'>{item.CategoryName}</a>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get categories list for navigation
        public static HtmlString GetNavCategoryList(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderBy(s => s.CategoryOrder).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<li class='nav-item'>
	                                        <a class='nav-link' id='{item.ShortCategoryName}' href='/Category/{item.ShortCategoryName}'>{item.CategoryName} <span class='sr-only'>(current)</span></a>
                                        </li>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        // Get home featured news
        public static HtmlString GetFeaturedHomeNews()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "StandardNewsPost" || s.PostType == "NewsGalleryPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                    if (DBQuery.Any())
                    {
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='fh5co_suceefh5co_height zoom-xm'>
	                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='img'/>
	                                        <div class='fh5co_suceefh5co_height_position_absolute'></div>
	                                        <div class='fh5co_suceefh5co_height_position_absolute_font'>
		                                        <div class=''>
			                                        <a href='/Posts/{item.PostPermalink}' class='color_fff'>
				                                        <i class='far fa-clock'></i>&nbsp;&nbsp; {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}
			                                        </a>
		                                        </div>
		                                        <div class=''><a href='/Posts/{item.PostPermalink}' class='fh5co_good_font'> {TextHelper.FormatLongText(item.PostTitle, 75)} </a></div>
	                                        </div>
                                        </div>";
                        }
                        return new HtmlString(result);
                    }

                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }


        // Get other home featured news
        public static HtmlString GetOtherFeaturedHomeNews()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "StandardNewsPost" || s.PostType == "NewsGalleryPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(1).Take(4);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-md-6 col-6 paddding animate-box' data-animate-effect='fadeIn'>
	                                        <div class='fh5co_suceefh5co_height_2 zoom-xm'>
                                            <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='img'/>
		                                        <div class='fh5co_suceefh5co_height_position_absolute'></div>
		                                        <div class='fh5co_suceefh5co_height_position_absolute_font_2'>
			                                        <div class=''>
				                                        <a href='/Posts/{item.PostPermalink}' class='color_fff'> 
				                                        <i class='far fa-clock'></i>&nbsp;&nbsp; {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())} 
				                                        </a>
			                                        </div>
			                                        <div class=''><a href='/Posts/{item.PostPermalink}' class='fh5co_good_font_2'> {TextHelper.FormatLongText(item.PostTitle, 75)} </a></div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get footer category links
        public static HtmlString GetFooterCategoryLinks(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderByDescending(x => Guid.NewGuid()).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<li><a href='/Category/{item.ShortCategoryName}' class=''><i class='fa fa-angle-right'></i>&nbsp;&nbsp; {item.CategoryName}</a></li>";
                        }

                        //add sitemap link
                        //result += $@"<li><a href='/Sitemap' target='_blank' class='mt-2'><i class='fa fa-angle-right'></i>&nbsp;&nbsp; SiteMap</a></li>";
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        //get most viewed this week footer
        public static HtmlString GetTrendingNewsFooter(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(PostHelper.GetPostData(item.PostID, "PostContent"))) ? TextHelper.FormatLongText(TextHelper.StripHTML(PostHelper.GetPostData(item.PostID, "PostContent")), 100) : "";

                            result += @$"<div class='footer_makes_sub_font'> {PostHelper.FormatPostDate(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded"))}</div>
									<a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='footer_post pb-4'> {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 75)} </a>
								";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get recent news footer
        public static HtmlString GetRecentNewsFooter(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s=> s.PostType == "StandardNewsPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<a href='/Posts/{item.PostPermalink}' class='footer_img_post_6 zoom-sm'><img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='img'/></a>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //========== CELEBRITY NEWS HELPERS ==========//
        // Get celebrity featured news
        public static HtmlString GetFeaturedCelebrityNews()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentNewsPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                    if (DBQuery.Any())
                    {
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='fh5co_suceefh5co_height zoom-xm'>
                                            <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='img' />
                                            <div class='fh5co_suceefh5co_height_position_absolute'></div>
                                            <div class='fh5co_suceefh5co_height_position_absolute_font'>
                                                <div class=''>
                                                    <a href='/Posts/{item.PostPermalink}' class='color_fff'>
                                                        <i class='far fa-clock'></i>&nbsp;&nbsp;{PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}
                                                    </a>
                                                </div>
                                                <div class=''><a href='/Posts/{item.PostPermalink}' class='fh5co_good_font'> {TextHelper.FormatLongText(item.PostTitle, 75)} </a></div>
                                            </div>
                                        </div>";
                        }
                        return new HtmlString(result);
                    }

                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }

        // Get other home featured news
        public static HtmlString GetOtherFeaturedCelebrityNews(int skip, int take)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentNewsPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(take);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-md-12 col-6 paddding animate-box' data-animate-effect='fadeIn'>
	                                        <div class='fh5co_suceefh5co_height_2 zoom-xm'>
		                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='img' />
		                                        <div class='fh5co_suceefh5co_height_position_absolute'></div>
		                                        <div class='fh5co_suceefh5co_height_position_absolute_font_2'>
			                                        <div class=''><a href='/Posts/{item.PostPermalink}' class='color_fff'> <i class='far fa-clock'></i>&nbsp;&nbsp; {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}</a></div>
			                                        <div class=''><a href='/Posts/{item.PostPermalink}' class='fh5co_good_font_2'> {TextHelper.FormatLongText(item.PostTitle, 75)} </a></div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get trending news celebrity
        public static HtmlString GetTrendingCelebrityNews(int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s=> (s.PostType == "EntertainmentNewsPost" || s.PostType == "EntertainmentVideoPost" || s.PostType == "EntertainmentAudioPost") && s.PostType != "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_latest_trading_img_position_relative'>
		                                        <div class='fh5co_latest_trading_img'>
                                                    <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}'> 
		                                                {PostHelper.GetPostImagePreview(item.PostID)}
                                                    </a>
		                                        </div>
		                                        <div class='fh5co_latest_trading_img_position_absolute'></div>
		                                        <div class='fh5co_latest_trading_img_position_absolute_1'>
			                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-white'> 
				                                        {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 75)}
			                                        </a>
			                                        <div class='fh5co_latest_trading_date_and_name_color'> 
                                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-white'> 
		                                                    {AccountHelper.GetAccountData(PostHelper.GetPostData(item.PostID, "PostAuthor"), "FullName")} - {PostHelper.FormatPostDate(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded"))}
                                                        </a>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        //get celebrity body news
        public static HtmlString GetCelebrityBodyNews(int skip, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentNewsPost" || s.PostType == "EntertainmentVideoPost" || s.PostType == "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(item.PostContent)) ? TextHelper.FormatLongText(TextHelper.StripHTML(item.PostContent), 150) : "";

                            result += @$"<div class='row pb-4'>
                                            <div class='col-md-5'>
                                                <div class='fh5co_hover_news_img'>
                                                    <div class='fh5co_news_img'>{PostHelper.GetPostImagePreview(item.PostID)}</div>
                                                    <div></div>
                                                </div>
                                            </div>
                                            <div class='col-md-7'>
                                                <a href='/Posts/{item.PostPermalink}' class='fh5co_magna py-2'>
                                                    {TextHelper.FormatLongText(item.PostTitle, 75)}
                                                </a> <a href='/Posts/{item.PostPermalink}' class='fh5co_mini_time py-3'>
                                                    {AccountHelper.GetAccountData(item.PostAuthor, "FullName")} -
                                                    {PostHelper.FormatPostDate(item.ApprovalsDateAdded.ToString())}
                                                </a>
                                                <div class='fh5co_consectetur'>
				                                    {PostContent}
                                                </div>
                                            </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }



        // Get video featured news
        public static HtmlString GetFeaturedVideoNews()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentVideoPost").OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                    if (DBQuery.Any())
                    {
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='embed-responsive embed-responsive-16by9'>
	                                        <iframe class='embed-responsive-item' id='main-featured-vid' src='https://www.youtube.com/embed/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}' allowfullscreen></iframe>
                                            </div>";
                        }
                        return new HtmlString(result);
                    }

                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }


        // Get other video featured news
        public static HtmlString GetOtherFeaturedVideoNews(int skip, int take)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentVideoPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(take);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-md-4 col-12 paddding animate-box' data-animate-effect='fadeIn'>
                                            <div class='m-2'>
	                                            <a href='#!' class='featured-vid' id='{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}'>
		                                            <img src='https://i1.ytimg.com/vi/{PostHelper.GetYouTubeVideoID(item.PostVideoLink)}/hqdefault.jpg' class='w-100 m-2' alt='Cinque Terre' height='220'>
	                                            </a>
                                            </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get top 10 video news
        public static HtmlString GetTopTenVideos()
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    //TODO add vimeo type
                    var DBQuery = db.TopTenList.Where(s => s.ListType == "MusicVideos").OrderBy(s => s.ListOrder);

                    if (DBQuery.Any())
                    {
                        int count = 1;
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string img_class = (count >= 2) ? "_" + count : "";

                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_hover_news_img'>
		                                        <div class='fh5co_hover_news_img_video_tag_position_relative'>
			                                        <div class='fh5co_news_img'>
				                                        <iframe id='video{img_class}' class='w-100' height='200'
						                                        src='https://www.youtube.com/embed/{PostHelper.GetYouTubeVideoID(item.ListLink)}?rel=0&amp;showinfo=0'
						                                        frameborder='0' allowfullscreen></iframe>
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute fh5co_hide{img_class}'>
				                                        <img src='https://i1.ytimg.com/vi/{PostHelper.GetYouTubeVideoID(item.ListLink)}/sddefault.jpg' alt='' />
			                                        </div>
			                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1 fh5co_hide{img_class}' id='play-video{img_class}'>
				                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button_1'>
					                                        <div class='fh5co_hover_news_img_video_tag_position_absolute_1_play_button'>
						                                        <span><i class='fa fa-play'></i></span>
					                                        </div>
				                                        </div>
			                                        </div>
		                                        </div>
		                                        <div class='pt-2'>
			                                        <div>
				                                        <a href='{item.ListLink}' target='_blank' class='d-block fh5co_small_post_heading'>
					                                        <span class=''>{count} - {item.ListTitle}</span>
				                                        </a>
				                                        <div class='c_g'><i class='far fa-clock'></i> {PostHelper.FormatPostDate(item.UpdateDate.ToString())}</div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                            count++;
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }



        // Get top featured music
        public static HtmlString GetFeaturedMusic(int skip, int take)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(take);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string AudioDirectory = DateTime.Parse(item.ApprovalsDateAdded.ToString()).ToString("MM-yyyy");

                            result += @$"<div class='card'>
                                              <img class='card-img-top img-fluid mp3-img-card zoom-xm' src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='cover image'>
                                              <div class='card-body p-2'>
	                                            <a href='/Posts/{item.PostPermalink}'>
                                                    <h5 class='card-title'><i class='fas fa-music mr-2'></i>{item.PostTitle}</h5>
	                                                <p class='card-text'>{item.PostExtract}</p>
                                                </a>
	                                            <div class='bg-secondary border border-dark rounded pt-2 mb-2 text-center'>
	                                              <audio controls controlsList='nodownload'>
		                                            <source src='/files/audios/{AudioDirectory}/{item.PostAudioLink}' type='audio/mpeg'>
		                                            Your browser does not support the audio element.
	                                              </audio>
	                                            </div>
	                                            <a type='application/octet-stream' href='/files/audios/{AudioDirectory}/{item.PostAudioLink}' download class='btn btn-outline-secondary float-right'>
		                                            <i class='fas fa-download'></i>
		                                            Download
	                                            </a>
                                              </div>
                                            </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        // Get trending music
        public static HtmlString GetTrendingMusic(int skip, int take)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "EntertainmentAudioPost").OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(take);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string AudioDirectory = DateTime.Parse(item.ApprovalsDateAdded.ToString()).ToString("MM-yyyy");

                            result += @$"<div class='item px-2'>
	                                        <div class='fh5co_hover_news_img'>
		                                        <div class='row'>
			                                        <div class='col-sm-12 paddding animate-box' data-animate-effect='fadeIn'>
				                                        <div class='card'>
				                                          <img class='card-img-top img-fluid mp3-img-card-sm zoom-xm' src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='cover image'>
				                                          <div class='card-body p-2'>
					                                        <a href='/Posts/{item.PostPermalink}'>
                                                                <h5 class='card-title'><i class='fas fa-music mr-2'></i>{item.PostTitle}</h5>
	                                                            <p class='card-text'>{item.PostExtract}</p>
                                                            </a>
					                                        <div class='bg-secondary border border-dark rounded pt-2 mb-2 text-center'>
					                                          <audio controls controlsList='nodownload'>
						                                        <source src='/files/audios/{AudioDirectory}/{item.PostAudioLink}' type='audio/mpeg'>
						                                        Your browser does not support the audio element.
					                                          </audio>
					                                        </div>
					                                        <a type='application/octet-stream' href='/files/audios/{AudioDirectory}/{item.PostAudioLink}' download class='btn btn-outline-secondary float-right'>
						                                        <i class='fas fa-download'></i>
						                                        Download
					                                        </a>
				                                          </div>
				                                        </div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }





        // Get embedded music
        public static HtmlString GetEmbeddedMusic(string embed_type, string div_class, int total)
        {
            string result = "";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.EmbeddedMusic.Where(s => s.EmbedType == embed_type).OrderByDescending(s => s.UpdateDate).Take(total);

                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='{div_class}'>
                                            {item.EmbedCode}
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //check if there is any embed post for type
        public static bool HasAnyEmbedPost(string embed_type)
        {
            using (var db = new DBConnection())
            {
                var DBQuery = db.EmbeddedMusic.Where(s=> s.EmbedType == embed_type);
                if (DBQuery.Any())
                {
                    return true;
                }
            }
            return false;
        }



    }












    //  ██╗    ██╗ ██████╗ ██████╗ ██╗     ██████╗     ████████╗██╗███╗   ███╗███████╗    ██╗      █████╗ ██╗   ██╗ ██████╗ ██╗   ██╗████████╗
    //  ██║    ██║██╔═══██╗██╔══██╗██║     ██╔══██╗    ╚══██╔══╝██║████╗ ████║██╔════╝    ██║     ██╔══██╗╚██╗ ██╔╝██╔═══██╗██║   ██║╚══██╔══╝
    //  ██║ █╗ ██║██║   ██║██████╔╝██║     ██║  ██║       ██║   ██║██╔████╔██║█████╗      ██║     ███████║ ╚████╔╝ ██║   ██║██║   ██║   ██║   
    //  ██║███╗██║██║   ██║██╔══██╗██║     ██║  ██║       ██║   ██║██║╚██╔╝██║██╔══╝      ██║     ██╔══██║  ╚██╔╝  ██║   ██║██║   ██║   ██║   
    //  ╚███╔███╔╝╚██████╔╝██║  ██║███████╗██████╔╝       ██║   ██║██║ ╚═╝ ██║███████╗    ███████╗██║  ██║   ██║   ╚██████╔╝╚██████╔╝   ██║   
    //   ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═════╝        ╚═╝   ╚═╝╚═╝     ╚═╝╚══════╝    ╚══════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝  ╚═════╝    ╚═╝   
    //                                                                                                                                        
    //World time template layout helper class : https://www.bootstrapdash.com/demo/world-time/index.html
    public static class LayoutHelperWorldTime
    {
        //get categories for navigation
        public static HtmlString GetNavCategories()
        {
            string result = "<span class='text-danger d-none'>No categories available</span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderBy(s=> s.CategoryOrder);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += $"<li class='nav-item'><a class='nav-link' href='/Category/{item.ShortCategoryName}'>{item.CategoryName}</a></li>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get categories for navigation with total 
        public static HtmlString GetNavCategories(int total)
        {
            string result = "<span class='text-danger d-none'>No categories available</span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderBy(s => s.CategoryOrder).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += $"<li class='nav-item'><a class='nav-link' href='/Category/{item.ShortCategoryName}'>{item.CategoryName}</a></li>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        //get side category name using category id
        public static HtmlString GetSideCategories()
        {
            string result = "<span class='text-danger d-none'>No categories available</span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1).OrderBy(s=> s.CategoryOrder);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += $"<li><a href='/Category/{item.ShortCategoryName}'>{item.CategoryName}</a></li>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get side category for small view
        public static HtmlString GetSideCategoriesSmall()
        {
            string result = "<span class='text-danger d-none'>No categories available</span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1).OrderBy(s => s.CategoryOrder);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-12 mb-2 zoom-xm'>
							                <div class='card'>
								                <div class='card-header bg-light'>
									                <a class='card-link' href='/Category/{item.ShortCategoryName}'>
										                <span class='text-dark'>{item.CategoryName}</span>
									                </a>
								                </div>
							                </div>
						                </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }

        // Get home featured news
        public static HtmlString GetFeaturedHomeNews(string featured_categories)
        {
            string result = @"<span class='text-danger'>No post available</span>";
            try
            {
                using(var db = new DBConnection()) {
                    if (db.vwPostsApproved.Any() && !string.IsNullOrEmpty(PostHelper.GetFeaturedNewsID(featured_categories)))
                    {
                        string FeaturedNewsID = PostHelper.GetFeaturedNewsID(featured_categories);
                        var DBQuery = db.vwPostsApproved.Where(s => s.PostID == FeaturedNewsID);
                        foreach(var item in DBQuery)
                        {
                            result = @$"<div class='position-relative w-100'>
                                            <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}' alt='banner' class='img-fluid featured-img rounded' />
                                            <div class='banner-content'>
                                                <div class='badge badge-danger fs-12 font-weight-bold mb-3'>
                                                    {PostHelper.GetCategoryName(item.PostCategory)}
                                                </div>
                                                <a href='/Posts/{item.PostPermalink}' class='text-white text-bordered text-decoration-none'>
                                                    <h1 class='mb-2'>
                                                        {TextHelper.FormatLongText(item.PostTitle, 150)}
                                                    </h1>
                                                </a>
                                                <div class='fs-12'>
                                                    <span class='mr-2 mb-2'> {GetTimeSince(item.ApprovalsDateAdded)} ago </span> 
                                                </div>
                                            </div>
                                        </div>";
                        }
                        return new HtmlString(result);
                    }
                    
                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }



        /// Get latest news for home page
        public static HtmlString GetLatestNewsHome(int total)
        {
            string result = "<span class='text-danger'></span>";

            try
            {
                using (var db = new DBConnection())
                {
                    if (db.vwPostsApproved.Any())
                    {
                        result = "";
                        var DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='d-flex border-bottom-blue pt-3 pb-4 align-items-center justify-content-between'>
	                                        <div class='pr-3 w-75'>
	                                          <a href='/Posts/{item.PostPermalink}' class='text-white text-decoration-none'>
		                                        <h5>{TextHelper.FormatLongText(item.PostTitle, 100)}</h5>
		                                        <div class='fs-12'>
		                                          <span class='mr-2'> {GetTimeSince(item.ApprovalsDateAdded)} ago </span>
		                                        </div>
	                                          </a>
	                                        </div>
	                                        <div class='rotate-img'>
	                                            <div class='row'>
                                                    <a href='/Posts/{item.PostPermalink}' class='text-white text-decoration-none'>
		                                            <img
		                                                src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
		                                                alt='thumb'
		                                                class='rounded latest-img'
		                                            />
	                                                </a>   
	                                            </div> 
                                            </div> 
                                        </div>";
                        }

                        return new HtmlString(result);
                    }

                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }


        //get home top stories
        public static HtmlString GetTopStoriesHome(int total)
        {
            string result = "<span class='text-danger'>No stories available</span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.OrderByDescending(s=> s.ApprovalsDateAdded).Skip(4).Take(total);
                    if (!DBQuery.Any())
                    {
                        DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Skip(0).Take(total);
                    }
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(item.PostContent)) ? TextHelper.FormatLongText(TextHelper.StripHTML(item.PostContent), 150) : "";
                            result += @$"<div class='row'>
	                                        <div class='col-sm-4 grid-margin'>
		                                        <div class='position-relative'>
			                                        <div class='rotate-img'>
				                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
					                                         alt='thumb'
					                                         class='img-fluid rounded'/>
			                                        </div>
			                                        <div class='badge-positioned'>
				                                        <span class='badge badge-dark font-weight-bold'>{PostHelper.GetCategoryName(item.PostCategory)}</span>
			                                        </div>
		                                        </div>
	                                        </div>
	                                        <div class='col-sm-8  grid-margin'> 
		                                        <h2 class='mb-2 font-weight-600'>
			                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
				                                        {TextHelper.FormatLongText(item.PostTitle,75)}
			                                        </a>
		                                        </h2>
		                                        <div class='fs-13 mb-2'>
			                                        <span class='mr-2'> {GetTimeSince(item.ApprovalsDateAdded)} ago </span>
		                                        </div>
		                                        <p class='mb-0'>
			                                        {PostContent}
		                                        </p>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }



        //get popular this week main
        public static HtmlString GetPopularThisWeekMain()
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    string PostID = null;
                    var PostData = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(1);
                    if (PostData.Any())
                    {
                        PostID = PostData.FirstOrDefault().PostID;
                    }
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostID == PostID);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(item.PostContent))? TextHelper.FormatLongText(TextHelper.StripHTML(item.PostContent), 100) : "";
                            result += @$"<div class='col-xl-6 col-lg-8 col-sm-6'>
	                                        <div class='rotate-img'>
		                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
			                                         alt='thumb'
			                                         class='img-fluid rounded' />
	                                        </div>
	                                        <h2 class='mt-3 text-primary mb-2'>
		                                        <a href='/Posts/{item.PostPermalink}' class='text-primary text-decoration-none'>
			                                        {TextHelper.FormatLongText(item.PostTitle, 50)}
		                                        </a>
	                                        </h2>
	                                        <p class='fs-13 mb-1 text-muted'>
		                                        <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
	                                        </p>
	                                        <p class='my-3 fs-15'>
		                                        {PostContent}
	                                        </p>
	                                        <a href='/Posts/{item.PostPermalink}' class='font-weight-600 fs-16 text-dark'>Read more</a>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get popular this week other
        public static HtmlString GetPopularThisWeekOther(int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Skip(1).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        int count = 1;
                        string div_type = " class='border-bottom pb-3 mb-3'";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(PostHelper.GetPostData(item.PostID, "PostContent"))) ? TextHelper.FormatLongText(TextHelper.StripHTML(PostHelper.GetPostData(item.PostID, "PostContent")), 100) : "";
                            div_type = (count == 4)? "" : " class='border-bottom pb-3 mb-3'";

                            result += @$"<div{div_type}>
	                                        <h3 class='font-weight-600 mb-0'>
		                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-dark text-decoration-none'>
			                                        {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 50)}
		                                        </a>
	                                        </h3>
	                                        <p class='fs-13 text-muted mb-0'>
		                                        <span class='mr-2'>{GetTimeSince(Convert.ToDateTime(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded")))} ago</span>
	                                        </p>
	                                        <p class='mb-0'>
		                                        {PostContent}
                                            </p>
                                        </div>";
                            count++;
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get news for a category (used in lover featured in home)
        public static HtmlString GetCategoryNews(string category_id, int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    string CategoryName = PostHelper.GetCategoryName(category_id);
                    if (!string.IsNullOrEmpty(CategoryName))
                    {
                        string[] CategoryNameArray = CategoryName.Split(" ");
                        var DBQuery = db.vwPostsApproved.Where(s => s.PostCategory == category_id || s.PostTitle.Contains(CategoryName) || s.PostTags.Contains(CategoryName)).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                        if (DBQuery.Any())
                        {
                            result = "";
                            foreach (var item in DBQuery)
                            {
                                result += @$"<div class='row'>
	                                        <div class='col-sm-12'>
		                                        <div class='border-bottom pb-3'>
			                                        <div class='row'>
				                                        <div class='col-sm-5 pr-2'>
					                                        <div class='rotate-img'>
						                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
							                                         alt='thumb'
							                                         class='img-fluid w-100 rounded' />
					                                        </div>
				                                        </div>
				                                        <div class='col-sm-7 pl-2'>
					                                        <p class='fs-16 font-weight-400 mb-0'>
                                                                <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-dark text-decoration-none'>
			                                                        {TextHelper.FormatLongText(item.PostTitle, 50)}
		                                                        </a>
					                                        </p>
					                                        <p class='fs-13 text-muted mb-0'>
						                                        <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
					                                        </p>
				                                        </div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                            }
                        }
                        return new HtmlString(result);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get featured home news
        public static HtmlString GetFeaturedBodyNews(int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    if (db.vwPostsApproved.Any())
                    {
                        //exclude most recent post by id
                        string RecentPostID = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).FirstOrDefault().PostID;
                        var DBQuery = db.vwPostsApproved.Where(s => s.PostID != RecentPostID && s.FeaturedPost == 1).OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                        if (DBQuery.Any())
                        {
                            result = "";
                            foreach (var item in DBQuery)
                            {
                                result += @$"<div class='border-bottom pb-3'>
	                                        <div class='rotate-img'>
		                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
			                                         alt='thumb'
			                                         class='img-fluid rounded' />
	                                        </div>
	                                        <p class='fs-16 font-weight-600 mb-0 mt-3'>
		                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
			                                        {TextHelper.FormatLongText(item.PostTitle, 60)}
		                                        </a>
	                                        </p>
	                                        <p class='fs-13 text-muted mb-0'>
		                                        <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
	                                        </p>
                                        </div>";
                            }
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get latest videos and gallery 
        public static HtmlString GetVideoGalleriesHome(int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "Gallery" || s.PostType == "Video").OrderByDescending(s=> s.ApprovalsDateAdded).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string post_icon = (item.PostType == "Video") ? "mdi mdi-play" : "fas fa-image";
                            result += @$"<div class='col-sm-6 grid-margin'>
	                                    <div class='position-relative'>
		                                    <div class='rotate-img'>
                                                <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
			                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
				                                         alt='thumb'
				                                         class='img-fluid' />
		                                        </a>
		                                    </div>
		                                    <div class='badge-positioned w-90'>
			                                    <div class='d-flex justify-content-between align-items-center'>
				                                    <span class='badge badge-danger font-weight-bold'>{PostHelper.GetCategoryName(item.PostCategory)}</span>
				                                    <div class='video-icon'>
					                                    <i class='{post_icon}'></i>
				                                    </div>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get latest videos and gallery 
        public static HtmlString GetLatestVideoGalleriesHome(int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.Where(s => s.PostType == "Gallery" || s.PostType == "Video").OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='d-flex justify-content-between align-items-center border-bottom pb-2'>
	                                        <div class='div-w-80 mr-3'>
		                                        <div class='rotate-img'>
			                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
				                                         alt='thumb'
				                                         class='img-fluid' />
		                                        </div>
	                                        </div>
	                                        <h3 class='font-weight-600 mb-0'>
		                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
			                                        {TextHelper.FormatLongText(item.PostTitle, 25)}
		                                        </a>
	                                        </h3>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        /// Get latest news in single post view
        public static HtmlString GetLatestNewsPosts(int total)
        {
            string result = "<span class='text-danger'></span>";

            try
            {
                using (var db = new DBConnection())
                {
                    if (db.vwPostsApproved.Any())
                    {
                        result = "";
                        var DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='row'>
	                                        <div class='col-sm-12'>
		                                        <div class='border-bottom pb-4 pt-4'>
			                                        <div class='row'>
				                                        <div class='col-sm-8'>
					                                        <h5 class='font-weight-600 mb-1'>
						                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
							                                        {TextHelper.FormatLongText(item.PostTitle, 50)}
						                                        </a>
					                                        </h5>
					                                        <p class='fs-13 text-muted mb-0'>
						                                        <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
					                                        </p>
				                                        </div>
				                                        <div class='col-sm-4'>
					                                        <div class='rotate-img'>
						                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
							                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
								                                         alt='banner'
								                                         class='img-fluid' />
						                                        </a>
					                                        </div>
				                                        </div>
			                                        </div>
		                                        </div>
	                                        </div>
                                        </div>";
                        }

                        return new HtmlString(result);
                    }

                }
            }
            catch (FormatException)
            {
                //TODO log error
            }

            return new HtmlString(result);
        }


        //get trending this week other
        public static HtmlString GetTrendingNewsPosts(int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string PostContent = (!string.IsNullOrEmpty(PostHelper.GetPostData(item.PostID, "PostContent"))) ? TextHelper.FormatLongText(TextHelper.StripHTML(PostHelper.GetPostData(item.PostID, "PostContent")), 100) : "";

                            //string PostType = "EntertainmentAudioPost";

                            result += @$"<div class='mb-4'>
	                                        <div class='rotate-img'>
		                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-dark text-decoration-none'>		
			                                        <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
			                                         alt='banner'
			                                         class='img-fluid' />
		                                        </a>
	                                        </div>
	                                        <h3 class='mt-3 font-weight-600'>
		                                        <a href='/Posts/{PostHelper.GetPostData(item.PostID, "PostPermalink")}' class='text-dark text-decoration-none'>
			                                        {TextHelper.FormatLongText(PostHelper.GetPostData(item.PostID, "PostTitle"), 50)}
		                                        </a>
	                                        </h3>
	                                        <p class='fs-13 text-muted mb-0'>
		                                        <span class='mr-2'>{GetTimeSince(Convert.ToDateTime(PostHelper.GetPostData(item.PostID, "ApprovalsDateAdded")))} ago</span> 
	                                        </p>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get related posts under post view
        public static HtmlString GetRelatedPosts(string post_permalink, int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    string PostCategory = db.vwPostsApproved.Where(s => s.PostPermalink == post_permalink).FirstOrDefault().PostCategory;
                    string PostTags = db.vwPostsApproved.Where(s => s.PostPermalink == post_permalink).FirstOrDefault().PostTags;
                    var DBQuery = db.vwPostsApproved.Where(s => (s.PostCategory == PostCategory || s.PostTags.Contains(PostTags)) && s.PostPermalink != post_permalink).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-sm-6'>
	                                        <div class='post-author'>
		                                        <div class='rotate-img'>
		                                            <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
			                                            <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
				                                             alt='banner'
				                                             class='img-fluid' />
		                                            </a>
		                                        </div>
		                                        <div class='post-author-content'>
			                                        <h5 class='mb-1'>
				                                        <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
					                                        {TextHelper.FormatLongText(item.PostTitle, 50)}
				                                        </a>
			                                        </h5>
			                                        <p class='fs-13 text-muted mb-0'>
				                                        <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
			                                        </p>
		                                        </div>
	                                        </div>
                                        </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get more news home
        public static HtmlString GetMoreNewsHome(int skip, int total)
        {
            string result = "<span class='text-danger'></span>";
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Skip(skip).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='col-sm-6 mb-3'>
                                            <div class='border-sm-bottom pb-3'>
                                              <div class='rotate-img'>
							                  <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>		
									                <img src='/files/{PostHelper.GetPostImageLink(item.PostID)}'
									                 alt='thumb'
									                 class='img-fluid rounded' />
								                </a>
                                              </div>
                                              <p class='fs-16 font-weight-600 mb-0 mt-3'>
                                                <a href='/Posts/{item.PostPermalink}' class='text-dark text-decoration-none'>
									                {TextHelper.FormatLongText(item.PostTitle, 50)}
								                </a>
                                              </p>
                                              <p class='fs-13 text-muted mb-0'>
                                                <span class='mr-2'>{GetTimeSince(item.ApprovalsDateAdded)} ago</span> 
                                              </p>
                                            </div>
                                          </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        // get recent posts for footer
        public static HtmlString GetRecentPostsFooter(int total)
        {
            string result = "<span class='text-danger'></span>";
            int count = 1;
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            string div_class = (count == total) ? "" : "footer-border-bottom pb-2 pt-2";
                            result += $@"<div class='row'>
                                            <div class='col-sm-12'>
										<div class='{div_class}'>
											<div class='row'>
												<div class='col-3'>
													<a href = '/Posts/{item.PostPermalink}' class='text-white'>
														<img src = '/files/{PostHelper.GetPostImageLink(item.PostID)}'

                                                             alt='thumb'
															 class='img-fluid' />
													</a>
												</div>
												<div class='col-9'>
													<h5 class='font-weight-600'>
														<a href = '/Posts/{item.PostPermalink}' class='text-white'>
															{TextHelper.FormatLongText(item.PostTitle, 50)}
														</a>
													</h5>
												</div>
											</div>
										</div>
									</div>
								</div>";
                            count++;
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get footer category links
        public static HtmlString GetFooterCategories(int total)
        {
            string result = "<span class='text-danger d-none'>No categories available</span>";
            int count = 1;
            try
            {
                using (var db = new DBConnection())
                {
                    var DBQuery = db.Categories.Where(s => s.IsPublished == 1 && s.IsHeader == 1).OrderByDescending(x => Guid.NewGuid()).Take(total);
                    if (DBQuery.Any())
                    {
                        result = "";
                        string div_class = (count == total) ? "pt-2" : "footer-border-bottom pb-2 pt-2";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='{div_class}'>
	                                        <div class='d-flex justify-content-between align-items-center'>
		                                        <h5 class='mb-0 font-weight-600'>
			                                        <a class='text-decoration-none text-white' href='/Category/{item.ShortCategoryName}'>{item.CategoryName}</a>
		                                        </h5>
		                                        <div class='count'>{PostHelper.GetTotalCategoryPosts(item.CategoryID)}</div>
	                                        </div>
                                        </div>";
                            count++;
                        }

                        //add sitemap link
                        result += $@"<div class='pt-2 mt-2'>
	                                    <div class='d-flex justify-content-between align-items-center'>
	                                      <h5 class='mb-0 font-weight-600'>
		                                    <a href='/Sitemap' target='_blank' class='text-light'>
			                                    Site Map
		                                    </a>
	                                      </h5>
	                                    </div>
                                    </div>";
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get breaking news
        public static HtmlString GetBreakingNews()
        {
            string result = @"<div class='d-flex align-items-center'>
                                 <p class='mb-0'>
                                    <span class='text-danger d-none'>No breaking news available</span>
                                 </p>
                              </div>";
            try
            {
                using (var db = new DBConnection())
                {
                    //get last breaking news

                    DateTime BreakingTime = DateTime.Now.AddHours(-6);
                    var DBQuery = db.vwPostsApproved.Where(s => s.IsBreakingNews == 1 && s.ApprovalsDateAdded >= BreakingTime).OrderByDescending(s=> s.ApprovalsDateAdded).Take(1);
                    if (DBQuery.Any())
                    {
                        result = "";
                        foreach (var item in DBQuery)
                        {
                            result += @$"<div class='d-flex align-items-center'>
	                                    <span class='badge badge-danger mr-3'>Breaking news</span>
	                                    <p class='mb-0'>
		                                    <a href='/Posts/{item.PostPermalink}' class='text-decoration-none breaking-news-text text-dark'>
			                                    <span class='breaking-news-text'>{TextHelper.FormatLongText(item.PostTitle, 75)}</span>
		                                    </a>
	                                    </p>
                                    </div>";
                        }
                    }
                    return new HtmlString(result);
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
            }
            return new HtmlString(result);
        }


        //get post gallery
        public static HtmlString GetPostGallery(string post_id)
        {
            string result = "";
            using(var db = new DBConnection())
            {
                if(db.GalleryImages.Any(s=> s.PostID == post_id))
                {
                    result = @$"<div class='container'>
	                                    <div class='row'>
		                                    <div id='ImageGallery' class='carousel slide' data-ride='carousel'>
			                                    <ul class='carousel-indicators'>
				                                    {GetPostGalleryLinks(post_id)}
			                                    </ul>
			                                    <div class='carousel-inner'>
				                                    {GetPostGalleryImageLinks(post_id)}
			                                    </div>
			                                    <a class='carousel-control-prev' href='#ImageGallery' data-slide='prev'>
				                                    <span class='carousel-control-prev-icon'></span>
			                                    </a>
			                                    <a class='carousel-control-next' href='#ImageGallery' data-slide='next'>
				                                    <span class='carousel-control-next-icon'></span>
			                                    </a>
		                                    </div>
	                                    </div>
                                    </div>";
                }
            }
            return new HtmlString(result);
        }

        //get gallery links
        public static HtmlString GetPostGalleryLinks(string post_id)
        {
            string result = "";
            using (var db = new DBConnection())
            {
                if (db.GalleryImages.Any(s => s.PostID == post_id))
                {
                    var DBQuery = db.GalleryImages.Where(s => s.PostID == post_id);
                    int count = 0;
                    foreach(var item in DBQuery)
                    {
                        string list_class = (count == 0) ? "active" : "";
                        result += @$"<li data-target='#ImageGallery' data-slide-to='{count}' class='{list_class}'></li>";
                        count++;
                    }
                }
            }
            return new HtmlString(result);
        }


        //get gallery image links
        public static HtmlString GetPostGalleryImageLinks(string post_id)
        {
            string result = "";
            using (var db = new DBConnection())
            {
                if (db.GalleryImages.Any(s => s.PostID == post_id))
                {
                    var DBQuery = db.GalleryImages.Where(s => s.PostID == post_id);
                    int count = 0;
                    foreach (var item in DBQuery)
                    {
                        string list_class = (count == 0) ? "active" : "";
                        result += @$"<div class='carousel-item {list_class}'>
					                    <img src='/files/{PostHelper.GetPostGalleryImageLink(item.ID, item.PostID)}' alt='{item.ImageCaption}' class='img-fluid w-100'>
					                    <div class='carousel-caption'>
						                    <h4 class='text-bordered'>{item.ImageCaption}</h4>
					                    </div>
				                    </div>";
                        count++;
                    }
                }
            }
            return new HtmlString(result);
        }


        //get post video tags
        public static HtmlString GetPostVideos(string post_id)
        {
            string result = "";
            //get post video folder name
            var DirectoryName = PostHelper.GetPostImageDirectory(post_id);
            using(var db = new DBConnection())
            {
                if (db.VideoUploads.Any(s => s.PostID == post_id))
                {
                    var DBQuery = db.VideoUploads.Where(s => s.PostID == post_id);
                    foreach (var item in DBQuery)
                    {
                        string link = (item.VideoLink.Contains("http")) ? item.VideoLink : "/files/videos/" + DirectoryName + "/" + item.VideoLink + "";
                        //if link is video link like youtube/vimeo
                        if (item.VideoLink.Contains("http"))
                        {
                            result += @$"<div class='col-12 mb-1'>
                                            <div class='embed-responsive embed-responsive-16by9'>
	                                            <iframe class='w-100' src='{link}' frameborder='0' allowfullscreen></iframe>
                                            </div>
                                          </div>";
                        }
                        else
                        {
                            result += @$"<div class='col-12 mb-1'>
                                            <div class='embed-responsive embed-responsive-16by9'>
	                                            <video class='w-100' controls>
		                                            <source src='{link}' type='video/mp4'>
	                                            </video>
                                            </div>
                                         </div>";
                        }
                    }
                }
            }

            return new HtmlString(result);
        }


        //get post video tag
        public static HtmlString DisplayPostVideo(string post_id, string video_link)
        {
            string result = "";
            //get post video folder name
            var DirectoryName =PostHelper.GetPostImageDirectory(post_id);
            result = @$"<div class='embed-responsive embed-responsive-16by9'>
	                        <video width='320' height='240' controls>
		                        <source src='/files/videos/{DirectoryName}/{video_link}' type='video/mp4'>
	                        </video>
                        </div>";
            
            return new HtmlString(result);
        }

        //get post video link tag
        public static HtmlString DisplayPostVideoLink(string post_id, string video_link)
        {
            string result = "";
            result = @$"<div class='col-12 mb-1'>
                            <div class='embed-responsive embed-responsive-16by9'>
	                            <iframe class='w-100' src='{video_link}' frameborder='0' allowfullscreen></iframe>
                            </div>
                        </div>";
            
            return new HtmlString(result);
        }


        // return how much time passed since date object
        public static string GetTimeSince(DateTime? objDateTime)
        {
            DateTime newDateObject = (DateTime)((objDateTime == null) ? DateTime.Now : objDateTime);
            // here we are going to subtract the passed in DateTime from the current time converted to UTC
            TimeSpan ts = DateTime.Now.ToUniversalTime().Subtract(newDateObject);
            int intDays = ts.Days;
            int intHours = ts.Hours;
            int intMinutes = ts.Minutes;
            int intSeconds = ts.Seconds;

            if (intDays > 0)
                return string.Format("{0} days", intDays);

            if (intHours > 0)
                return string.Format("{0} hours", intHours);

            if (intMinutes > 0)
                return string.Format("{0} minutes", intMinutes);

            if (intSeconds > 0)
                return string.Format("{0} seconds", intSeconds);

            // let's handle future times..just in case
            if (intDays < 0)
                return string.Format("{0} days", Math.Abs(intDays));

            if (intHours < 0)
                return string.Format("{0} hours", Math.Abs(intHours));

            if (intMinutes < 0)
                return string.Format("{0} minutes", Math.Abs(intMinutes));

            if (intSeconds < 0)
                return string.Format("{0} seconds", Math.Abs(intSeconds));

            return "just now";
        }





    }



}
