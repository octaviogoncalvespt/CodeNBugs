using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PlantEShop.VM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
