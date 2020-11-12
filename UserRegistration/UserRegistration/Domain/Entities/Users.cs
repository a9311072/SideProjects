using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter user name.")]
        [MaxLength(128)]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string CountryCode { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter the confirm-password.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

    }

}