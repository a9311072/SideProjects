using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_5.Models
{
    [Table("Operation")]
    public class Operation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public int ActionId { get; set; }
        public int EQPServiceId { get; set; }

        public virtual OperationAction OperationAction { get; set; }
        public virtual EQPService EQPService { get; set; }
    }
}