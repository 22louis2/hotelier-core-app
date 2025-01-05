using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Core.Helpers
{
    public class PastDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dt)
            {
                if (dt.Date >= DateTime.Today.Date)
                    return ValidationResult.Success ?? new ValidationResult("Past date entry not allowed");
                else
                    return new ValidationResult("Past date entry not allowed");
            }
            return new ValidationResult("Invalid date supplied");
        }
    }
}
