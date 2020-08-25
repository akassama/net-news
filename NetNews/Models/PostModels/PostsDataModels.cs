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

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "PostPermalink")]
        public string PostPermalink { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "PostAuthor")]
        public string PostAuthor { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "PostTitle")]
        public string PostTitle { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "PostExtract")]
        public string PostExtract { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "PostImage")]
        public string PostImage { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "ImageCaption")]
        public string ImageCaption { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "IsBreakingNews")]
        public int IsBreakingNews { get; set; } = 0;

        [StringLength(50000, MinimumLength = 80)]
        [Display(Name = "PostContent")]
        public string PostContent { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "PostTags")]
        public string PostTags { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
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
        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "CategoryParent")]
        public string CategoryParent { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "CategoryDescription")]
        public string CategoryDescription { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "CategoryIcon")]
        public string CategoryIcon { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "IsPublished")]
        public int IsPublished { get; set; } = 0;

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "IsHeader")]
        public int IsHeader { get; set; } = 0;

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
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
        [Display(Name = "TagName")]
        public string TagName { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "TagDescription")]
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




    /* GALLERIES MODEL */
    [Table("Galleries")]
    public class GalleriesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Gallery ID")]
        public string GalleryID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "GalleryPermalink")]
        public string GalleryPermalink { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "GalleryPostAuthor")]
        public string GalleryPostAuthor { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "GalleryPostTitle")]
        public string GalleryPostTitle { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "ShortExtract")]
        public string ShortExtract { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "IsApproved")]
        public int IsApproved { get; set; } = 0;

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
        [Display(Name = "Gallery ID")]
        public string GalleryID { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "ImageLink")]
        public string ImageLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "ImageCaption")]
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






    /* VIDEOS MODEL */
    [Table("Videos")]
    public class VideosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Video ID")]
        public string VideoID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "VideoPermalink")]
        public string VideoPermalink { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "VideoPostAuthor")]
        public string VideoPostAuthor { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "VideoPostTitle")]
        public string VideoPostTitle { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "ShortExtract")]
        public string ShortExtract { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "VideoPostTags")]
        public string VideoPostTags { get; set; }

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
        [Display(Name = "Video ID")]
        public string VideoID { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "VideoLink")]
        public string VideoLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "ImageCaption")]
        public string ImageCaption { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "IsApproved")]
        public int IsApproved { get; set; } = 0;

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

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Post ID")]
        public string PostID { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "ApprovalState")]
        public int ApprovalState { get; set; } = 0;

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Approved")]
        public DateTime DateApproved { get; set; }

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

        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Browser")]
        public string Browser { get; set; }

        [Display(Name = "Device")]
        public string Device { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime? VisitDate { get; set; } = DateTime.Now;

        [Display(Name = "OtherDetails")]
        public string OtherDetails { get; set; }
    }


    /* REVISION HISTORY MODEL */
    [Table("RevisionHistory")]
    public class RevisionHistoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Column")]
        public string PostColumn { get; set; }

        [StringLength(50000, MinimumLength = 1)]
        [Display(Name = "Origin")]
        public string Origin { get; set; }

        [StringLength(50000, MinimumLength = 1)]
        [Display(Name = "After Changes")]
        public string AfterChange { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }


}
