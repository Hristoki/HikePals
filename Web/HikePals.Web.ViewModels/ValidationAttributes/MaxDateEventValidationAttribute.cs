namespace HikePals.Web.ViewModels.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class MaxDateEventValidationAttribute : ValidationAttribute
    {
        public MaxDateEventValidationAttribute(int yearsCountInFuture)
        {
            this.MaxYear = DateTime.Now.AddYears(yearsCountInFuture).Year;
            this.ErrorMessage = $"The date should be between {DateTime.UtcNow.Year} and {this.MaxYear}.";
        }

        public int MaxYear { get; }

        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                if (intValue <= this.MaxYear)
                {
                    return true;
                }
            }

            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue.Year >= DateTime.UtcNow.Year
                    && dateTimeValue.Year <= this.MaxYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
