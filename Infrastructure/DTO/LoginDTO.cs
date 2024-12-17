using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class LoginDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
