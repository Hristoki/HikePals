namespace HikePals.Web.ViewModels.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HikePals.Common;

    public class ContactFormInputViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinContactsSubjectLenght, ErrorMessage = GlobalConstants.SubjectLengthErrorMessage)]
        [MaxLength(GlobalConstants.MaxContactsSubjectLenght, ErrorMessage = GlobalConstants.SubjectLengthErrorMessage)]
        public string Subject { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinContactsContentLenght, ErrorMessage = GlobalConstants.ContactsContentLengthErrorMessage)]
        [MaxLength(GlobalConstants.MaxContactsContentLenght, ErrorMessage = GlobalConstants.ContactsContentLengthErrorMessage)]
        public string Content { get; set; }
    }
}
