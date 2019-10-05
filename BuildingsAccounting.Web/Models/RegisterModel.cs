using System.ComponentModel.DataAnnotations;

namespace BuildingsAccounting.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string PasswordConfirm { get; set; }
    }
}