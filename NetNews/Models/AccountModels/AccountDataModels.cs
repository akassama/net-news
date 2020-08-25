using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AccountsDataModel
{
    /* ACCOUNTS MODEL */
    [Table("Accounts")]
    public class AccountsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 6 or more characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Active")]
        public int? Active { get; set; } = 0;

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Oauth")]
        public int? Oauth { get; set; } = 0;

        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Email Verification")]
        public int? EmailVerification { get; set; } = 0;

        [Display(Name = "Directory Name")]
        public string DirectoryName { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Remember Token")]
        public string RememberToken { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }




    /* ACCOUNTS DETAILS MODEL */
    [Table("AccountDetails")]
    public class AccountDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }

        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [RegularExpression(@"^[\d]{1,4}$", ErrorMessage = "Only numbers allowed. 4 digits max")]
        [Display(Name = "Country Code")]
        public int? CountryCode { get; set; }

        [RegularExpression(@"^[\d]{1,20}$", ErrorMessage = "Only phone numbers allowed. 20 digits max")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Biography")]
        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }





    /* SOCIAL MEDIA DETAILS MODEL */
    [Table("SocialMediaDetails")]
    public class SocialMediaDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "WebsiteLink")]
        public string WebsiteLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "FacebookLink")]
        public string FacebookLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "TwitterLink")]
        public string TwitterLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "InstagramLink")]
        public string InstagramLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "LinkedInLink")]
        public string LinkedInLink { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "SkypeName")]
        public string SkypeName { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "WhatsAppNumber")]
        public string WhatsAppNumber { get; set; }

        [StringLength(250, MinimumLength = 5)]
        [Display(Name = "TelegramAppNumber")]
        public string TelegramAppNumber { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }
}
