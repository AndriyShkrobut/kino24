using System.ComponentModel.DataAnnotations;

namespace kino24_user.BL.DTO.UserAuthentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Wrong email format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password doesn`t match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
    }
}
