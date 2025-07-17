using System.ComponentModel.DataAnnotations;

namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password must be equal to Paswword")]
        public string ConfirmPassword { get; set; }
    }
}
