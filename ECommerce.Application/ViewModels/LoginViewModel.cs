using System.ComponentModel.DataAnnotations;

namespace E_Commerce1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
