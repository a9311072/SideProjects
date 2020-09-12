using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_5.Areas.Admin.Models
{
    [Table("Menu")]
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Url { get; set; }

        [Required]
        public int ParentMenuId { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}