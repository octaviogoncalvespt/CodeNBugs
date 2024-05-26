using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PlantEShop.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
