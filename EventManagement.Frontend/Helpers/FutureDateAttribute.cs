using System.ComponentModel.DataAnnotations;

namespace EventManagement.Frontend.Helpers
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Date >= DateTime.Now.Date)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessage ?? "Date must be in the future.");
            }
            return new ValidationResult("Invalid date format.");
        }
    }
}
