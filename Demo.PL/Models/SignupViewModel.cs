using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class SignupViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Format For Email")]
        public string Email {  get; set; }
        [Required]
        [MaxLength(5)]
       
        public string Password { get; set; }
        [Required]
        [MaxLength(5)]
        [Compare(nameof(Password), ErrorMessage = "Password Mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAgree {  get; set; }

    }
}
