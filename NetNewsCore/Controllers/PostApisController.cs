using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetNews.Models;
using NetNews.Models.AppModels;
using NetNews.Models.PostsDataModel;

namespace NetNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApisController : ControllerBase
    {
        AppFunctions functions = new AppFunctions();
        private readonly DBConnection _context;

        public PostApisController(DBConnection context)
        {
            _context = context;
        }

        // GET: api/GetLatestHeader
        [HttpGet("GetLatestHeader")]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetLatestHeader()
        {
            try
            {
                DateTime Yesterday = DateTime.Now.AddDays(-12);
                var data = _context.vwPostsApproved.Where(s => (s.FeaturedPost == 1 || s.IsBreakingNews == 1) && s.ApprovalsDateAdded >= Yesterday).OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                if (!data.Any())
                {
                    data = _context.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                }
                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // GET: api/GetRecentNews
        //15 rows image on left and header on right
        [HttpGet("GetRecentNews")]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetRecentNews()
        {
            try
            {
                string LatestHeaderID = ""; //get id to exclude from query results
                DateTime Yesterday = DateTime.Now.AddDays(-12);
                var LatestHeader = _context.vwPostsApproved.Where(s => (s.FeaturedPost == 1 || s.IsBreakingNews == 1) && s.ApprovalsDateAdded >= Yesterday).OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                if (!LatestHeader.Any())
                {
                    LatestHeaderID = LatestHeader.FirstOrDefault().PostID;
                }

                var data = _context.vwPostsApproved.Where(s => s.PostID != LatestHeaderID).OrderByDescending(s => s.ApprovalsDateAdded).Take(15);

                return Ok(await data.ToListAsync());

            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);

                return NotFound();
            }
        }


        // GET: api/GetFeaturedNews
        //split into 2x2
        [HttpGet("GetFeaturedNews")]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetFeaturedNews()
        {
            try
            {
                var data = _context.vwPostsApproved.Where(s => s.FeaturedPost == 1).OrderByDescending(s => s.ApprovalsDateAdded).Take(6);
                if (!data.Any())
                {
                    data = _context.vwPostsApproved.OrderByDescending(s => s.ApprovalsDateAdded).Take(1);
                }
                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/GetCategories
        //split into 2x2
        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetCategories()
        {
            try
            {
                var data = _context.Categories.Where(s => s.IsPublished == 1).OrderBy(s => s.CategoryName).Take(10);
                if (!data.Any())
                {
                    return NoContent();
                }
                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }

       
        // GET: api/PostApis/GetCategoryPost
        [HttpGet("GetCategoryPost/{id}")]
        public async Task<ActionResult<CategoriesModel>> GetCategoryPost(string id)
        {
            try
            {
                var categoriesModel = _context.Categories.Where(s => s.ShortCategoryName == id);

                if (!categoriesModel.Any())
                {
                    return NoContent();
                }

                string CategoryID = categoriesModel.FirstOrDefault().CategoryID;

                var data = _context.vwPostsApproved.Where(s => s.PostCategory == CategoryID).OrderByDescending(s => s.ApprovalsDateAdded).Take(3);


                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/PostDetails/5
        [HttpGet("PostDetails/{id}")]
        public async Task<ActionResult<PostsModel>> PostDetails(string id)
        {
            try
            {
                var postsModel = _context.vwPostsApproved.Where(s => s.PostID == id);

                if (!postsModel.Any())
                {
                    return NoContent();
                }
                return Ok(await postsModel.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/GetSimilarPosts/PostID
        [HttpGet("GetSimilarPosts/{id}")]
        public async Task<ActionResult<PostsModel>> GetSimilarPosts(string id)
        {
            try
            {
                var postsModel = _context.vwPostsApproved.Where(s => s.PostID == id);
                string PostCategory = "";
                string PostTags = "";

                if (!postsModel.Any())
                {
                    return NoContent();
                }

                PostCategory = _context.vwPostsApproved.Where(s => s.PostID == id).FirstOrDefault().PostCategory;
                PostTags = _context.vwPostsApproved.Where(s => s.PostID == id).FirstOrDefault().PostTags;

                var data = _context.vwPostsApproved.Where(s => (s.PostCategory == PostCategory || s.PostTags.Contains(PostTags)) && s.PostID != id).Take(2);

                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/GetPostData/postid&returndata
        [HttpGet("GetPostData/{id}/{return_data}")]
        public ActionResult<PostsModel> GetPostData(string id, string return_data)
        {
            try
            {

                if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(return_data))
                {
                    return NotFound();
                }

                string post_id = id;
                string ReturnValue = "";
                switch (return_data)
                {
                    case "PostType":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostType != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostType : "";
                        break;
                    case "PostPermalink":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostPermalink != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostPermalink : "";
                        break;
                    case "PostAuthor":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostAuthor != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAuthor : "";
                        break;
                    case "PostCategory":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostCategory != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostCategory : "";
                        break;
                    case "PostSubCategory":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostSubCategory != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostSubCategory : "";
                        break;
                    case "PostTitle":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostTitle != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTitle : "";
                        break;
                    case "PostExtract":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostExtract != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostExtract : "";
                        break;
                    case "PostImage":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostImage != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostImage : "";
                        break;
                    case "ImageCaption":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.ImageCaption != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().ImageCaption : "";
                        break;
                    case "IsBreakingNews":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.IsBreakingNews != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().IsBreakingNews.ToString() : "0";
                        break;
                    case "PostContent":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostContent != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostContent : "";
                        break;
                    case "PostVideoType":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostVideoType != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoType : "";
                        break;
                    case "PostVideoLink":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostVideoLink != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostVideoLink : "";
                        break;
                    case "PostAudioType":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostAudioType != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAudioType : "";
                        break;
                    case "PostAudioLink":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostAudioLink != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostAudioLink : "";
                        break;
                    case "PostTags":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.PostTags != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().PostTags : "";
                        break;
                    case "UpdatedBy":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.UpdatedBy != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().UpdatedBy : "";
                        break;
                    case "DateAdded":
                        ReturnValue = (_context.Posts.Any(s => s.PostID == post_id && s.DateAdded != null)) ? _context.Posts.Where(s => s.PostID == post_id).FirstOrDefault().DateAdded.ToString() : "";
                        break;
                    case "ApprovalsDateAdded":
                        ReturnValue = (_context.vwPostsApproved.Any(s => s.PostID == post_id && s.ApprovalsDateAdded != null)) ? _context.vwPostsApproved.Where(s => s.PostID == post_id).FirstOrDefault().ApprovalsDateAdded.ToString() : "";
                        break;
                    default:
                        ReturnValue = "NA";
                        break;
                }

                return Ok(ReturnValue);
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/GetPopularThisWeek
        //split into 2x2
        [HttpGet("GetPopularThisWeek")]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetPopularThisWeek()
        {
            try
            {
                var data = _context.vwPopularThisWeek.OrderByDescending(s => s.ValueOccurrence).Skip(1).Take(12);
                if (!data.Any())
                {
                    return NoContent();
                }
                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // GET: api/PostApis/GetSearchResults
        [HttpGet("GetSearchResults/{q}")]
        public async Task<ActionResult<CategoriesModel>> GetSearchResults(string q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    return NotFound();
                }

                var data = _context.vwPostsApproved.Where(s => s.PostTitle.Contains(q) || s.PostExtract.Contains(q) || s.PostContent.Contains(q) || s.PostTags.Contains(q)).OrderByDescending(s => s.ApprovalsDateAdded).Take(20);

                if (!data.Any())
                {
                    return NoContent();
                }

                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/PostApis/GetTagResults
        [HttpGet("GetTagResults/{q}")]
        public async Task<ActionResult<CategoriesModel>> GetTagResults(string q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    return NotFound();
                }

                var data = _context.vwPostsApproved.Where(s => s.PostTags.Contains(q)).OrderByDescending(s => s.ApprovalsDateAdded).Take(20);

                if (!data.Any())
                {
                    return NoContent();
                }

                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/PostApis/GetCategoryResults
        [HttpGet("GetCategoryResults/{q}")]
        public async Task<ActionResult<CategoriesModel>> GetCategoryResults(string q)
        {
            try
            {
                if (string.IsNullOrEmpty(q))
                {
                    return NotFound();
                }

                string CategoryID = functions.GetCategoryID(q);

                var data = _context.vwPostsApproved.Where(s => s.PostCategory == CategoryID).OrderByDescending(s => s.ApprovalsDateAdded).Take(20);

                if (!data.Any())
                {
                    return NoContent();
                }

                return Ok(await data.ToListAsync());
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/GetAccountData/account_id&returndata
        [HttpGet("GetAccountData/{account_id}/{return_data}")]
        public ActionResult<PostsModel> GetAccountData(string account_id, string return_data)
        {
            try
            {

                if (string.IsNullOrEmpty(account_id) && string.IsNullOrEmpty(return_data))
                {
                    return NotFound();
                }

                string post_id = account_id;
                string ReturnValue = "";
                switch (return_data)
                {
                    case "FirstName":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.FirstName != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().FirstName : "";
                        break;
                    case "LastName":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.LastName != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().LastName : "";
                        break;
                    case "FullName":
                        ReturnValue = _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().FirstName + " " + _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().LastName;
                        break;
                    case "Email":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.Email != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Email : "";
                        break;
                    case "ProfilePicture":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.ProfilePicture != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().ProfilePicture : "";
                        break;
                    case "Active":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.Active != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Active.ToString() : "";
                        break;
                    case "Oauth":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.Oauth != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Oauth.ToString() : "";
                        break;
                    case "EmailVerification":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.EmailVerification != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().EmailVerification.ToString() : "";
                        break;
                    case "DirectoryName":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.DirectoryName != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().DirectoryName : "";
                        break;
                    case "Country":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.Country != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Country : "";
                        break;
                    case "CountryCode":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.CountryCode != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().CountryCode.ToString() : "";
                        break;
                    case "PhoneNumber":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.PhoneNumber != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumber.ToString() : "";
                        break;
                    case "PhoneNumberVerification":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.PhoneNumberVerification != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumberVerification.ToString() : "";
                        break;
                    case "Biography":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.Biography != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Biography : "";
                        break;
                    case "DateOfBirth":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.DateOfBirth != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().DateOfBirth.ToString() : "";
                        break;
                    case "Gender":
                        ReturnValue = (_context.AccountDetails.Any(s => s.AccountID == account_id && s.Gender != null)) ? _context.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Gender : "";
                        break;
                    case "UpdatedBy":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.UpdatedBy != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().UpdatedBy : "";
                        break;
                    case "UpdateDate":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.UpdateDate != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().UpdateDate.ToString() : "";
                        break;
                    case "DateAdded":
                        ReturnValue = (_context.Accounts.Any(s => s.AccountID == account_id && s.DateAdded != null)) ? _context.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().DateAdded.ToString() : "";
                        break;
                    default:
                        ReturnValue = "NA";
                        break;
                }

                return Ok(ReturnValue);
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return NotFound();
            }
        }


        // GET: api/PostApis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostsModel>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
            //return Ok(await _context.Posts.ToListAsync());
        }

        // PUT: api/PostApis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostsModel(int id, PostsModel postsModel)
        {
            if (id != postsModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(postsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostApis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostsModel>> PostPostsModel(PostsModel postsModel)
        {
            _context.Posts.Add(postsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostsModel", new { id = postsModel.ID }, postsModel);
        }

        // DELETE: api/PostApis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostsModel>> DeletePostsModel(int id)
        {
            var postsModel = await _context.Posts.FindAsync(id);
            if (postsModel == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(postsModel);
            await _context.SaveChangesAsync();

            return postsModel;
        }

        private bool PostsModelExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }
}
