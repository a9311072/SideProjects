using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_5.Models
{
    [Table("OperationAction")]
    public class OperationAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public virtual ICollection<OperationBasicAction> GetOperationBasicActions { get; set; }
    }
}