using System.ComponentModel.DataAnnotations;

namespace E_Commerce1.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)] //عشان تظهر ال password ***
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)] //عشان تظهر ال password ***
        [Required]
        [Compare("Password")]
        public string ComfirmPassword { get; set; }

        public string Address { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
