namespace HikePals.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ForgotPassWordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
