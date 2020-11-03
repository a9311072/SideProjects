using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter course name.")]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Category { get; set; }

        public int? Level { get; set; }

        public int? Hours { get; set; }
    }
}