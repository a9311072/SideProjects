using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_5.Areas.Admin.Models
{
    [Table("Role")]
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }

    //[Table("UserRole")]
    //public class UserRole
    //{
    //    [Key]
    //    [Column(Order = 0)]
    //    [MaxLength(128)]
    //    public string UserId { get; set; }

    //    [Key]
    //    [Column(Order = 1)]
    //    [MaxLength(32)]
    //    public string RoleId { get; set; }
    //}
}