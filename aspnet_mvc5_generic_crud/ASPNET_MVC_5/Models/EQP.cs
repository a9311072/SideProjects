using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_5.Models
{
    [Table("EQP")]
    public class EQP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }

    [Table("EQPService")]
    public class EQPService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }

    [Table("EQPServiceList")]
    public class EQPServiceList
    {
        [Key]
        [Column(Order = 0)]
        public int EQP_Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int EQPService_Id { get; set; }
    }
}