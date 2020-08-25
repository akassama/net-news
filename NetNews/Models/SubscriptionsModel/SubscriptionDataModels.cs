using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.SubscriptionsDataModel
{
    /* SUBSCRIBERS MODEL */
    [Table("Subscribers")]
    public class SubscribersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Subscriber ID")]
        public string SubscriberID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "SubscriberEmail")]
        public string SubscriberEmail { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "CategoryID")]
        public string CategoryID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Subscribed")]
        public DateTime? DateSubscribed { get; set; } = DateTime.Now;
    }


    /* CATEGORIES SUBSCRIPTION MODEL */
    [Table("CategoriesSubscription")]
    public class CategoriesSubscriptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Subscriber ID")]
        public string SubscriberID { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Category ID")]
        public string CategoryID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Subscribed")]
        public DateTime? DateSubscribed { get; set; } = DateTime.Now;
    }


    /* TAGS SUBSCRIPTION MODEL */
    [Table("TagsSubscription")]
    public class TagsSubscriptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Subscriber ID")]
        public string SubscriberID { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Tag ID")]
        public string TagID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Subscribed")]
        public DateTime? DateSubscribed { get; set; } = DateTime.Now;
    }


    /* AUTHOR SUBSCRIPTION MODEL */
    [Table("AuthorsSubscription")]
    public class AuthorsSubscriptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Subscriber ID")]
        public string SubscriberID { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Author ID")]
        public string AuthorID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Subscribed")]
        public DateTime? DateSubscribed { get; set; } = DateTime.Now;
    }
}
