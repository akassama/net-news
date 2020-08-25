using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AccessDataModels
{
    /* ROLES MODEL */
    [Table("Roles")]
    public class RolesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "RoleID")]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "RoleDescription")]
        public string RoleDescription { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }


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



    /* ACCOUNT TO ROLE MODEL */
    [Table("AccountToRole")]
    public class AccountToRoleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Account ID")]
        public int AccountID { get; set; }


        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }



    /* ROLE TO PERMISSION MODEL */
    [Table("RoleToPermission")]
    public class RoleToPermissionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Role ID")]
        public int RoleID { get; set; }

        [Display(Name = "Permission ID")]
        public string PermissionID { get; set; }

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
