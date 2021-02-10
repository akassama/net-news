using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AppDataModels
{
    /* LOGIN VIEW MODEL */
    public class LoginViewModel
    {
        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 6 or more characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }


    /* CHANGE PASSWORD MODEL */
    public class ChangePasswordModel
    {
        [Key]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 6 or more characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }


    /* CHANGE PASSWORD MODEL */
    [Table("PasswordForgot")]
    public class PasswordForgotModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }

        [Required]
        [Display(Name = "Reset ID")]
        public string ResetID { get; set; }

        [Required]
        [Display(Name = "ResetDate")]
        public DateTime? ResetDate { get; set; } = DateTime.Now;
    }


    /* SITE LOOKUP DATA MODEL */
    [Table("SiteDataLookup")]
    public class SiteDataLookupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "Uinque Key")]
        public string UinqueKey { get; set; }

        [Display(Name = "Data Type")]
        public string DataType { get; set; }

        [Display(Name = "Data Options")]
        public string DataOptions { get; set; }

        [Display(Name = "Data Group")]
        public string DataGroup { get; set; }

        [Required]
        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* ACTIVITY LOGS MODEL */
    [Table("ActivityLogs")]
    public class ActivityLogsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Activity User")]
        public string ActivityUser { get; set; }

        [Display(Name = "Action By")]
        public string ActionBy { get; set; }

        [Display(Name = "Log Type")]
        public string LogType { get; set; }

        [Required]
        [Display(Name = "Action")]
        public string Action { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime? ActivityDate { get; set; } = DateTime.Now;
    }


    /* CONTACT MESSAGES MODEL */
    [Table("ContactMessages")]
    public class ContactMessagesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* CONTACT MESSAGES VIEWS MODEL */
    [Table("MessageViews")]
    public class MessageViewsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Message ID")]
        public string MessageID { get; set; }
    }


    /* ADVERT MODEL */
    [Table("Adverts")]
    public class AdvertsModel 
    {
        [Key]
        [Display(Name = "Advert ID")]
        public string AdvertID { get; set; }

        [Required]
        [Display(Name = "Advert Title")]
        public string AdvertTitle { get; set; }

        [Display(Name = "Advert Permalink")]
        public string AdvertPermalink { get; set; }
        
        [Display(Name = "Advert Image")]
        public string AdvertImage { get; set; }

        [Required]
        [Display(Name = "Advert Text")]
        public string AdvertText { get; set; }

        [Display(Name = "Post By")]
        public string PostBy { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* JOBS MODEL */
    [Table("Jobs")]
    public class JobsModel
    {
        [Key]
        [Display(Name = "Job ID")]
        public string JobID { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Advert Permalink")]
        public string JobAdvertPermalink { get; set; }

        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Application Link")]
        public string ApplicationLink { get; set; }

        [Required]
        [Display(Name = "Job Post")]
        public string JobPost { get; set; }

        [Display(Name = "Post By")]
        public string PostBy { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }

    /* COUNTRIES MODEL */
    [Table("Country")]
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "ISO")]
        public string ISO { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Nice Name")]
        public string NiceName { get; set; }

        [Display(Name = "ISO3")]
        public string ISO3 { get; set; }

        [Display(Name = "Num Code")]
        public Int16? NumCode { get; set; }

        [Display(Name = "Phone Code")]
        public int? PhoneCode { get; set; } 
    }


    /* SITE DATA MODEL */
    [Table("SiteStats")]
    public class SiteStatsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Stat Type")]
        public string StatType { get; set; }

        [Display(Name = "Action Value")]
        public string ActionValue { get; set; }

        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Browser")]
        public string Browser { get; set; }

        [Display(Name = "Device")]
        public string Device { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime ActionDate { get; set; } = DateTime.Now; 

        [Display(Name = "OtherDetails")]
        public string OtherDetails { get; set; }
    }



    /* VISIT LOGS MODEL */
    [Table("VisitLogs")]
    public class VisitLogsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Log Type")]
        public string LogType { get; set; }

        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }

        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Browser")]
        public string Browser { get; set; }

        [Display(Name = "Device")]
        public string Device { get; set; }

        [Display(Name = "Visit Time")]
        public string VisitTime { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; } = DateTime.Now;

        [Display(Name = "OtherDetails")]
        public string OtherDetails { get; set; }
    }


}
