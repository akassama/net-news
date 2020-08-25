using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AppDataModels
{
    /* LOGIN INFO MODEL */
    [Table("LoginInfo")]
    public class LoginInfoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }

        [Required]
        [Display(Name = "Failed Login Count")]
        public int FailedLoginCount { get; set; }

        [Required]
        [RegularExpression(@"^[0-1]$", ErrorMessage = "Only numbers allowed. 0 or 1")]
        [Display(Name = "Locked Status")]
        public int LockedStatus { get; set; }

        [Display(Name = "LockPeriod")]
        public DateTime? LockPeriod { get; set; }

        [Required]
        [Display(Name = "Total Logins")]
        public int TotalLogins { get; set; }

        [Required]
        [Display(Name = "Login Session ID")]
        public string LoginSessionID { get; set; }

        [Required]
        [Display(Name = "First Login")]
        public DateTime FirstLogin { get; set; } = DateTime.Now;
    }


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

}
