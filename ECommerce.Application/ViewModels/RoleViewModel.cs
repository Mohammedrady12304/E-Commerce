using System.ComponentModel.DataAnnotations;

namespace E_Commerce1.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

    }
}
