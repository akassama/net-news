using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AccessDataModels
{

    /* PERMISSIONS MODEL */
    [Table("Permissions")]
    public class PermissionsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Permission ID")]
        public int PermissionID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "Permission Name")]
        public string PermissionName { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "PermissionDescription")]
        public string PermissionDescription { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* ACCOUNT TO PERMISSION */
    [Table("AccountToPermission")]
    public class AccountToPermissionModel
    {
        [Key]
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }


        [Display(Name = "Permission ID")]
        public int PermissionID { get; set; }

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
