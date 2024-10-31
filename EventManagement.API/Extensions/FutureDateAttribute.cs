using System.ComponentModel.DataAnnotations;
using System;

namespace EventManagement.API.Extensions
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessage ?? "Date must be in the future.");
            }
            return new ValidationResult("Invalid date format.");
        }
    }
}
