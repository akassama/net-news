using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.ThemeDataModels
{
    /* THEMES MODEL */
    [Table("Themes")]
    public class ThemesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Theme ID")]
        public string ThemeID { get; set; }

        [Required]
        [Display(Name = "Theme Name")]
        public string ThemeName { get; set; }

        [Required]
        [Display(Name = "Theme Description")]
        public string ThemeDescription { get; set; }
    }

    /* THEME SETTINGS MODEL */
    [Table("ThemeSettings")]
    public class ThemeSettingsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Theme ID")]
        public string ThemeID { get; set; }

        [Required]
        [Display(Name = "Theme Key")]
        public string ThemeKey { get; set; }

        [Required]
        [Display(Name = "Theme Value")]
        public string ThemeValue { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime? ActivityDate { get; set; } = DateTime.Now;
    }
}
