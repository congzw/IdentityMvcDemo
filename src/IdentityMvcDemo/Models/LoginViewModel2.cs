using System.ComponentModel.DataAnnotations;

namespace IdentityMvcDemo.Models
{
    public class LoginViewModel2 : LoginViewModel
    {
        [Display(Name = "Validate Code")]
        [Required]
        public string ValidateCode { get; set; }
    }
}