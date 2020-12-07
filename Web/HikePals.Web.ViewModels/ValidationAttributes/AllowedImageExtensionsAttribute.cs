using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;

public class AllowedImageExtensionsAttribute : ValidationAttribute
{
    private static readonly string[] _extensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };

    public AllowedImageExtensionsAttribute()
    {
    }


    public string GetErrorMessage()
    {
        return $"The image must be in one of the following formats:{_extensions}!";
    }

    protected override ValidationResult IsValid(
    object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(this.GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }
}