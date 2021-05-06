namespace HikePals.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = HikePals.Common.GlobalConstants.ConfirmPassErrorMessage)]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
