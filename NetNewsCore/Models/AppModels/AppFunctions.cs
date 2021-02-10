using LazZiya.ImageResize;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using NetNews.Models.AccessDataModels;
using NetNews.Models.AccountsDataModel;
using NetNews.Models.AppDataModels;
using NetNews.Models.Email;
using NetNews.Models.PostsDataModel;
using NetNews.Models.SubscriptionsDataModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NetNews.Models.AppModels
{
    public class AppFunctions
    {
        //Generate unique id
        /// <summary>
        /// Generate unique directory name for new account created, takes in user email
        /// </summary>
        /// <returns>unique directory name</returns>
        public string GenerateDirectoryName(string user_email)
        {
            if (user_email.Contains("@"))
            {
                user_email = user_email.Split('@')[0];
            }
            return (user_email + RandomString(8)).ToLower();
        }

        //Converts string to integer
        /// <summary>
        /// Converts string to integer, returns zero if fails
        /// </summary>
        /// <returns>integer</returns>
        public int Int32Parse(string string_number)
        {
            try
            {
                return Int32.Parse(string_number); ;
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        //Converts string to integer
        /// <summary>
        /// Converts string to integer, returns zero if fails
        /// </summary>
        /// <returns>integer</returns>
        public int Int32Parse(string string_number, int return_default)
        {
            try
            {
                return Int32.Parse(string_number); ;
            }
            catch (FormatException)
            {
                return return_default;
            }
        }

        //Generates unique alphanumeric strings
        /// <summary>
        /// Generate unique alphanumeric strings
        /// </summary>
        /// <returns>unique string</returns>
        public string GetUinqueId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
            return FormNumber.ToString("X");

        }

        //Generates GUID
        /// <summary>
        /// Generating GUID using the Guid.NewGuid() method
        /// </summary>
        /// <returns>GUID as string</returns>
        public string GetGuid()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }

        //Generates random alphanumeric strings
        /// <summary>
        /// Take in the length, and type of random text then generates random string. Default is alphanumeric string.
        /// </summary>
        /// <returns>alphanumeric string</returns>
        private static Random Rand = new Random();
        public string RandomString(int length, string format)
        {
             string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            if (!string.IsNullOrEmpty(format))
            {
                switch (format)
                {
                    case "Text":
                        chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                        break;
                    case "Numbers":
                        chars = "0123456789";
                        break;
                    default:
                        chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        break;
                }
            }
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Rand.Next(s.Length)]).ToArray());
        }

        //Generates random alphanumeric strings
        /// <summary>
        /// Take in the length and generates random alphanumeric string
        /// </summary>
        /// <returns>alphanumeric string</returns>
        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Password match check
        /// <summary>
        /// Checks if the passwords passed are the same
        /// </summary>
        /// <returns>boolean</returns>
        public bool PasswordsMatch(string password_one, string password_two)
        {
            if (password_one.Equals(password_two))
            {
                return true;
            }
            return false;
        }


        //Generate unique post permalink
        /// <summary>
        /// Generate unique permalink for new post created, takes in post title text
        /// </summary>
        /// <returns>unique permalink</returns>
        public string GeneratePostPermalink(string post_title)
        {
            //First to lower case
            post_title = post_title.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(post_title);
            post_title = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            post_title = Regex.Replace(post_title, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            post_title = Regex.Replace(post_title, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            post_title = post_title.Trim('-', '_');

            //Replace double occurences of - or _
            post_title = Regex.Replace(post_title, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            //check if permalink too long
            if(post_title.Length > 50)
            {
                post_title = post_title.Substring(0, 40) + GetUinqueId().ToLower();
            }

            //check if post permalink exists
            using (var db = new DBConnection())
            {
                if(db.Posts.Any(s=> s.PostPermalink == post_title))
                {
                    //add randon text to permalink
                    post_title = (post_title.Replace(' ', '-') + "-" + GetUinqueId()).ToLower();
                }
            }

            return post_title;
        }




        //Log post view to database
        /// <summary>
        /// Log post view to database. Checks if viewd alreadly before adding
        /// </summary>
        /// <returns>boolean</returns>
        public bool LogPostView(string post_id, string post_author, string post_type, string visitor_ip, string visitor_browser, string visitor_device, string other)
        {

            
            using (var db = new DBConnection())
            {
                var DBQuery = db.PostViews.Where(s=> s.PostID == post_id && s.IpAddress == visitor_ip && s.Browser == visitor_browser && s.Device == visitor_device);
               
                //Disable reload check
                /*
                if (DBQuery.Any())
                {
                    DateTime PreviousVisitTime = Convert.ToDateTime(DBQuery.OrderByDescending(s=> s.VisitDate).FirstOrDefault().VisitDate);
                    DateTime CurrentdateTime = DateTime.Now;
                    TimeSpan Difference = CurrentdateTime - PreviousVisitTime;
                    double Hours = Difference.TotalHours;

                    if (Hours < 1.00)
                    {
                        return false;
                    }
                }
                */

                    // Create post object.
                    PostViewsModel post = new PostViewsModel
                    {
                        PostID = post_id,
                        PostAuthor = post_author,
                        PostType = post_type,
                        IpAddress = visitor_ip,
                        Country = FormatCountryName(GetIpInfo(visitor_ip, "Country")),
                        Browser = visitor_browser,
                        Device = visitor_device,
                        VisitDate = DateTime.Now,
                        OtherDetails = other
                    };

                    // Add the new object to the db collection.
                    db.PostViews.Add(post);

                    // Submit the change to the database.
                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //TODO Log error
                        Console.WriteLine(ex);
                    }
                return false;
            }
        }


        //Get current visitor ip address
        /// <summary>
        /// Get current user ip address.
        /// </summary>
        /// <returns>The IP Address</returns>
        public string FormatVisitorIP(string ip_address)
        {
            // Get the IP  
            if (ip_address == "::1")
            {
                ip_address = "127.0.0.1";
            }
            return ip_address;
        }

        //Get current visitor ip address
        /// <summary>
        /// Get current user ip address.
        /// </summary>
        /// <returns>The IP Address</returns>
        public string FormatVisitorIP(string ip_address, string optional_ip)
        {
            if (string.IsNullOrEmpty(ip_address))
            {
                ip_address = optional_ip;
            }

            // Get the IP  
            if (ip_address == "::1")
            {
                ip_address = "127.0.0.1";
            }
            return ip_address;
        }


        //Get country info
        /// <summary>
        /// Get current visitor country info based on ip address
        /// </summary>
        /// <returns>The the info for the second parameter passed</returns>
        public string GetIpInfo(string ip_address, string return_data)
        {
            try
            {
                string url = "https://api.ipgeolocationapi.com/geolocate/" + ip_address;
                var request = WebRequest.Create(url);

                using (WebResponse wrs = request.GetResponse())
                using (Stream stream = wrs.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var obj = JObject.Parse(json);
                    string continent = (string)obj["continent"];
                    string city = (string)obj["name"];
                    string country = (string)obj["name"];
                    string country_code = (string)obj["alpha2"];
                    string calling_code = (string)obj["country_code"];

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
                        case "CallCode":
                            return calling_code;
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
                return null;
            }
        }


        //Get List Of Categories
        /// <summary>
        /// Get the list of all categories
        /// </summary>
        /// <returns>list of categories</returns>
        public List<CategoriesModel> GetCategoryList()
        {
            using (var db = new DBConnection())
            {
                List<CategoriesModel> category_list = new List<CategoriesModel>();

                //-- Get data from db --//
                category_list = db.Categories.Where(s=> s.IsPublished == 1).OrderBy(s=> s.CategoryName).ToList();

                return category_list;
            }
        }

        //Get List Of Tags
        /// <summary>
        /// Get the list of all tags
        /// </summary>
        /// <returns>list of tags</returns>
        public List<TagsModel> GetTagsList()
        {
            using (var db = new DBConnection())
            {
                List<TagsModel> tags_list = new List<TagsModel>();

                //-- Get data from db --//
                tags_list = db.Tags.ToList();

                return tags_list;
            }
        }


        //Get List Of Countries
        /// <summary>
        /// Get the list of all countries
        /// </summary>
        /// <returns>list of countries</returns>
        public List<CountryModel> GetCountryList() 
        {
            using (var db = new DBConnection())
            {
                List<CountryModel> country_list = new List<CountryModel>();

                //-- Get data from db --//
                country_list = db.Countries.OrderBy(s=> s.Name).ToList();

                return country_list;
            }
        }



        //Get List Of Editors
        /// <summary>
        /// Get the list of editors
        /// </summary>
        /// <returns>list of editors</returns>
        public List<string> GetEditorList(string current_account_id, string permission_name)
        {
            using (var db = new DBConnection())
            {
                List<AccountsModel> accounts_list = new List<AccountsModel>();

                //-- Get data from db --//
                accounts_list = db.Accounts.Where(s => s.Active == 1 && s.AccountID != current_account_id).ToList();

                // Create editoe list  
                List<string> EditorList = new List<string>();

                //Get Editor Permission ID
                int PermissionID = 0;
                if (db.Permissions.Any(s => s.PermissionName == permission_name))
                {
                    PermissionID = db.Permissions.Where(s => s.PermissionName == permission_name).FirstOrDefault().PermissionID;
                }

                foreach (var item in accounts_list)
                {
                    if (db.AccountToPermission.Any(s=> s.AccountID == item.AccountID && s.PermissionID == PermissionID))
                    {
                        // Add account to editor list  
                        EditorList.Add(item.AccountID);
                    }
                }

                return EditorList;
            }
        }


        //Add post to Post Approvals table
        /// <summary>
        /// Add post to post approval
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddPostApprovalData(string post_id, string post_type, int approval_state)
        {
            using (var db = new DBConnection())
            {
                // Create PostApprovals object.
                PostApprovalsModel post_data = new PostApprovalsModel
                {
                    PostID = post_id,
                    PostType = post_type,
                    ApprovalState = approval_state,
                    DateAdded = DateTime.Now
                };

                // Add object to the PostApprovals collection.
                db.PostApprovals.Add(post_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    //TODO log error
                    return false;
                }
            }
        }

       
        //Update post data using linq
        /// <summary>
        /// Update post to post approval
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdatePostdata(string post_id, string post_type, string post_permalink, string post_author, string post_category, string post_sub_category, string post_title, string post_extract,
                      string post_image, string image_caption, int? is_breaking_news, string post_content, string post_video_type, string post_video_link, string post_audio_type,
                      string post_audio_link, string post_tags, string post_editor, string updated_by)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from post in db.Posts
                    where post.PostID == post_id
                    select post;

                // Executing the query, and updating the column values
                foreach (PostsModel post_data in DBQuery)
                {
                    post_data.PostPermalink = post_permalink;
                    post_data.PostType = post_type;
                    post_data.PostAuthor = post_author;
                    post_data.PostCategory = post_category;
                    post_data.PostSubCategory = post_sub_category;
                    post_data.PostTitle = post_title;
                    post_data.PostExtract = post_extract;
                    post_data.PostImage = post_image;
                    post_data.ImageCaption = image_caption; ;
                    post_data.IsBreakingNews = is_breaking_news;
                    post_data.PostContent = post_content;
                    post_data.PostVideoType = post_video_type;
                    post_data.PostVideoLink = post_video_link;
                    post_data.PostAudioType = post_audio_type;
                    post_data.PostAudioLink = post_audio_link;
                    post_data.PostTags = post_tags;
                    post_data.PostEditor = post_editor;
                    post_data.UpdatedBy = updated_by;
                    post_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }

            }
            return false;
        }



        //Add post to Post Approvals table
        /// <summary>
        /// Add post to post approval
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdatePostApprovalData(string post_id, string post_type, int approval_state)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from post_approval in db.PostApprovals
                    where post_approval.PostID == post_id
                    select post_approval;

                // Executing the query, and updating the column values
                foreach (PostApprovalsModel approval_data in DBQuery)
                {
                    approval_data.PostID = post_id;
                    approval_data.PostType = post_type;
                    approval_data.ApprovalState = approval_state;
                    approval_data.DateAdded = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }

            }
            return false;
        }


        //Add comment for post 
        /// <summary>
        /// Add cooments from autor or reviewer to post
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddPostComments(string post_id, string commenter_id, string comment)
        {
            using (var db = new DBConnection())
            {
                // Create PostReviews object.
                PostReviewsModel post_review_data = new PostReviewsModel
                {
                    PostID = post_id,
                    ReviewerID = commenter_id,
                    ReviewComment = comment,
                    DateAdded = DateTime.Now
                };

                // Add object to the PostReviews collection.
                db.PostReviews.Add(post_review_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    //TODO log error
                    return false;
                }
            }
        }



        //Update Post Approvals table
        /// <summary>
        /// Update Post Approvals
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdatePostState(string post_id, string action_by, int approval_state)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from post_approval in db.PostApprovals
                    where post_approval.PostID == post_id
                    select post_approval;

                // Executing the query, and updating the column values
                foreach (PostApprovalsModel approval_data in DBQuery)
                {
                    approval_data.PostID = post_id;
                    approval_data.ApprovalState = approval_state;
                    approval_data.ApprovedBy = action_by;
                    approval_data.DateApproved = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }

            }
            return false;
        }



        //Delete Account 
        /// <summary>
        /// Delete User Account 
        /// </summary>
        /// <returns>boolean</returns>
        public bool RemovePost(string post_id, string connection_string)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    // Query the database for the rows to be deleted.
                    var deletePostDetails =
                        from details in db.Posts
                        where details.PostID == post_id
                        select details;

                    foreach (var detail in deletePostDetails)
                    {
                        db.Posts.Remove(detail);
                    }

                    db.SaveChanges();

                    //delete relational data
                    DeleteTableData("PostApprovals", "PostID", post_id, connection_string); //delete from post approvals
                    DeleteTableData("PostReviews", "PostID", post_id, connection_string); //delete from post reviews
                    DeleteTableData("GalleryImages", "PostID", post_id, connection_string); //delete from gallery images
                    DeleteTableData("VideoUploads", "PostID", post_id, connection_string); //delete from post videos

                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }


        //Delete table record
        /// <summary>
        /// Delete table record(s) base on the key passed
        /// </summary>
        /// <returns>boolean</returns>
        public bool DeleteTableData(string model_name, string pk_name, string pk_value, string connection_string)
        {
            var MsgCountQuery = @"DELETE FROM [" + model_name + "] WHERE [" + pk_name + "]  = @key";
            try
            {
                using (var con = new SqlConnection(connection_string))
                {
                    con.Open();
                    var cmd = new SqlCommand(MsgCountQuery, con);
                    cmd.Parameters.AddWithValue("@key", pk_value);
                    if (cmd.ExecuteScalar() != DBNull.Value)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //throw; TODO Log error
                return false;
            }
        }


        //Validate required inputs 
        /// <summary>
        /// Validates array of inputs for not null
        /// </summary>
        /// <returns>boolean</returns>
        public bool ValidateInputs(string[] inputs)
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




        //Convert Text Case
        /// <summary>
        /// Convert Text Case to the desired format passed as parameter
        /// </summary>
        /// <returns>string</returns>
        public string ConvertCase(string text, string convert_to)
        {
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



        //Format Long Text
        /// <summary>
        /// trims text to the nearest desired lenght passed in the parameter
        /// </summary>
        /// <returns>string</returns>
        public string FormatLongText(string text, int max_length)
        {
            if (text != null && text.Length > max_length)
            {
                int iNextSpace = text.LastIndexOf(" ", max_length, StringComparison.Ordinal);
                text = $"{(text.Substring(0, (iNextSpace > 0) ? iNextSpace : max_length).Trim())}...";
            }
            return text;
        }


        //Update Account Status
        /// <summary>
        /// Update Post Approvals
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateAccountStatus(string account_id, string action_by, int active_state)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from accounts in db.Accounts
                    where accounts.AccountID == account_id
                    select accounts;

                // Executing the query, and updating the column values
                foreach (AccountsModel account_data in DBQuery)
                {
                    account_data.Active = active_state;
                    account_data.UpdatedBy = action_by;
                    account_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }

            }
            return false;
        }


        //Delete Account 
        /// <summary>
        /// Delete User Account 
        /// </summary>
        /// <returns>boolean</returns>
        public bool RemoveAccount(string account_id, string connection_string)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    // Query the database for the rows to be deleted.
                    var deleteAccountDetails =
                        from details in db.Accounts
                        where details.AccountID == account_id
                        select details;

                    foreach (var detail in deleteAccountDetails)
                    {
                        db.Accounts.Remove(detail);
                    }

                    //delete account details 
                    DeleteTableData("AccountDetails", "AccountID", account_id, connection_string);

                    //delete permissions  
                    DeleteTableData("AccountToPermission", "AccountID", account_id, connection_string);

                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }



        //Update Data Model Record
        /// <summary>
        /// Updates a column value of the data entity passed
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateTableData(string model_name, string pk_name, string pk_value, string update_column, string update_value, string connection_string)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    string DBQuery = $"Update [" + model_name + "] SET [" + update_column + "] = '" + update_value + "' Where [" + pk_name + "] = '" + pk_value + "' ";
                    using (SqlCommand command = new SqlCommand(DBQuery, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //TODO Provide for exceptions.
            }
            return false;
        }




        //Add record into table
        /// <summary>
        /// Adds new recored into entity table passed
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddTableData(string model_name, string entry_column, string entry_value, string connection_string)
        {

            try
            {
               using (SqlConnection connection = new SqlConnection(connection_string)) { 
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //Insert record to Users db
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"INSERT INTO [" + model_name + "] ([" + entry_column + "]) VALUES (@value)";
                        cmd.Parameters.AddWithValue("@value", ((object)entry_value) ?? DBNull.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                            if (connection != null)
                            {
                                //cleanup connection i.e close 
                                connection.Close();
                            }

                        if (rowsAffected == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //TODO Log Error
             }

            return false;
        }


        //Add forgot password data
        /// <summary>
        /// Add forgot password data to reset table
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddForgotPassword(string reset_id, string account_id)
        {
            using (var db = new DBConnection())
            {
                // Create PasswordForgot object.
                PasswordForgotModel forgot_password_data = new PasswordForgotModel
                {
                    ResetID = reset_id,
                    AccountID = account_id,
                    ResetDate = DateTime.Now
                };

                // Add object to the PasswordForgot collection.
                db.PasswordForgot.Add(forgot_password_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    //TODO log error
                    return false;
                }
            }
        }


        //Return account data
        /// <summary>
        /// Get specific account data. Takes account id and data to return
        /// </summary>
        /// <returns>account data</returns>
        public string GetAccountData(string account_id, string return_data)
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


        //Updates account permission
        /// <summary>
        /// Checks permission for user, if add updates, if empty, removes permission. 
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdatePermission(string permission_name, string permission_value, string account_id, string updated_by)
        {
            using(var db = new DBConnection())
            {
                try
                {
                    //get permission id
                    int PermissionID = 0;
                    if (db.Permissions.Any(s => s.PermissionName == permission_name))
                    {
                        PermissionID = db.Permissions.Where(s => s.PermissionName == permission_name).FirstOrDefault().PermissionID;
                    }

                    if (permission_value == "1")
                    {
                        //check if permission already exists for user
                        if (!db.AccountToPermission.Any(s => s.AccountID == account_id && s.PermissionID == PermissionID))
                        {
                            //if not add permission
                            AddPermission(account_id, PermissionID, updated_by);
                        }
                    }
                    else
                    {
                        //check if permission already exists for user
                        if (db.AccountToPermission.Any(s => s.AccountID == account_id && s.PermissionID == PermissionID))
                        {
                            //if so remove permission
                            RemovePermission(account_id, PermissionID);
                        }
                    }
                    return true;
                }
                catch (Exception)
                {
                    //TODO log error
                    return false;
                }
            }
            
        }


        //Add permission data
        /// <summary>
        /// Add forgot password data to reset table
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddPermission(string account_id, int permission_id, string updated_by)
        {
            using (var db = new DBConnection())
            {
                // Create AccountToPermission object.
                AccountToPermissionModel permission_data = new AccountToPermissionModel
                {
                    AccountID = account_id,
                    PermissionID = permission_id,
                    UpdatedBy = updated_by,
                    UpdateDate = DateTime.Now,
                    DateAdded = DateTime.Now
                };

                // Add object to the AccountToPermission collection.
                db.AccountToPermission.Add(permission_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    //TODO log error
                    Console.WriteLine(ex);

                    return false;
                }
            }
        }


        //Delete Permission
        /// <summary>
        /// Delete Permission for Account 
        /// </summary>
        /// <returns>boolean</returns>
        public bool RemovePermission(string account_id, int permission_id)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    // Query the database for the rows to be deleted.
                    var deletePermissionDetails =
                        from details in db.AccountToPermission
                        where details.AccountID == account_id && details.PermissionID == permission_id
                        select details;

                    foreach (var detail in deletePermissionDetails)
                    {
                        db.AccountToPermission.Remove(detail);
                    }

                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }


        /// <summary>
        /// Checks if a user has permission access
        /// </summary>
        public bool CheckUserAccess(string account_id, string permission_name)
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

        //Update Category
        /// <summary>
        /// Update Category Data
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateCategories(string category_id, string category_name, string category_short_name, string category_parent, string category_description, string category_icon, int category_order, int is_published, int is_header, string updated_by)
        {
            using (var db = new DBConnection())
            {


                // Query the database for the row to be updated.
                var DBQuery =
                    from categories in db.Categories
                    where categories.CategoryID == category_id
                    select categories;

                // Execute the query, and change the column values
                foreach (CategoriesModel category_data in DBQuery) 
                {
                    category_data.CategoryName = category_name;
                    category_data.ShortCategoryName = category_short_name;
                    category_data.CategoryParent = category_parent;
                    category_data.CategoryDescription = category_description;
                    category_data.CategoryIcon = category_icon;
                    category_data.CategoryOrder = category_order;
                    category_data.IsPublished = is_published;
                    category_data.IsHeader = is_header;
                    category_data.UpdatedBy = updated_by;
                    category_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();

                    //update others by one
                    UpdateOtherCategoriesOrder(category_id, category_order);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }


        //Update Other Category by one
        /// <summary>
        /// Update Other Category greater than updated category by one
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateOtherCategoriesOrder(string category_id, int category_order)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from categories in db.Categories
                    where categories.CategoryID != category_id && categories.CategoryOrder >= category_order

                    select categories;

                // Execute the query, and change the column values
                foreach (CategoriesModel category_data in DBQuery)
                {
                    category_data.CategoryOrder = category_data.CategoryOrder + 1;
                    category_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }




        //Update Other Lists by one
        /// <summary>
        /// Update Other Lists greater than updated list by one
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateOtherListOrder(string list_id, string list_type, int? list_order)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from lists in db.TopTenList
                    where lists.ListID != list_id && lists.ListType != list_type && lists.ListOrder >= list_order

                    select lists;

                // Execute the query, and change the column values
                foreach (TopTenListModel clist_data in DBQuery)
                {
                    clist_data.ListOrder = clist_data.ListOrder + 1;
                    clist_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }



        //Substract Other Category by one
        /// <summary>
        /// Substract Other Category greater than updated category by one
        /// </summary>
        /// <returns>boolean</returns>
        public bool SubstractOtherCategoriesOrder(int category_order)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from categories in db.Categories
                    where categories.CategoryOrder > category_order

                    select categories;

                // Execute the query, and change the column values
                foreach (CategoriesModel category_data in DBQuery)
                {
                    category_data.CategoryOrder = category_data.CategoryOrder - 1;
                    category_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }


        //Delete Category 
        /// <summary>
        /// Delete Tag
        /// </summary>
        /// <returns>boolean</returns>
        public bool DeleteCategory(string category_id)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    int category_order = db.Categories.Where(s => s.CategoryID == category_id).FirstOrDefault().CategoryOrder;

                    // Query the database for the rows to be deleted.
                    var deleteCategoryDetails =
                        from details in db.Categories
                        where details.CategoryID == category_id
                        select details;

                    foreach (var detail in deleteCategoryDetails)
                    {
                        db.Categories.Remove(detail);
                    }

                    db.SaveChanges();

                    //update other categories above deleted category
                    SubstractOtherCategoriesOrder(category_order);

                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }



        //Update Tag
        /// <summary>
        /// Update Tag Data
        /// </summary>
        /// <returns>boolean</returns>
        public bool UpdateTags(string tag_id, string tag_name, string tag_description, string updated_by)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the row to be updated.
                var DBQuery =
                    from tags in db.Tags
                    where tags.TagID == tag_id
                    select tags;

                // Execute the query, and change the column values
                foreach (TagsModel tag_data in DBQuery)
                {
                    tag_data.TagName = tag_name;
                    tag_data.TagDescription = tag_description;
                    tag_data.UpdatedBy = updated_by;
                    tag_data.UpdateDate = DateTime.Now;
                }

                // Submit the changes to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }



        //Delete Tag 
        /// <summary>
        /// Delete Tag
        /// </summary>
        /// <returns>boolean</returns>
        public bool DeleteTag(string tag_id)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    // Query the database for the rows to be deleted.
                    var deleteTagDetails =
                        from details in db.Tags
                        where details.TagID == tag_id
                        select details;

                    foreach (var detail in deleteTagDetails)
                    {
                        db.Tags.Remove(detail);
                    }

                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO Provide for exceptions.
                }
            }
            return false;
        }


        //Add gallery image
        /// <summary>
        /// Add gallery image with optional caption
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddGalleryImage(string post_id, string image_link, string image_caption, string updated_by)
        {
            using (var db = new DBConnection())
            {
                // Create GalleryImages object.
                GalleryImagesModel images_data = new GalleryImagesModel
                {
                    PostID = post_id,
                    ImageLink = image_link,
                    ImageCaption = image_caption,
                    UpdatedBy = updated_by,
                    UpdateDate = DateTime.Now,
                    DateAdded = DateTime.Now
                };

                // Add object to the GalleryImages collection.
                db.GalleryImages.Add(images_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    //TODO log error
                    Console.WriteLine(ex);

                    return false;
                }
            }
        }



        /// Upload media video
        /// <summary>
        /// Upload video
        /// </summary>
        /// <returns>uploaded video filename</returns>
        public string UploadMediaFile(List<IFormFile> MediaFile)
        {
            try
            {
                long size = MediaFile.Sum(f => f.Length);

                var filePaths = new List<string>();
                foreach (var formFile in MediaFile)
                {
                    if (formFile.Length > 0)
                    {

                        //file infp
                        var Name = formFile.Name;
                        var FileName = RandomString(8) + "-" + formFile.FileName; //random file name
                        var FileContentType = formFile.ContentType;
                        var FileLength = formFile.Length;
                        string FolderName = "videos";
                        if (FileContentType.Contains("audio"))
                        {
                            FolderName = "audios";
                        }

                        //Saving file with resize, text and image watermark
                        var DirectoryName = DateTime.Now.ToString("MM-yyyy");
                        var SavePath = @"wwwroot\\files\\"+ FolderName + "\\" + DirectoryName + "\\";

                        try
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        catch (Exception ex)
                        {
                            //TODO handle them here
                            Console.WriteLine(ex);
                        }

                        // full path to file in temp location
                        var filePath = SavePath;
                        filePaths.Add(filePath);
                        using (var stream = new FileStream(Path.Combine(SavePath, FileName), FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                            return FileName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
                return null;
            }
            return null;
        }


        /// Upload media videos
        /// <summary>
        /// Upload videos and save video link in VideoUploads
        /// </summary>
        /// <returns>boolean</returns>
        public bool UploadVideos(string post_id, List<IFormFile> MediaFile,  string updated_by)
        {
            try
            {
                long size = MediaFile.Sum(f => f.Length);

                var filePaths = new List<string>();
                foreach (var formFile in MediaFile)
                {
                    if (formFile.Length > 0)
                    {
                        //Saving file with resize, text and image watermark
                        var DirectoryName = DateTime.Now.ToString("MM-yyyy");
                        var SavePath = @"wwwroot\\files\\videos\\" + DirectoryName + "\\";

                        try
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        catch (Exception ex)
                        {
                            //TODO handle them here
                            Console.WriteLine(ex);
                        }

                        //file infp
                        var Name = formFile.Name;
                        var FileName = RandomString(8) + "-" + formFile.FileName; //random file name
                        var FileContentType = formFile.ContentType;
                        var FileLength = formFile.Length;


                        // full path to file in temp location
                        var filePath = SavePath;
                        filePaths.Add(filePath);
                        using (var stream = new FileStream(Path.Combine(SavePath, FileName), FileMode.Create))
                        {
                            formFile.CopyTo(stream);

                            //Add video link to VideoUploads
                            AddVideoLink(post_id, FileName, null, updated_by);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
                return false;
            }
        }

        /// Upload video links
        /// <summary>
        /// Upload video links and save link in VideoUploads
        /// </summary>
        /// <returns>boolean</returns>
        public bool UploadVideoLinks(string post_id, string video_links, string updated_by) 
        {
            try
            {
                string[] VideoLinks = video_links.Split(",");
                // Loop over strings.
                foreach (string link in VideoLinks)
                {
                    //Add video link to VideoUploads
                    AddVideoLink(post_id, link,null, updated_by);
                }
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
                return false;
            }
        }



        //Add video link of post 
        /// <summary>
        /// Adds video link of the post to VideoUploads
        /// </summary>
        /// <returns>boolean</returns>
        public bool AddVideoLink(string post_id, string video_link, string video_caption, string updated_by)
        {
            using (var db = new DBConnection())
            {
                // Create UploadVideos object.
                VideoUploadsModel video_uploads_data = new VideoUploadsModel
                {
                    PostID = post_id,
                    VideoLink = video_link,
                    VideoCaption = video_caption,
                    UpdatedBy = updated_by,
                    UpdateDate = DateTime.Now,
                    DateAdded = DateTime.Now
                };

                // Add object to the PostReviews collection.
                db.VideoUploads.Add(video_uploads_data);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //TODO log error
                    return false;
                }
            }
        }


        /// Upload post image
        /// <summary>
        /// Upload post image
        /// </summary>
        /// <returns>post image link</returns>
        public string UploadImage(List<IFormFile> ImageFile, string text_waterMark, string image_watermark, int image_height, int image_width)
        {
            //Set post image to empty. Assigned upon upload
            string PostImage = "";

            //Saving file with resize, text and image watermark
            var DirectoryName = DateTime.Now.ToString("MM-yyyy");

            var SavePath = @"wwwroot\\files\\images\\" + DirectoryName + "\\";

            //create directory if not exist
            Directory.CreateDirectory(SavePath);

            //Set counter for first image
            int ImageCounter = 0;
            foreach (var file in ImageFile)
            {
                if (file.Length > 0 && !file.FileName.Contains("mp4"))
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            string NewFileName = RandomString(8) + "-" + file.FileName;
                            try
                            {
                                img.ScaleAndCrop(image_width, image_height)
                                .AddImageWatermark(@"wwwroot\files\images\defaults\" + image_watermark)
                                .AddTextWatermark(text_waterMark)
                                .SaveAs(SavePath + "\\" + NewFileName);
                            }
                            catch (Exception ex)
                            {
                                //TODO log error
                                Console.WriteLine(ex);
                            }

                            if(ImageCounter == 0)
                            {
                                //Set post image
                                PostImage = NewFileName;
                            }
                            ImageCounter++;
                        }
                    }
                }
            }

            return PostImage;
        }


        /// Upload gallery images
        /// <summary>
        /// Upload post images
        /// </summary>
        /// <returns>post image link</returns>
        public bool UploadGalleryImages(string account_id, string post_id, List<IFormFile> ImageFiles, string text_watermark, string image_watermark, int image_height, int image_width, string image_captions)
        {
            try
            {
                //Saving file with resize, text and image watermark
                var DirectoryName = DateTime.Now.ToString("MM-yyyy");
                var SavePath = @"wwwroot\\files\\images\\" + DirectoryName + "\\";
                //create directory if not exist
                Directory.CreateDirectory(SavePath);

                int TotalCaptions = image_captions.Split(",").Length;
                int ImgCount = 0;
                int ImageListCount = 0; //keeps track for 1st image in file to upload as headline image

                foreach (var file in ImageFiles)
                {
                    //get image caption value 
                    string ImageCaption = null;
                    if (ImgCount >= 0 && ImgCount < TotalCaptions && ImageListCount != 0)
                    {
                        //if text == "#caption", it's default caption value. Set to null
                        ImageCaption = (image_captions.Split(",")[ImgCount] == "#caption") ? null : image_captions.Split(",")[ImgCount];
                        ImgCount++;
                    }

                    if (file.Length > 0)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            using (var img = Image.FromStream(stream))
                            {
                                string NewFileName = RandomString(8) + "-" + file.FileName;

                                try
                                {
                                    img.ScaleAndCrop(image_width, image_height)
                                    .AddImageWatermark(@"wwwroot\files\images\defaults\" + image_watermark)
                                    .AddTextWatermark(text_watermark)
                                    .SaveAs(SavePath + "\\" + NewFileName);
                                }
                                catch (Exception)
                                {
                                    //for possible LaZziya issue with resizing pngs
                                    try
                                    {
                                        img.AddTextWatermark(text_watermark)
                                        .SaveAs(SavePath + "\\" + NewFileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        //TODO log error
                                        Console.WriteLine(ex);
                                    }
                                }

                                //add to post gallery images
                                AddGalleryImage(post_id, NewFileName, ImageCaption, account_id);

                            }
                        }
                    }
                    ImageListCount++;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// Notify editors of posts
        /// <summary>
        /// Sends email notification to editors of new post
        /// </summary>
        /// <returns>boolean</returns>
        public bool EditorPostNotification(string editor_id, string post_link, string closure, string company, string unsubscribe_link, 
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                using (var db = new DBConnection())
                {
                    string ReviewerAccountID = editor_id;
                    string ReviewerEmail = GetAccountData(ReviewerAccountID, "Email");

                    //set email data
                    string ToName = GetAccountData(ReviewerAccountID, "FullName");
                    string[] MessageParagraphs = { "Hello " + ToName + ", ", "There is a new post awaiting reviewer action." };
                    string PreHeader = "New post notification.";
                    bool Button = true;
                    int ButtonPosition = 2;
                    string ButtonLink = post_link;
                    string ButtonLinkText = "View Post";
                    string Closure = closure;
                    string Company = company;
                    string UnsubscribeLink = unsubscribe_link;
                    string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                    string FromEmail = smtp_email;
                    string ToEmail = ReviewerEmail;
                    string Subject = "New Post";
                    EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                }

                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }


        /// Notify author of comment
        /// <summary>
        /// Sends email notification to author of post comment
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAutorOfApproval(string account_id, string post_link, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string ToName = GetAccountData(account_id, "FullName");
                string[] MessageParagraphs = { "Hello " + ToName + ", ", "A post you made has been approved." };
                string PreHeader = "Post approval notification.";
                bool Button = true;
                int ButtonPosition = 2;
                string ButtonLink = post_link;
                string ButtonLinkText = "View Post";
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string ToEmail = GetAccountData(account_id, "Email");
                string Subject = "Post Approval";
                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }


        /// Notify author of comment
        /// <summary>
        /// Sends email notification to author of post comment
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAutorOfComment(string account_id, string post_link, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string ToName = GetAccountData(account_id, "FullName");
                string[] MessageParagraphs = { "Hello " + ToName + ", ", "There is a comment on a post pending approval that requires your action." };
                string PreHeader = "Post review comment notification.";
                bool Button = true;
                int ButtonPosition = 1;
                string ButtonLink = post_link;
                string ButtonLinkText = "View Post";
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string ToEmail = GetAccountData(account_id, "Email");
                string Subject = "Post Comment";
                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }



        /// Notify reviewer of comment
        /// <summary>
        /// Sends email notification to reviewer of post
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyReviewersOfComment(string account_id, string post_link, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string ToName = GetAccountData(account_id, "FullName");
                string[] MessageParagraphs = { "Hello " + ToName + ", ", "There has been an action on a post you have reviwed." };
                string PreHeader = "Post update notification.";
                bool Button = true;
                int ButtonPosition = 1;
                string ButtonLink = post_link;
                string ButtonLinkText = "View Post";
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string ToEmail = GetAccountData(account_id, "Email");
                string Subject = "Edit Post";
                EmailService.SendEmail(FromEmail, ToEmail, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }

        /// Notify account of removal
        /// <summary>
        /// Sends email notification to account email of account removal
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAccountOfRemoval(string account_email, string to_name, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string[] MessageParagraphs = { "Hello " + to_name + ", ", "Your account has been removed. If you have further questions, please send us an email." };
                string PreHeader = "Account removal notification.";
                bool Button = false;
                int ButtonPosition = 0;
                string ButtonLink = null;
                string ButtonLinkText = null;
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string Subject = "Account Removal";
                EmailService.SendEmail(FromEmail, account_email, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }


        /// Notify account of rejection
        /// <summary>
        /// Sends email notification to account email of account rejection
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAccountOfRejection(string account_email, string to_name, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string[] MessageParagraphs = { "Hello " + to_name + ", ", "Your account has been rejected. If you have further questions, please send us an email." };
                string PreHeader = "Account rejection notification.";
                bool Button = false;
                int ButtonPosition = 0;
                string ButtonLink = null;
                string ButtonLinkText = null;
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string Subject = "Account Rejection";
                EmailService.SendEmail(FromEmail, account_email, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }


        /// Notify account of rejection
        /// <summary>
        /// Sends email notification to account email of account rejection
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAccountOfSuspension(string account_email, string to_name, string from_email, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string[] MessageParagraphs = { "Hello " + to_name + ", ", "Your account has been suspended. If you have further questions, please send us an email." };
                string PreHeader = "Account suspension notification.";
                bool Button = false;
                int ButtonPosition = 0;
                string ButtonLink = null;
                string ButtonLinkText = null;
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string Subject = "Account Suspension";
                EmailService.SendEmail(FromEmail, account_email, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }

        /// Notify account of approval
        /// <summary>
        /// Sends email notification to account email of account approval
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAccountOfApproval(string account_email, string to_name, string from_email, string button_link, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string[] MessageParagraphs = { "Hello " + to_name + ", ", "Your account has been approved." };
                string PreHeader = "Post approval notification.";
                bool Button = true;
                int ButtonPosition = 1;
                string ButtonLink = button_link;
                string ButtonLinkText = "SignIn Link";
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string Subject = "Account Approval";
                EmailService.SendEmail(FromEmail, account_email, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }

        /// Notify account of approval
        /// <summary>
        /// Sends email notification to account email of account approval
        /// </summary>
        /// <returns>boolean</returns>
        public bool NotifyAccountOfActivation(string account_email, string to_name, string from_email, string button_link, string closure, string company, string unsubscribe_link,
            string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            try
            {
                //set email data
                string[] MessageParagraphs = { "Hello " + to_name + ", ", "Your account status has been set to active." };
                string PreHeader = "Post approval notification.";
                bool Button = true;
                int ButtonPosition = 1;
                string ButtonLink = button_link;
                string ButtonLinkText = "SignIn Link";
                string Closure = closure;
                string Company = company;
                string UnsubscribeLink = unsubscribe_link;
                string MessageBody = EmailFormating.FormatEmail(MessageParagraphs, PreHeader, Button, ButtonPosition, ButtonLink, ButtonLinkText, Closure, Company, UnsubscribeLink);

                string FromEmail = from_email;
                string Subject = "Account Status Update";
                EmailService.SendEmail(FromEmail, account_email, Subject, MessageBody, smtp_email, smtp_pass, display_name, smtp_host, smtp_port);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);

                return false;
            }
        }

        /// Delete image in directory
        /// <summary>
        ///  Delete image in directory
        /// </summary>
        /// <returns>boolean</returns>
        /// 
        public bool DeletePostImageGalleries(string post_id)
        {
            using(var db = new DBConnection())
            {
                // Query the database for the rows to be deleted.
                var DBQuery = db.GalleryImages.Where(s => s.PostID == post_id);
                try
                {
                    foreach (var item in DBQuery)
                    {
                        var DirectoryName = GetPostImageDirectory(post_id);
                        var FilePath = @"wwwroot\\files\\images\\" + DirectoryName + "\\" + item.ImageLink;
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                        
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }      
            }
        }


        /// Delete video in directory
        /// <summary>
        ///  Delete video in directory
        /// </summary>
        /// <returns>boolean</returns>
        /// 
        public bool DeletePostVideo(string post_id)
        {
            using (var db = new DBConnection())
            {
                // Query the database for the rows to be deleted.
                var DBQuery = db.Posts.Where(s => s.PostID == post_id);
                try
                {
                    foreach (var item in DBQuery)
                    {
                        var DirectoryName = GetPostImageDirectory(post_id);
                        var FilePath = @"wwwroot\\files\\videos\\" + DirectoryName + "\\" + item.PostVideoLink;
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }

                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }



        /// Upload advert image
        /// <summary>
        /// Uploads advert image to directory
        /// </summary>
        /// <returns>boolean</returns>
        public string UploadAdvertMedia(List<IFormFile> ImageFile)
        {
            //Saving file with resize, text and image watermark
            var DirectoryName = DateTime.Now.ToString("MM-yyyy");
            var SavePath = @"wwwroot\\files\\images\\" + DirectoryName + "\\";
            //create directory if not exist
            Directory.CreateDirectory(SavePath);

            string fileName = "";
            foreach (var file in ImageFile)
            {
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            string NewFileName = RandomString(8) + "-" + file.FileName;
                            img.ScaleAndCrop(800, 600)
                                 .AddTextWatermark("lm-impact ad")
                                 .SaveAs($"{SavePath}{NewFileName}");
                            fileName = NewFileName;
                        }
                    }
                }
            }

            return fileName;
        }


        /// Get post image folder name
        /// <summary>
        /// Get post image folder name
        /// </summary>
        /// <returns>image directory name</returns>
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


        //Log site stat to database
        /// <summary>
        /// Log site stat to database.
        /// </summary>
        /// <returns>boolean</returns>
        public bool LogSiteStat(string stat_type, string action_value, string visitor_ip, string visitor_browser, string visitor_device, string other)
        {
            using (var db = new DBConnection())
            {

                // Create post object.
                SiteStatsModel stat = new SiteStatsModel
                {
                    StatType = stat_type,
                    ActionValue = action_value,
                    IpAddress = visitor_ip,
                    Country = FormatCountryName(GetIpInfo(visitor_ip, "Country")),
                    Browser = visitor_browser,
                    Device = visitor_device,
                    ActionDate = DateTime.Now,
                    OtherDetails = other
                };

                // Add the new object to the db collection.
                db.SiteStats.Add(stat);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    //TODO Log error
                    Console.WriteLine(ex);
                }
                return false;
            }
        }


        /// <summary>
        /// Logs activity
        /// </summary>
        public bool LogActivity(string activity_user, string action_by, string log_type, string action)
        {
            using (var db = new DBConnection())
            {
                ActivityLogsModel activity = new ActivityLogsModel
                {
                    ActivityUser = activity_user,
                    ActionBy = action_by,
                    LogType = log_type,
                    Action = action,
                    ActivityDate = DateTime.Now
                    // …
                };

                // Add the new object to the collection.
                db.ActivityLogs.Add(activity);

                // Submit the change to the database.
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return false;
        }


        /// <summary>
        /// Log site visits
        /// </summary>
        public bool VisitLog(string log_type, string page_title, string ip_address, string browser, string device, 
            string visit_time, string other_details)
        {
            using (var db = new DBConnection())
            {
                try
                {
                    //Disable reload check
                    /*
                    var DBQuery = db.VisitLogs.Where(s => s.LogType == log_type && s.PageTitle == page_title && s.IpAddress == ip_address && s.Browser == browser && s.Device == device);
                    if (DBQuery.Any())
                    {
                        DateTime PreviousVisitTime = Convert.ToDateTime(DBQuery.OrderByDescending(s => s.VisitDate).FirstOrDefault().VisitDate);
                        DateTime CurrentdateTime = DateTime.Now;
                        TimeSpan Difference = CurrentdateTime - PreviousVisitTime;
                        double Hours = Difference.TotalHours;

                        if (Hours < 1.00)
                        {
                            return false;
                        }
                    }
                    */

                    VisitLogsModel log = new VisitLogsModel
                    {
                        LogType = log_type,
                        PageTitle = page_title,
                        IpAddress = ip_address,
                        Country = GetIpInfo(ip_address, "Country"),
                        Browser = browser,
                        Device = device,
                        VisitTime = visit_time,
                        VisitDate = DateTime.Now,
                        OtherDetails = device
                    };

                    db.VisitLogs.Add(log);

                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //TODO log error
                        Console.WriteLine(ex);
                    }
                }
                catch (Exception ex)
                {
                    //TODO log error
                    Console.WriteLine(ex);
                }

            }
            return false;
        }

        /// <summary>
        /// Add new subscriber
        /// </summary>
        public bool AddSubscriber(string subscriber_email)
        {
            using (var db = new DBConnection())
            {
                if (!db.Subscribers.Any(s=> s.SubscriberEmail == subscriber_email))
                {
                    // Create a new Subscriber object.
                    SubscribersModel subscriber = new SubscribersModel
                    {
                        SubscriberID = GetGuid(),
                        SubscriberEmail = subscriber_email,
                        DateSubscribed = DateTime.Now
                    };

                    // Add the new object to the Orders collection.
                    db.Subscribers.Add(subscriber);

                    // Submit the change to the database.
                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// get category name using category id
        /// </summary>
        public string GetCategoryName(string category_id)
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

        /// <summary>
        /// get category id using category name
        /// </summary>
        public string GetCategoryID(string category_name)
        {
            category_name = ConvertCase(category_name, "TitleTrim");
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


        /// <summary>
        /// Sets proper country name. e.g Russia => Russian Federation
        /// </summary>
        public string FormatCountryName(string country)
        {
            using(var db = new DBConnection())
            {
                if (!string.IsNullOrEmpty(country))
                {
                    //if it exists return country name
                    var DBQuery = db.Countries.Where(s => s.NiceName == country);
                    if (!DBQuery.Any())
                    {
                        //else chexk if name in country
                        DBQuery = db.Countries.Where(s => s.NiceName.Contains(country));
                        if (DBQuery.Any())
                        {
                            return DBQuery.FirstOrDefault().NiceName;
                        }
                        else
                        {
                            //else chexk if part of name in country
                            if (country.Length >= 10)
                            {
                                string country_sub = country.Substring(0, 10);
                                DBQuery = db.Countries.Where(s => s.NiceName.Contains(country_sub));
                                if (DBQuery.Any())
                                {
                                    return DBQuery.FirstOrDefault().NiceName;
                                }
                            }
                        }
                    }
                }
                return country;
            }
        }

        /// <summary>
        /// Get youtube video id
        /// </summary>
        public string GetYouTubeVideoID(string url)
        {
            if(url.Contains("http") || url.Contains("www") || url.Contains("you"))
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
        ///get specific post data. Takes post id and data to return
        /// </summary>
        public string GetSiteLookupData(string unique_key)
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

        /// <summary>
        ///get specific post data. Takes post id and data to return
        /// </summary>
        public HtmlString GetSiteHtmlLookupData(string unique_key)
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


        /// <summary>
        ///removes html tags from text
        /// </summary>
        public string StripHTML(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = Regex.Replace(text, "<.*?>", "");
            }
            return text;
        }

        /// <summary>
        /// Returns the absolute url.
        /// </summary>
        public string Url(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }

    }
}
