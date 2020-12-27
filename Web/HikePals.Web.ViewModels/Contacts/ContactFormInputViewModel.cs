namespace HikePals.Web.ViewModels.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ContactFormInputViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
