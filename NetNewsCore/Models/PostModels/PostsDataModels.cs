using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.PostsDataModel
{
    /* POSTS MODEL */
    [Table("Posts")]
    public class PostsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "Post Permalink")]
        public string PostPermalink { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }

        [Required]
        [Display(Name = "Post Category")]
        public string PostCategory { get; set; }

        [Display(Name = "Sub Category")]
        public string PostSubCategory { get; set; }

        [Required]   
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        
        [Display(Name = "Post Extract")]
        public string PostExtract { get; set; }

        
        [Display(Name = "Post Image")]
        public string PostImage { get; set; }

        
        [Display(Name = "Image Caption")]
        public string ImageCaption { get; set; }

        
        [Display(Name = "Is Breaking News")]
        public int? IsBreakingNews { get; set; } = 0;

        [Display(Name = "Is Featured Post")]
        public int? FeaturedPost { get; set; } = 0;

        [Display(Name = "Post Content")]
        public string PostContent { get; set; }
        
        [Display(Name = "Video Type")]
        public string PostVideoType { get; set; }

        [Display(Name = "Post Video Link")]
        public string PostVideoLink { get; set; }

        [Display(Name = "Audio Type")]
        public string PostAudioType { get; set; }

        [Display(Name = "Post Audio Link")]
        public string PostAudioLink { get; set; }
        
        [Display(Name = "Post Tags")]
        public string PostTags { get; set; }

        [Required]
        [Display(Name = "Editor")]
        public string PostEditor { get; set; }

        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }


    /* ALL POSTS VIEWMODEL */
    [Table("vwPosts")]
    public class vwPostsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Permalink")]
        public string PostPermalink { get; set; }

        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }

        [Display(Name = "Post Category")]
        public string PostCategory { get; set; }

        [Display(Name = "Sub Category")]
        public string PostSubCategory { get; set; }

        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Display(Name = "Post Extract")]
        public string PostExtract { get; set; }

        [Display(Name = "Post Image")]
        public string PostImage { get; set; }

        [Display(Name = "Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name = "Is Breaking News")]
        public int? IsBreakingNews { get; set; } = 0;

        [Display(Name = "Is Featured Post")]
        public int? FeaturedPost { get; set; } = 0;

        [Display(Name = "Post Content")]
        public string PostContent { get; set; }

        [Display(Name = "Video Type")]
        public string PostVideoType { get; set; }

        [Display(Name = "Post Video Link")]
        public string PostVideoLink { get; set; }

        [Display(Name = "Audio Type")]
        public string PostAudioType { get; set; }

        [Display(Name = "Post Audio Link")]
        public string PostAudioLink { get; set; }

        [Display(Name = "Post Tags")]
        public string PostTags { get; set; }

        [Display(Name = "Editor")]
        public string PostEditor { get; set; }

        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Approvals ID")]
        public int ApprovalsID { get; set; }

        [Display(Name = "Approvals PostID ID")]
        public string ApprovalsPostID { get; set; }

        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Display(Name = "ApprovalState")]
        public int ApprovalState { get; set; } 

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Date Approved")]
        public DateTime? DateApproved { get; set; }

        [Display(Name = "Approvals Date Added")]
        public DateTime? ApprovalsDateAdded { get; set; } 
    }




    /* ALL APPROVED POSTS VIEWMODEL */
    [Table("vwPostsApproved")]
    public class vwPostsApprovedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Permalink")]
        public string PostPermalink { get; set; }

        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }

        [Display(Name = "Post Category")]
        public string PostCategory { get; set; }

        [Display(Name = "Sub Category")]
        public string PostSubCategory { get; set; }

        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Display(Name = "Post Extract")]
        public string PostExtract { get; set; }

        [Display(Name = "Post Image")]
        public string PostImage { get; set; }

        [Display(Name = "Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name = "Is Breaking News")]
        public int? IsBreakingNews { get; set; } = 0;

        [Display(Name = "Is Featured Post")]
        public int? FeaturedPost { get; set; } = 0;

        [Display(Name = "Post Content")]
        public string PostContent { get; set; }

        [Display(Name = "Video Type")]
        public string PostVideoType { get; set; }

        [Display(Name = "Post Video Link")]
        public string PostVideoLink { get; set; }

        [Display(Name = "Audio Type")]
        public string PostAudioType { get; set; }

        [Display(Name = "Post Audio Link")]
        public string PostAudioLink { get; set; }

        [Display(Name = "Post Tags")]
        public string PostTags { get; set; }

        [Display(Name = "Editor")]
        public string PostEditor { get; set; }

        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Approvals ID")]
        public int ApprovalsID { get; set; }

        [Display(Name = "Approvals PostID ID")]
        public string ApprovalsPostID { get; set; }

        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Display(Name = "ApprovalState")]
        public int ApprovalState { get; set; }

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Date Approved")]
        public DateTime? DateApproved { get; set; }

        [Display(Name = "Approvals Date Added")]
        public DateTime? ApprovalsDateAdded { get; set; }
    }


    /* CATEGORIES MODEL */
    [Table("Categories")]
    public class CategoriesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Category ID")]
        public string CategoryID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Short Category Name")]
        public string ShortCategoryName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Category Parent")]
        public string CategoryParent { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "Category Icon")]
        public string CategoryIcon { get; set; }

        [Display(Name = "Category Order")]
        public int CategoryOrder { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Published")]
        public int IsPublished { get; set; } = 0;

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Is Header?")]
        public int IsHeader { get; set; } = 0;

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }





    /* TAGS MODEL */
    [Table("Tags")]
    public class TagsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Tag ID")]
        public string TagID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Short Tag Name")]
        public string ShortTagName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Tag Description")]
        public string TagDescription { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }




    /* GALLERY IMAGES MODEL */
    [Table("GalleryImages")]
    public class GalleryImagesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "ImageLink")]
        public string ImageLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "Image Caption")]
        public string ImageCaption { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* VIDEO UPLOADS MODEL */
    [Table("VideoUploads")]
    public class VideoUploadsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "VideoLink")]
        public string VideoLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "Video Caption")]
        public string VideoCaption { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }




    /* POST APPROVALS MODEL */
    [Table("PostApprovals")]
    public class PostApprovalsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Approval State")]
        public int ApprovalState { get; set; } = 0;

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Date Approved")]
        public DateTime? DateApproved { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* POST VIEWS MODEL */
    [Table("PostViews")]
    public class PostViewsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }

        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Browser")]
        public string Browser { get; set; }

        [Display(Name = "Device")]
        public string Device { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; } = DateTime.Now;

        [Display(Name = "OtherDetails")]
        public string OtherDetails { get; set; }
    }



    /* POST REVIEWS */
    [Table("PostReviews")]
    public class PostReviewsModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Reviewer ID")]
        public string ReviewerID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Review Comment")]
        public string ReviewComment { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }

    /* POST REVIEWS VIEW MODEL */
    [Table("vwPostReviews")]
    public class vwPostReviewsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }

        [Display(Name = "Post Category")]
        public string PostCategory { get; set; }

        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Display(Name = "Approval State")]
        public int ApprovalState { get; set; }

        [Display(Name = "Reviews PostID")]
        public string ReviewsPostID { get; set; }

        [Display(Name = "Reviewer ID")]
        public string ReviewerID { get; set; }

        [Display(Name = "Review Comment")]
        public string ReviewComment { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }


    /* POPULAR THIS WEEK MODEL */
    [Table("vwPopularThisWeek")]
    public class vwPopularThisWeekModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Value Occurrence")]
        public int ValueOccurrence { get; set; }
    }


    /* TOP TEN MODEL */
    [Table("TopTenList")]
    public class TopTenListModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "List ID")]
        public string ListID { get; set; }

        [Display(Name = "List Type")]
        public string ListType { get; set; }


        [Display(Name = "Title")]
        public string ListTitle { get; set; }

        [Required]
        [Display(Name = "List Order")]
        public int? ListOrder { get; set; } = 0;

        [Required]
        [Display(Name = "List Link")]
        public string ListLink { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* EMBEDDED MUSIC MODEL */
    [Table("EmbeddedMusic")]
    public class EmbeddedMusicModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Embed ID")]
        public string EmbedID { get; set; }

        [Display(Name = "Embed Title")]
        public string EmbedTitle { get; set; }


        [Display(Name = "Embed Type")]
        public string EmbedType { get; set; }

        [Required]
        [Display(Name = "Embed Code")]
        public string EmbedCode { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* NEWS API MODEL */
    [Table("NewsApi")]
    public class NewsApiModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Url To Image")]
        public string UrlToImage { get; set; }

        [Display(Name = "Published At")]
        public string PublishedAt { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }
    }


}
